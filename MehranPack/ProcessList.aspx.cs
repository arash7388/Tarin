using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using Repository.DAL;
using Repository.Data;
using Repository.Entity.Domain;

namespace MehranPack
{
    public partial class ProcessList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Debuging.Info("ProcessList Page_Load");
            if (!Page.IsPostBack)
            {
                 Session["Result"] = gridList.DataSource = new ProcessRepository().GetAll().OrderBy(a => a.Id).ToList();
                gridList.DataBind();
            }
        }

        protected void gridList_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            var routeValues = new RouteValueDictionary();
            routeValues.Add("Id", e.CommandArgument);

            if (e.CommandName == "Edit")
            {
                Response.RedirectToRoute("Process", routeValues);
                Response.End();
            }
            else if (e.CommandName == "Delete")
            {
                if (e.CommandArgument.ToSafeInt() == 99 || e.CommandArgument.ToSafeInt() == 999)
                {
                    Session["PostProcessMessage"] = new PostProcessMessage()
                    {
                        Message = "این فرآیند سیستمی است و قابل حذف نیست",
                        MessageType = MessageType.Error
                    };

                    Response.RedirectToRoute("ProcessList");
                }
                else
                {
                    var data = new ConfirmData();

                    data.Command = "Delete";
                    data.Id = e.CommandArgument.ToSafeInt();
                    data.Msg = "آیا از حذف اطمینان دارید؟";
                    data.Table = "Processes";
                    data.RedirectAdr = "ProcessList";

                    Session["ConfirmData"] = data;
                    Response.RedirectToRoute("Confirmation");
                    Response.End();
                }
               
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
            Response.RedirectToRoute("Process",routeValues);
        }
    }
}