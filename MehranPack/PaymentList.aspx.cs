using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using Repository.DAL;
using Repository.Entity.Domain;

namespace MehranPack
{
    public partial class PaymentList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Environment.MachineName.ToLower() == "arash-laptop")
                Session["User"] = new BaseRepository<User>().GetById(1);

            if (!Page.IsPostBack)
            {
                var factorRepo = new PaymentRepository();
                gridList.DataSource = Session["Result"] = factorRepo.GetAllPayments();
                gridList.DataBind();

                //after saving a payment user should see payment list.
                if (Page.RouteData.Values["ActionResult"].ToSafeBool())
                {
                    var saveResult = (ActionResult)Session["SavePaymentActionResult"];

                    if (saveResult != null)
                        ((Main)(Page.Master)).SetGeneralMessage(saveResult.ResultMessage, MessageType.Success);
                }
            }
        }

        protected string GetCustomerName(int id)
        {
            var repo = new CustomerRepository();
            var cust = repo.GetById(id);
            if (cust != null)
                return cust.Name;

            return "";
        }

        protected void gridList_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            var routeValues = new RouteValueDictionary();
            routeValues.Add("Id", e.CommandArgument);

            if (e.CommandName == "Edit")
            {
                Response.RedirectToRoute("Payment", routeValues);
                Response.End();
            }
            else if (e.CommandName == "Delete")
            {
                var data = new ConfirmData();

                data.Command = "Delete";
                data.Id = e.CommandArgument.ToSafeInt();
                data.Msg = "آیا از حذف دریافت اطمینان دارید؟";
                data.Table = "Payments";
                data.RedirectRoute = "PaymentList";

                Session["ConfirmData"] = data;
                Response.RedirectToRoute("Confirmation");
                Response.End();
            }
            else if (e.CommandName == "Print")
            {
                Response.RedirectToRoute("PaymentPrint", routeValues);
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
            Response.RedirectToRoute("Payment", routeValues);
        }

        protected void gridList_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection nextSortDir;

            if (Session["SortExp"] == null)
                Session["SortExp"] = "PaymentNo-DESC";

            if (e.SortExpression == Session["SortExp"].ToString().Split('-')[0]) //just switch dir
                nextSortDir = Session["SortExp"].ToString().Split('-')[1] == "ASC"
                    ? SortDirection.Descending
                    : SortDirection.Ascending;
            else
                nextSortDir = e.SortDirection;

            var source = (List<Repository.Entity.Domain.Payment>)Session["Result"];

            if (nextSortDir == SortDirection.Ascending)
                switch (e.SortExpression)
                {
                    case "PaymentNo":
                        gridList.DataSource = Session["Result"] = source.OrderBy(a => a.PaymentNo).ToList();
                        Session["SortExp"] = "PaymentNo-ASC";
                        break;

                    case "FactorNo":
                        gridList.DataSource = Session["Result"] = (from s in source
                                                                   join f in new FactorRepository().GetAll()
                                                                   on s.FactorId equals f.Id
                                                                   orderby f.FactorNo
                                                                   select s).ToList();
                        Session["SortExp"] = "FactorNo-ASC";
                        break;

                    case "CustomerName":
                        gridList.DataSource = Session["Result"] = (from s in source
                                                                   join f in new FactorRepository().GetAll()
                                                                   on s.FactorId equals f.Id
                                                                   join c in new CustomerRepository().GetAll()
                                                                       on f.CustomerId equals c.Id
                                                                   orderby c.Name
                                                                   select s).ToList();
                        Session["SortExp"] = "CustomerName-ASC";
                        break;
                }
            else
            {
                switch (e.SortExpression)
                {
                    case "PaymentNo":
                        gridList.DataSource = Session["Result"] = source.OrderByDescending(a => a.PaymentNo).ToList();
                        Session["SortExp"] = "PaymentNo-DESC";
                        break;

                    case "FactorNo":
                        gridList.DataSource = Session["Result"] = (from s in source
                                                                   join f in new FactorRepository().GetAll()
                                                                   on s.FactorId equals f.Id
                                                                   orderby f.FactorNo descending 
                                                                   select s).ToList();
                        Session["SortExp"] = "FactorNo-DESC";
                        break;

                    case "CustomerName":
                        gridList.DataSource = Session["Result"] = (from s in source
                                                                   join f in new FactorRepository().GetAll()
                                                                   on s.FactorId equals f.Id
                                                                   join c in new CustomerRepository().GetAll()
                                                                       on f.CustomerId equals c.Id
                                                                   orderby c.Name descending 
                                                                   select s).ToList();
                        Session["SortExp"] = "CustomerName-DESC";
                        break;
                }
            }

            gridList.DataBind();
        }
        
    }
}