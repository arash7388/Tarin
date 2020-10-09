using System;
using System.Data.SqlClient;
using System.Web.Routing;
using Common;
using Repository.DAL;

namespace Inv
{
    public partial class Confirmation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                if (Session["ConfirmData"] == null)
                {
                    Response.RedirectToRoute("Home");
                    Response.End();
                }

                lblMessage.Text = ((ConfirmData) Session["ConfirmData"]).Msg;
            }
            else
            {
                confirmArea.Visible = false;
            }
            
        }

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            var data = ((ConfirmData) Session["ConfirmData"]);
            var id = data.Id;

            var table = data.Table;
            
            try
            {
                var result = new UnitOfWork().ExecCommand("Delete from " + table + " Where id = @Id",
                    new SqlParameter("@Id", id));

                if (!result.IsSuccess) throw new LocalException("Error in Deleting from " + table + " with id " + id, "خطا در حذف");
                 ((SiteMaster)Page.Master).SetGeneralMessage("عملیات با موفقیت انجام شد",MessageType.Success);
            }
            catch (LocalException exception)
            {
                ((SiteMaster)Page.Master).SetGeneralMessage(exception.ResultMessage,MessageType.Error);
            }
            catch (Exception ex)
            {
                ((SiteMaster)Page.Master).SetGeneralMessage("به علت بروز مشکل درخواست شما انجام نشد-" + ex.Message, MessageType.Error);
            }
        }

        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
            var data = ((ConfirmData)Session["ConfirmData"]);
            Response.RedirectToRoute(data.RedirectRoute);
        }
    }

    public class ConfirmData
    {
        public string Command { get; set; }
        public int Id { get; set; }
        public string Msg { get; set; }
        public string Table { get; set; }
        public string RedirectRoute { get; set; }
        public RouteValueDictionary RedirectRouteValueDictionary { get; set; }
    }
}