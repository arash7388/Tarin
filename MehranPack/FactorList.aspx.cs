using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using Energy;
using Repository.DAL;
using Repository.Entity.Domain;

namespace MehranPack
{
    public partial class FactorList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Environment.MachineName.ToLower() == "arash-laptop")
                Session["User"] = new BaseRepository<User>().GetById(1);

            if (!Page.IsPostBack)
            {
                var factorRepo = new FactorRepository();
                gridList.DataSource = Session["Result"] = factorRepo.GetAllFactors();
                gridList.DataBind();

                //after saving a factor user should see factor list.
                if (Page.RouteData.Values["ActionResult"].ToSafeBool())
                {
                    var saveResult = (ActionResult)Session["SaveFactorActionResult"];

                    if (saveResult != null)
                        ((Main) (Page.Master)).SetGeneralMessage(saveResult.ResultMessage, MessageType.Success);
                }
            }
        }

        protected void gridList_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            var routeValues = new RouteValueDictionary();
            routeValues.Add("Id", e.CommandArgument);

            if (e.CommandName == "Edit")
            {
                Response.RedirectToRoute("Factor", routeValues);
                Response.End();
            }
            else if (e.CommandName == "Delete")
            {
                var data = new ConfirmData();

                data.Command = "Delete";
                data.Id = e.CommandArgument.ToSafeInt();
                data.Msg = "آیا از حذف فاکتور اطمینان دارید؟";
                data.Table = "Factors";
                data.RedirectRoute = "FactorList";

                Session["ConfirmData"] = data;
                Response.RedirectToRoute("Confirmation");
                Response.End();
            }
            else if (e.CommandName == "Print")
            {
                Response.RedirectToRoute("FactorPrint", routeValues);
                Response.End();
            }
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
            Response.RedirectToRoute("Factor", routeValues);
        }
    }
}