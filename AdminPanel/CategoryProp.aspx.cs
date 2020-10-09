using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using Repository.DAL;
using Repository.Entity.Domain;

namespace AdminPanel
{
    public partial class CategoryProp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindDrpType();
                BindDrpCategory();

                if (Request.QueryString["Id"] != null)
                {
                    var repo = new CategoryPropRepository();
                    var toBeEditedCatProp = repo.GetById(Request.QueryString["Id"].ToSafeInt());

                    if (toBeEditedCatProp != null)
                    {
                        txtName.Text = toBeEditedCatProp.Name;
                        txtCaption.Text = toBeEditedCatProp.Caption;
                        drpType.SelectedValue = toBeEditedCatProp.Type.ToString();
                        drpCategory.SelectedValue = toBeEditedCatProp.CategoryId.ToString();
                        chkHasDataSource.Checked = toBeEditedCatProp.HasDatasource;
                    }
                }
            }
        }

        private void BindDrpCategory()
        {
            var leaves = new CategoryRepository().GetAllLeafCategories();

            drpCategory.DataValueField = "Id";
            drpCategory.DataTextField = "Name";

            leaves.Insert(0, new Repository.Entity.Domain.Category()
            {
                Id = -1,
                Name = "انتخاب کنید"
            });

            drpCategory.DataSource = leaves;
            drpCategory.DataBind();
        }

        private void BindDrpType()
        {
            var source = new List<KeyValuePair<int, string>>();

            var names = Enum.GetNames(typeof(CategoryPropType));
            for (int i = 0; i < names.Length; i++)
                source.Add(new KeyValuePair<int, string>(i,names[i]));

            drpType.DataValueField = "Key";
            drpType.DataTextField = "Value";
            source.Insert(0,new KeyValuePair<int, string>(-1,"انتخاب کنید"));
            drpType.DataSource = source;
            drpType.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text == string.Empty) throw new LocalException("Name is empty", "نام را وارد نمایید");
                if (txtCaption.Text == string.Empty) throw new LocalException("Caption is empty", "برچسب را وارد نمایید");
                if (drpCategory.SelectedValue.ToSafeInt() == -1) throw new LocalException("Category is empty", "گروه را وارد نمایید");
                if (drpType.SelectedValue.ToSafeInt() == -1) throw new LocalException("Type is empty", "نوع را وارد نمایید");

                UnitOfWork u = new UnitOfWork();

                if (Request.QueryString["Id"] == null)
                {
                    u.CategoryProps.Create(new Repository.Entity.Domain.CategoryProp()
                    {
                        Name = txtName.Text,
                        Caption = txtCaption.Text,
                        CategoryId = drpCategory.SelectedValue.ToSafeInt(),
                        Type = drpType.SelectedValue.ToSafeInt(),
                        HasDatasource = chkHasDataSource.Checked
                    });
                }
                else
                {
                    var toBeEditedCatProp = u.CategoryProps.GetById(Request.QueryString["Id"].ToSafeInt());
                    toBeEditedCatProp.Name = txtName.Text;
                    toBeEditedCatProp.Caption = txtCaption.Text;
                    toBeEditedCatProp.Type = drpType.SelectedValue.ToSafeInt();
                    toBeEditedCatProp.CategoryId = drpCategory.SelectedValue.ToSafeInt();
                    toBeEditedCatProp.HasDatasource = chkHasDataSource.Checked;
                }

                u.SaveChanges();

                lblResult.InnerText = "اطلاعات با موفقیت ذخیره شد";
                ClearControls();
            }
            catch (LocalException ex)
            {
                lblResult.InnerText = ex.ResultMessage;
            }
            catch (Exception ex)
            {
                lblResult.InnerText = ex.Message;
            }
        }

        private void ClearControls()
        {
            txtName.Text = txtCaption.Text = "";
            drpCategory.SelectedValue = drpType.SelectedValue = "-1";
            chkHasDataSource.Checked = false;
        }
    }
}