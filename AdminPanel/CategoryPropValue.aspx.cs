using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using Repository.DAL;

namespace AdminPanel
{
    public partial class CategoryPropValue : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindDrpCategory();

                if (Request.QueryString["Id"] != null)
                {
                    var repo = new CategoryPropValueRepository();
                    var toBeEditedCatPropValue = repo.GetById(Request.QueryString["Id"].ToSafeInt());

                    if (toBeEditedCatPropValue != null)
                    {
                        drpCategoryProp.SelectedValue = toBeEditedCatPropValue.CategoryPropId.ToString();

                        var cat = new CategoryRepository().GetByCatPropId(toBeEditedCatPropValue.CategoryPropId);
                        drpCategory.SelectedValue = cat.Id.ToSafeString();
                        
                        txtValue.Text = toBeEditedCatPropValue.Value;
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (drpCategory.SelectedValue.ToSafeInt() == -1) throw new LocalException("Category is empty", "گروه را انتخاب نمایید");
                if (drpCategoryProp.SelectedValue.ToSafeString() == string.Empty) throw new LocalException("CaptegoryProp is empty", "ویژگی را انتخاب نمایید");
                if (txtValue.Text == string.Empty) throw new LocalException("Value is empty", "مقدار را وارد نمایید");

                UnitOfWork u = new UnitOfWork();

                if (Request.QueryString["Id"] == null)
                {
                    u.CategoryPropValues.Create(new Repository.Entity.Domain.CategoryPropValue()
                    {
                        CategoryId = drpCategory.SelectedValue.ToSafeInt(),
                        CategoryPropId = drpCategoryProp.SelectedValue.ToSafeInt(),
                        Value = txtValue.Text
                    });
                }
                else
                {
                    var toBeEditedCatPropValue = u.CategoryPropValues.GetById(Request.QueryString["Id"].ToSafeInt());
                    toBeEditedCatPropValue.CategoryPropId= drpCategoryProp.SelectedValue.ToSafeInt();
                    toBeEditedCatPropValue.Value = txtValue.Text;
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
            drpCategory.SelectedValue = "-1";
            drpCategoryProp.SelectedValue = null;
            txtValue.Text = "";
        }

        protected void drpCategory_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpCategory.SelectedValue.ToSafeInt() == -1) return;

            var repo = new CategoryPropRepository();
            drpCategoryProp.DataSource = repo.GetPropHaveDatasourceByCatId(drpCategory.SelectedValue.ToSafeInt());

            drpCategoryProp.DataValueField = "Id";
            drpCategoryProp.DataTextField = "Caption";

            drpCategoryProp.DataBind();
        }
    }
}