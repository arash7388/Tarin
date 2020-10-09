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

namespace MehranPack
{
    public partial class CustomerList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Debuging.Info("CustomerList Page_Load");
            if (!Page.IsPostBack)
            {
                 Session["Result"] = gridList.DataSource = new CustomerRepository().GetAll().OrderBy(a => a.Name).ToList();
                gridList.DataBind();
            }
        }

        protected void gridList_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            var routeValues = new RouteValueDictionary();
            routeValues.Add("Id", e.CommandArgument);

            if (e.CommandName == "Edit")
            {
                Response.RedirectToRoute("Customer", routeValues);
                Response.End();
            }
            else if (e.CommandName == "Delete")
            {
                //var msg = Utility.AesEncrypt("آیا از حذف مشتری اطمینان دارید؟");
                //var table = Utility.AesEncrypt("Customers");
                //routeValues.Add("Msg", msg);
                //routeValues.Add("Table", table);
                var data =new ConfirmData();

                data.Command = "Delete";
                data.Id = e.CommandArgument.ToSafeInt();
                data.Msg = "آیا از حذف مشتری اطمینان دارید؟";
                data.Table = "Customers";
                data.RedirectAdr = "CustomerList";

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
            Response.RedirectToRoute("Customer",routeValues);
        }
    }
}