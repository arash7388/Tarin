using Common;
using Repository.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MehranPack
{
    public partial class ReworkReason : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Page.RouteData.Values["Id"].ToSafeInt()!=0)
                {
                    var repo = new ReworkReasonRepository();
                    var toBeEditedReason = repo.GetById(Page.RouteData.Values["Id"].ToSafeInt());

                    if (toBeEditedReason != null)
                    {
                        txtName.Text = toBeEditedReason.Name;
                    }
                }
            }
        }

      
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtName.Text)) throw new LocalException("Name is empty", "نام  را وارد نمایید");

                UnitOfWork uow = new UnitOfWork();

                if (Page.RouteData.Values["Id"].ToSafeInt() == 0)
                {
                    var newReason = new Repository.Entity.Domain.ReworkReason();

                    newReason.Name =  txtName.Text;
                    
                    uow.ReworkReasons.Create(newReason);
                }
                else
                {
                    var repo = uow.ReworkReasons;
                    var tobeEditedReason = repo.GetById(Page.RouteData.Values["Id"].ToSafeInt());
                   
                    tobeEditedReason.Name = txtName.Text;
                }

                uow.SaveChanges();
                ((Main)Page.Master).SetGeneralMessage("اطلاعات با موفقیت ذخیره شد", MessageType.Success);
                ClearControls();
            }
            catch (LocalException ex)
            {
                ((Main)Page.Master).SetGeneralMessage("خطا در دخیره سازی -" + ex.ResultMessage, MessageType.Error);
            }
        }

        private void ClearControls()
        {
            txtName.Text = "";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.RedirectToRoute("ReworkReason");
        }
    }
}