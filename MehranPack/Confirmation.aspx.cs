using System;
using System.Data.SqlClient;
using System.Web.Routing;
using Common;
using Energy;
using Repository.DAL;

namespace MehranPack
{
    public partial class Confirmation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["ConfirmData"] == null)
                {
                    Response.RedirectToRoute("Home");
                    Response.End();
                }

                lblMessage.Text = ((ConfirmData)Session["ConfirmData"]).Msg;
            }
            else
            {
                confirmArea.Visible = false;
            }

        }

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            var data = ((ConfirmData)Session["ConfirmData"]);
            var id = data.Id;

            var table = data.Table;

            try
            {
                Repository.DAL.ActionResult result = null;

                if (string.IsNullOrEmpty(data.RawCommand))
                {
                    result = new UnitOfWork().ExecCommand("Delete from " + table + " Where id = @Id",
                      new SqlParameter("@Id", id));
                }
                else
                    result = new UnitOfWork().ExecCommand(data.RawCommand);

                if (!result.IsSuccess) throw new LocalException("Error in Deleting from " + table + " with id " + id, "خطا در حذف");
                ((Main)Page.Master).SetGeneralMessage("عملیات با موفقیت انجام شد", MessageType.Success);


                Session["PostProcessMessage"] = new PostProcessMessage()
                {
                    Message = "عملیات با موفقیت انجام شد",
                    MessageType = MessageType.Success
                };

                if (data.RedirectAdr.ToSafeString() != "")
                    if (data.RedirectAdr.ToLower().Contains(".aspx"))
                        Response.Redirect(data.RedirectAdr);
                    else
                        Response.RedirectToRoute(data.RedirectAdr, data.RedirectRouteValueDictionary);
            }
            catch (LocalException exception)
            {
                ((Main)Page.Master).SetGeneralMessage(exception.ResultMessage, MessageType.Error);
            }
            catch (Exception ex)
            {
                ((Main)Page.Master).SetGeneralMessage("به علت بروز مشکل درخواست شما انجام نشد-" + ex.Message, MessageType.Error);
            }
        }

        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
            var data = ((ConfirmData)Session["ConfirmData"]);
            Response.RedirectToRoute(data.RedirectAdr, data.RedirectRouteValueDictionary);
        }
    }

    public class ConfirmData
    {
        public string Command { get; set; }
        public string RawCommand { get; set; }
        public int Id { get; set; }
        public string Msg { get; set; }
        public string Table { get; set; }
        public string RedirectAdr { get; set; }
        public RouteValueDictionary RedirectRouteValueDictionary { get; set; }
        public string RedirectRoute { get; internal set; }
    }

    public class PostProcessMessage
    {
        public string Message { get; set; }
        public MessageType MessageType { get; set; }
    }
}