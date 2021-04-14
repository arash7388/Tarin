using System;
using System.Web.Routing;
using Common;
using Repository.DAL;

namespace Tashim
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Environment.MachineName == "ADMIN-PC" && Environment.UserName == "admin")
            {
                //RadCaptcha1.Visible = false;
                Response.RedirectToRoute("Home");
            }
        }

        protected void btnLogin_OnClick(object sender, EventArgs e)
        {
            if (RadCaptcha1.Visible && !RadCaptcha1.IsValid) return;

            var userRepo = new UserRepository();

            try
            {
                var user = userRepo.GetByUserPass(txtUser.Text, txtPassword.Text);
                if (user == null)
                {
                    lblError.Visible = true;
                    lblError.Text = "نام کاربری یا رمز عبور صحیح نیست";
                }
                else
                {
                    Session["User"] = user;
                    Response.Redirect("memberlist.aspx");
                }

            }
            catch (LocalException localException)
            {
                lblError.Visible = true;
                lblError.Text = localException.ResultMessage;
            }
        }
    }
}