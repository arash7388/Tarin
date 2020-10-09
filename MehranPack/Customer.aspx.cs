using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using Energy;
using Repository.DAL;

namespace MehranPack
{
    public partial class Customer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               if (Page.RouteData.Values["Id"].ToSafeInt() != 0)
                {
                    var repo = new CustomerRepository();
                    var tobeEditedCustomer = repo.GetById(Page.RouteData.Values["Id"].ToSafeInt());

                    txtCode.Text = tobeEditedCustomer.Code;
                    txtName.Text = tobeEditedCustomer.Name;
                    txtTel.Text = tobeEditedCustomer.Tel;
                    txtMobile.Text = tobeEditedCustomer.Mobile;
                    txtEmail.Text = tobeEditedCustomer.Email;
                    txtAdr.Text = tobeEditedCustomer.Address;
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
                    var newCustomer = new Repository.Entity.Domain.Customer();

                    newCustomer.Code = txtCode.Text;
                    newCustomer.Name = txtName.Text;
                    newCustomer.Tel =  txtTel.Text;
                    newCustomer.Mobile = txtMobile.Text;
                    newCustomer.Email = txtEmail.Text;
                    newCustomer.Address = txtAdr.Text;

                    uow.Customers.Create(newCustomer);
                }
                else
                {
                    var repo = uow.Customers;
                    var tobeEditedCustomer = repo.GetById(Page.RouteData.Values["Id"].ToSafeInt());
                    tobeEditedCustomer.Code = txtCode.Text;
                    tobeEditedCustomer.Name = txtName.Text;
                    tobeEditedCustomer.Tel = txtTel.Text;
                    tobeEditedCustomer.Mobile = txtMobile.Text;
                    tobeEditedCustomer.Email = txtEmail.Text;
                    tobeEditedCustomer.Address = txtAdr.Text;
                }

                uow.SaveChanges();
                ((Main)Page.Master).SetGeneralMessage("اطلاعات با موفقیت ذخیره شد",MessageType.Success);
                ClearControls();
            }
            catch (LocalException ex)
            {
                ((Main)Page.Master).SetGeneralMessage("خطا در دخیره سازی -" +ex.ResultMessage, MessageType.Error); 
            }
        }

        private void ClearControls()
        {
            txtCode.Text = "";
            txtName.Text = "";
            txtTel.Text = "";
            txtMobile.Text = "";
            txtEmail.Text = "";
            txtAdr.Text = "";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.RedirectToRoute("Home");
        }
    }
}