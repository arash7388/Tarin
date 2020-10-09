using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using Repository.DAL;
using Repository.Entity.Domain;
using Telerik.Reporting;
using SortDirection = System.Web.UI.WebControls.SortDirection;

namespace MehranPack
{
    public partial class OrderList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                gridList.DataSource = Session["Result"] = new OrderRepository().GetAllByOrder();
                gridList.DataBind();
                Session["SortExp"] = null;
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

        protected void gridList_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection nextSortDir;
            
            if (Session["SortExp"] == null)
                Session["SortExp"] = "OrderNo-DESC";

            if (e.SortExpression == Session["SortExp"].ToString().Split('-')[0]) //just switch dir
                nextSortDir = Session["SortExp"].ToString().Split('-')[1] == "ASC"
                    ? SortDirection.Descending
                    : SortDirection.Ascending;
            else
                nextSortDir = e.SortDirection;

            var source = (List<Repository.Entity.Domain.Order>)Session["Result"];

            if (nextSortDir == SortDirection.Ascending)
                switch (e.SortExpression)
                {
                    case "OrderNo":
                        gridList.DataSource = Session["Result"] = source.OrderBy(a => a.OrderNo).ToList();
                        Session["SortExp"] = "OrderNo-ASC";
                        break;

                    case "CustomerName":
                        gridList.DataSource = Session["Result"] = (from s in source
                                                                   join c in new CustomerRepository().GetAll()
                                                                       on s.CustomerId equals c.Id
                                                                   orderby c.Name
                                                                   select s).ToList();
                        Session["SortExp"] = "CustomerName-ASC";
                        break;
                }
            else
            {
                switch (e.SortExpression)
                {
                    case "OrderNo":
                        gridList.DataSource = Session["Result"] = source.OrderByDescending(a => a.OrderNo).ToList();
                        Session["SortExp"] = "OrderNo-DESC";
                        break;

                    case "CustomerName":
                        gridList.DataSource = Session["Result"] = (from s in source
                                                                   join c in new CustomerRepository().GetAll()
                                                                       on s.CustomerId equals c.Id
                                                                   orderby c.Name descending
                                                                   select s).ToList();
                        Session["SortExp"] = "CustomerName-DESC";
                        break;
                }
            }

            gridList.DataBind();
        }

        protected string GetProductTypeName(int productTypeId)
        {
            var repo = new BaseRepository<ProductType>();
            var pType = repo.GetById(productTypeId);

            if (pType != null)
                return pType.Name;

            return "";
        }

        protected void gridList_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            var routeValues = new RouteValueDictionary();
            routeValues.Add("Id", e.CommandArgument);

            if (e.CommandName == "Edit")
            {
                Response.RedirectToRoute("Order", routeValues);
                Response.End();
            }
            else if (e.CommandName == "Delete")
            {
                //var msg = Utility.AesEncrypt("آیا از حذف سفارش اطمینان دارید؟");
                //var table = Utility.AesEncrypt("Orders");
                //routeValues.Add("Msg", msg);
                //routeValues.Add("Table", table);
                //Response.RedirectToRoute("Confirmation", routeValues);
                //Response.End();

                var data = new ConfirmData();

                data.Command = "Delete";
                data.Id = e.CommandArgument.ToSafeInt();
                data.Msg = "آیا از حذف سفارش اطمینان دارید؟";
                data.Table = "Orders";
                data.RedirectRoute = "OrderList";

                Session["ConfirmData"] = data;
                Response.RedirectToRoute("Confirmation");
                Response.End();
            }
            else if (e.CommandName == "Print")
            {
                Response.RedirectToRoute("OrderPrint", routeValues);
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
            Response.RedirectToRoute("Order", routeValues);
        }

        protected void btnDelete_OnClick(object sender, EventArgs e)
        {

        }

        protected void PageDropDownList_SelectedIndexChanged(Object sender, EventArgs e)
        {

            // Retrieve the pager row.
            GridViewRow pagerRow = gridList.BottomPagerRow;

            // Retrieve the PageDropDownList DropDownList from the bottom pager row.
            DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");

            // Set the PageIndex property to display that page selected by the user.
            gridList.PageIndex = pageList.SelectedIndex;

        }

        protected void gridList_OnDataBound(object sender, EventArgs e)
        {
            // Retrieve the pager row.
            GridViewRow pagerRow = gridList.BottomPagerRow;

            // Retrieve the DropDownList and Label controls from the row.
            DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");
            Label pageLabel = (Label)pagerRow.Cells[0].FindControl("CurrentPageLabel");

            if (pageList != null)
            {

                // Create the values for the DropDownList control based on 
                // the  total number of pages required to display the data
                // source.
                for (int i = 0; i < gridList.PageCount; i++)
                {

                    // Create a ListItem object to represent a page.
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());

                    // If the ListItem object matches the currently selected
                    // page, flag the ListItem object as being selected. Because
                    // the DropDownList control is recreated each time the pager
                    // row gets created, this will persist the selected item in
                    // the DropDownList control.   
                    if (i == gridList.PageIndex)
                    {
                        item.Selected = true;
                    }

                    // Add the ListItem object to the Items collection of the 
                    // DropDownList.
                    pageList.Items.Add(item);

                }

            }

            if (pageLabel != null)
            {

                // Calculate the current page number.
                int currentPage = gridList.PageIndex + 1;

                // Update the Label control with the current page information.
                pageLabel.Text = "Page " + currentPage.ToString() +
                  " of " + gridList.PageCount.ToString();

            }



        }
    }
}