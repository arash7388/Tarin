using System;
using System.Linq;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using Repository.DAL;

namespace MehranPack
{
    public partial class ReworkList : System.Web.UI.Page
    {
        private bool IsEsghatMode()
        {
            return Page.RouteData.Values["Mode"].ToSafeString().ToLower() == "es";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(IsEsghatMode())
            {
                h3Title.InnerText = "لیست اسقاط ها";

                if (!Page.IsPostBack)
                {
                    Session["Result"] = gridList.DataSource = new EsghatRepository().GetAllWithRelations().OrderByDescending(a => a.Id).ToList();
                    gridList.DataBind();
                }
            }
            else if (!Page.IsPostBack)
            {
                 Session["Result"] = gridList.DataSource = new ReworkRepository().GetAllWithRelations().OrderByDescending(a => a.Id).ToList();
                gridList.DataBind();
            }
        }

        protected void gridList_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            var routeValues = new RouteValueDictionary();
            routeValues.Add("Id", e.CommandArgument);

            if (e.CommandName == "Edit")
            {
                if (IsEsghatMode())
                    Response.RedirectToRoute("Esghat", routeValues);
                else
                    Response.RedirectToRoute("Rework", routeValues);

                Response.End();
            }
            else if (e.CommandName == "Delete")
            {
                var data =new ConfirmData();

                data.Command = "Delete";
                data.Id = e.CommandArgument.ToSafeInt();
                data.Msg = "آیا از حذف اطمینان دارید؟";
                data.Table = IsEsghatMode() ? "Esghats" : "Reworks";
                data.RedirectAdr = IsEsghatMode() ? "EsghatList" : "ReworkList";

                Session["ConfirmData"] = data;
                Response.RedirectToRoute("Confirmation");
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

            if (IsEsghatMode())
                routeValues.Add("Mode", "es");

            if (IsEsghatMode())
                Response.RedirectToRoute("Esghat", routeValues);
            else 
                Response.RedirectToRoute("Rework",routeValues);
        }
    }
}