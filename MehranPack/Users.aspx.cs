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
    public partial class Users : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindDrpType(); 

                if (Page.RouteData.Values["Id"].ToSafeInt()!=0)
                {
                    var repo = new UserRepository();
                    var toBeEditedUser = repo.GetById(Page.RouteData.Values["Id"].ToSafeInt());

                    if (toBeEditedUser != null)
                    {
                        txtName.Text = toBeEditedUser.FriendlyName;
                        drpKind.SelectedValue = toBeEditedUser.Type.ToString();
                    }
                }
            }
        }

        private void BindDrpType()
        {
            //var repo = new UserRepository();
            //List<Repository.Entity.Domain.Category> source = new List<Repository.Entity.Domain.Category>();
            //source.Add(new Repository.Entity.Domain.Category() { Id = -1, Name = "انتخاب کنید" });
            //source.AddRange(repo.GetAll());
            var source = new List<KeyValuePair<int, string>>();
            source.Add(new KeyValuePair<int, string>(0, "کاربر سیستمی"));
            source.Add(new KeyValuePair<int, string>(1, "اوپراتور تولید"));
            source.Add(new KeyValuePair<int, string>(2, "مسئول کنترل کیفیت"));

            drpKind.DataSource = source;
            drpKind.DataValueField = "Key";
            drpKind.DataTextField = "Value";
            drpKind.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtName.Text)) throw new LocalException("Name is empty", "نام  را وارد نمایید");
                if (string.IsNullOrEmpty(txtPassword.Text)) throw new LocalException("Password is empty", "رمز را وارد نمایید");
                if (string.IsNullOrEmpty(txtPasswordConfirm.Text)) throw new LocalException("Password confirm is empty", "تکرار رمز را وارد نمایید");
                if (txtPasswordConfirm.Text!= txtPassword.Text) throw new LocalException("Password and confirm are different", "رمز عبور و تکرار آن یکسان نیستند");

                UnitOfWork uow = new UnitOfWork();

                if (Page.RouteData.Values["Id"].ToSafeInt() == 0)
                {
                    var newUser = new Repository.Entity.Domain.User();

                    newUser.FriendlyName = newUser.Username =  txtName.Text;
                    newUser.Password = txtPassword.Text;
                    newUser.Type = drpKind.SelectedValue.ToSafeInt();
                    newUser.ReworkPassword = txtReworkPassword.Text;
                    
                    uow.Users.Create(newUser);
                }
                else
                {
                    var repo = uow.Users;
                    var tobeEditedUser = repo.GetById(Page.RouteData.Values["Id"].ToSafeInt());
                   
                    tobeEditedUser.Username = txtName.Text;
                    tobeEditedUser.FriendlyName = txtName.Text;
                    tobeEditedUser.Password = txtPassword.Text;
                    tobeEditedUser.Type = drpKind.SelectedValue.ToSafeInt();
                    tobeEditedUser.ReworkPassword = txtReworkPassword.Text;
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
            txtName.Text = txtPassword.Text = txtPasswordConfirm.Text= "";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.RedirectToRoute("Home");
        }
    }
}