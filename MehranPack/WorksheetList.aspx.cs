using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using Repository.DAL;
using ActionResult = Common.ActionResult;

namespace MehranPack
{
    public partial class WorksheetList : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                Session["Result"] = gridList.DataSource = new WorksheetRepository().GetAll().OrderByDescending(a => a.Id).ToList();
                h3Header.InnerText = "لیست کاربرگ ها";

                gridList.DataBind();

                //after saving a io user should see factor list.
                if (Page.RouteData.Values["ActionResult"].ToSafeBool())
                {
                    var saveResult = (ActionResult)Session["SaveIOActionResult"];

                    if (saveResult != null)
                        ((Main)(Page.Master)).SetGeneralMessage(saveResult.ResultMessage, MessageType.Success);
                }

            }
        }

        public string GetOperatorName(int opId)
        {
            return new UserRepository().GetById(opId)?.FriendlyName;
        }

        protected void gridList_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            var routeValues = new RouteValueDictionary();
            routeValues.Add("Id", e.CommandArgument);

            if (e.CommandName == "Edit")
            {
                Response.RedirectToRoute("Worksheet", routeValues);
                Response.End();
            }
            else if (e.CommandName == "Delete")
            {
                DoDelete(e);
            }
            else if (e.CommandName == "Print")
            {
                Response.Redirect($"WorksheetPrint.aspx?id={e.CommandArgument.ToSafeInt()}");
            }
        }

        private void DoDelete(GridViewCommandEventArgs e)
        {
            var data = new ConfirmData();

            data.Command = "Delete";
            data.Id = e.CommandArgument.ToSafeInt();
            data.Msg = "آیا از حذف اطمینان دارید؟";
            data.Table = "Worksheets";
            data.RedirectAdr = "WorksheetList";
            data.RedirectRouteValueDictionary = Page.RouteData.Values;
            Session["ConfirmData"] = data;
            Response.RedirectToRoute("Confirmation");
            Response.End();
        }

        protected void gridList_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridList.PageIndex = e.NewPageIndex;
            gridList.DataSource = Session["Result"];
            gridList.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            var routeValues = new RouteValueDictionary();
            routeValues.Add("Id", 0);
            Response.RedirectToRoute("Worksheet", routeValues);
        }
    }
}