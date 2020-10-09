using System;
using Common;
using Repository.DAL;

namespace AdminPanel
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Environment.MachineName == "ADMIN-PC" && Environment.UserName == "admin")
                RadCaptcha1.Visible = false;
        }

        protected void btnLogin_OnClick(object sender, EventArgs e)
        {
            
            if (RadCaptcha1.Visible && !RadCaptcha1.IsValid) return;

            var userRepo = new UserRepository();

            try
            {
                var user = userRepo.GetByUserPass(txtUser.Text, txtPassword.Text);
                if (user == null)
                    lblError.InnerText = "نام کاربری یا رمز عبور صحیح نیست";
                else
                {
                    Session["Username"] = user;
                    Response.Redirect("~/AdminPanelMain.aspx");
                }

            }
            catch (LocalException localException)
            {
                lblError.InnerText = localException.ResultMessage;
            }
        }
    }
}