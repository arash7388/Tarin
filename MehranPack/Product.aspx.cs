using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Repository.DAL;

namespace MehranPack
{
    public partial class Product : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BinddrpCat();

                if (Request.QueryString["Id"] != null)
                {
                    ProductRepository repo = new ProductRepository();
                    var tobeEditedProduct = repo.GetById(Request.QueryString["Id"].ToSafeInt());

                    txtCode.Text = tobeEditedProduct.Code;
                    txtName.Text = tobeEditedProduct.Name;
                    drpCategory.SelectedValue = tobeEditedProduct.ProductCategoryId.ToString();
                 }
            }
        }

       
        protected void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                if (string.IsNullOrEmpty(txtCode.Text)) throw new LocalException("Code is empty","کد کالا را وارد نمایید");
                if (string.IsNullOrEmpty(txtName.Text)) throw new LocalException("Name is empty", "نام کالا را وارد نمایید");
                if (Convert.ToInt32(drpCategory.SelectedItem.Value) == -1) throw new LocalException("Category is empty", "گروه محصول کالا را وارد نمایید");

                if (!ImageIsValid()) return;

                UnitOfWork uow = new UnitOfWork();

                  if (Request.QueryString["Id"] == null)
                  {
                    if (uow.Products.Get(a => a.Code == txtCode.Text).Any())
                        throw new LocalException("Duplicate code", "کد کالا تکراری است");

                    var newProduct = new Repository.Entity.Domain.Product();

                      newProduct.Code = txtCode.Text;
                      newProduct.Name = txtName.Text;
                      newProduct.ProductCategoryId = Convert.ToInt32(drpCategory.SelectedItem.Value);
                      newProduct.IsInOutProduct = Utility.IsInOutMode();
                      newProduct.Image = fileUploadControl.FileBytes;
                      uow.Products.Create(newProduct);
                  }
                  else
                  {
                      var repo = uow.Products;
                      var tobeEditedProduct = repo.GetById(Request.QueryString["Id"].ToSafeInt());
                      tobeEditedProduct.Code = txtCode.Text;
                      tobeEditedProduct.Name = txtName.Text;
                      tobeEditedProduct.ProductCategoryId = drpCategory.SelectedValue.ToSafeInt();
                      tobeEditedProduct.Image = fileUploadControl.FileBytes;
                  }
                
                
                uow.SaveChanges();
                lblResult.InnerText = "اطلاعات با موفقیت ذخیره شد";
                ClearControls();
            }
            catch (LocalException ex)
            {
                lblResult.InnerText = ex.ResultMessage;
            }
        }

        private void ClearControls()
        {
            txtCode.Text = "";
            txtName.Text = "";
        }

        private bool ImageIsValid()
        {
            bool result = false;

            if (fileUploadControl.HasFile)
            {
                try
                {
                    if (fileUploadControl.PostedFile.ContentType == "image/jpeg")
                    {
                        if (fileUploadControl.PostedFile.ContentLength < 52000)
                        {
                            //string filename = Path.GetFileName(FileUploadControl.FileName);
                            //FileUploadControl.SaveAs(Server.MapPath("~/") + filename);
                            //imgPath = StatusLabel.Text = filename;
                            result = true;
                        }
                        else
                            statusLabel.Text = "حجم تصویر باید کمتر از 50 کیلوبایت باشد ";
                    }
                    else
                        statusLabel.Text = "فرمت عکس باید Jpeg باشد";
                }
                catch (Exception ex)
                {
                    statusLabel.Text = "خطا در  ذخیره تصویر";
                }
            }
            else
            {
                result = true;
            }

            return result;
        }


        private void BinddrpCat()
        {
            CategoryRepository repo = new CategoryRepository();
            List<Repository.Entity.Domain.Category> source = new List<Repository.Entity.Domain.Category>();
            source.Add(new Repository.Entity.Domain.Category() { Id = -1, Name = "انتخاب کنید" });

            if (Utility.IsInOutMode())
              source.AddRange(repo.GetAllWithFullNameForInOut());
            else
              source.AddRange(repo.GetAllWithFullName());

            drpCategory.DataSource = source;
            drpCategory.DataValueField = "Id";
            drpCategory.DataTextField = "Name";
            drpCategory.DataBind();
        }
    }
}