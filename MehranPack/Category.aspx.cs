using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Repository.DAL;

namespace MehranPack
{
    public partial class Category : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BinddrpCat();

                if (Request.QueryString["Id"] != null)
                {
                    CategoryRepository repo = new CategoryRepository();
                    var toBeEditedCat = repo.GetById(Request.QueryString["Id"].ToSafeInt());

                    if (toBeEditedCat != null)
                    {
                        txtCode.Text = toBeEditedCat.Code;
                        txtName.Text = toBeEditedCat.Name;

                        if (toBeEditedCat.ParentId != null)
                            drpParentCat.SelectedValue = toBeEditedCat.ParentId.ToString();
                    }
                }
            }

        }

        private void BinddrpCat()
        {
            CategoryRepository repo = new CategoryRepository();
            List<Repository.Entity.Domain.Category> source = new List<Repository.Entity.Domain.Category>();
            source.Add(new Repository.Entity.Domain.Category() {Id = -1, Name = "انتخاب کنید"});
            
            if(Utility.IsInOutMode())
                source.AddRange(repo.GetAllWithFullNameForInOut());
            else
                source.AddRange(repo.GetAllWithFullName());

            drpParentCat.DataSource = source;
            drpParentCat.DataValueField = "Id";
            drpParentCat.DataTextField = "Name";
            drpParentCat.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCode.Text==string.Empty) throw new LocalException("Code is empty","کد گروه را وارد نمایید");
                if (txtName.Text==string.Empty) throw new LocalException("Name is empty","نام گروه را وارد نمایید");
                

                int? parentId = drpParentCat.SelectedValue.ToSafeInt() != -1 ? Convert.ToInt32(drpParentCat.SelectedValue) : (int?)null;
                UnitOfWork u = new UnitOfWork();

                if (Request.QueryString["Id"]==null)
                {
                    if(u.Categories.Get(a=>a.Code==txtCode.Text).Any())
                        throw new LocalException("Duplicate code", "کد گروه تکراری است");

                    var newCat = new Repository.Entity.Domain.Category()
                    {
                        Code = txtCode.Text,
                        Name = txtName.Text,
                        ParentId = parentId,
                        Image = fileUploadControl.FileBytes,
                        IsInOutCategory = Utility.IsInOutMode()
                    };

                    u.Categories.Create(newCat);

                    var p = u.Processes.Filter(a => a.Name == "اتمام تولید").FirstOrDefault();
                    if(p!=null)
                    {
                        u.ProcessCategories.Create(new Repository.Entity.Domain.ProcessCategory
                        {
                            Process = p,
                            Category = newCat,
                            Order=99
                        });
                    }
                }
                else
                {
                    var toBeEditedCat = u.Categories.GetById(Request.QueryString["Id"].ToSafeInt());
                    toBeEditedCat.Code = txtCode.Text;
                    toBeEditedCat.Name = txtName.Text;
                    toBeEditedCat.ParentId = parentId;
                    toBeEditedCat.Image = fileUploadControl.FileBytes;
                }
                
                u.SaveChanges();

                lblResult.InnerText = "اطلاعات با موفقیت ذخیره شد";
                ClearControls();
                BinddrpCat();
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
            txtName.Text = "";
            drpParentCat.SelectedIndex = 0;
            txtCode.Text = "";
        }
    }
}