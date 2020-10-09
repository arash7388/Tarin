using System;
using Common;
using Repository.DAL;

namespace AdminPanel
{
    public partial class Tag : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["Id"] != null)
                {
                    TagRepository repo = new TagRepository();
                    var tobeEditedProduct = repo.GetById(Request.QueryString["Id"].ToSafeInt());

                    txtCode.Text = tobeEditedProduct.Code;
                    txtName.Text = tobeEditedProduct.Name;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCode.Text)) throw new LocalException("Code is empty", "کد تگ را وارد نمایید");
                if (string.IsNullOrEmpty(txtName.Text)) throw new LocalException("Name is empty", "نام تگ را وارد نمایید");

                UnitOfWork uow = new UnitOfWork();

                if (Request.QueryString["Id"] == null)
                {
                    var newTag = new Repository.Entity.Domain.Tag();

                    newTag.Code = txtCode.Text;
                    newTag.Name = txtName.Text;
                    uow.Tags.Create(newTag);
                }
                else
                {
                    var repo = uow.Tags;
                    var tobeEditedTag = repo.GetById(Request.QueryString["Id"].ToSafeInt());
                    tobeEditedTag.Code = txtCode.Text;
                    tobeEditedTag.Name = txtName.Text;
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
    }
}