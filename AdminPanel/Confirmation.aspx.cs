using System;
using System.Data.SqlClient;
using Common;
using Repository.DAL;

namespace AdminPanel
{
    public partial class Confirmation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var msg = Utility.AesDecrypt(Request.QueryString["m"].Replace(" ", "+"));
            lblMessage.Text = msg;
        }

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            var id = Request.QueryString["Id"];
            
            var table = Utility.AesDecrypt(Request.QueryString["t"].Replace(" ", "+"));
            
            try
            {
                var result = new UnitOfWork().ExecCommand("Delete from " + table + " Where id = @Id",
                    new SqlParameter("@Id", id));

                if (!result.IsSuccess) throw new LocalException("Error in Deleting from " + table + " with id " + id, "خطا در حذف");
                lblInfo.Text = "عملیات با موفقیت انجام شد";
            }
            catch (LocalException exception)
            {
                lblInfo.Text = exception.ResultMessage;
            }
            catch (Exception ex)
            {
                lblInfo.Text = "به علت بروز مشکل درخواست شما انجام نشد";
            }

        }


    }
}