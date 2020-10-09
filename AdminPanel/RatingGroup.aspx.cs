using System;
using Common;
using Repository.DAL;

namespace AdminPanel
{
    public partial class RatingGroup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["Id"] != null)
                {
                    var repo = new RatingGroupRepository();
                    var toBeEditedRatingGroup = repo.GetById(Request.QueryString["Id"].ToSafeInt());

                    if (toBeEditedRatingGroup != null)
                        txtName.Text = toBeEditedRatingGroup.Name;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text == string.Empty) throw new LocalException("Name is empty", "نام را وارد نمایید");

                UnitOfWork uow = new UnitOfWork();

                if (Request.QueryString["Id"] == null)
                {
                    uow.RatingGroups.Create(new Repository.Entity.Domain.RatingGroup()
                    {
                        Name = txtName.Text,
                    });
                }
                else
                {
                    var toBeEditedRatingGroup = uow.RatingGroups.GetById(Request.QueryString["Id"].ToSafeInt());
                    toBeEditedRatingGroup.Name = txtName.Text;
                }
                uow.SaveChanges();

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
            txtName.Text = "";
        }
    }
}