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
    public partial class Process : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //BindDrpType(); 

                if (Page.RouteData.Values["Id"].ToSafeInt()!=0)
                {
                    var repo = new ProcessRepository();
                    var toBeEditedPr = repo.GetById(Page.RouteData.Values["Id"].ToSafeInt());

                    if (toBeEditedPr != null)
                    {
                        txtName.Text = toBeEditedPr.Name;
                        //drpKind.SelectedValue = toBeEditedUser.Type.ToString();
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
                    var newPr = new Repository.Entity.Domain.Process();

                    newPr.Name =  txtName.Text;

                    uow.Processes.Create(newPr);
                }
                else
                {
                    var repo = uow.Processes;
                    var tobeEditedPr = repo.GetById(Page.RouteData.Values["Id"].ToSafeInt());
                   
                    tobeEditedPr.Name = txtName.Text;
                    
                    //tobeEditedPr.Type = drpKind.SelectedValue.ToSafeInt();
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
            Response.RedirectToRoute("Home");
        }
    }
}