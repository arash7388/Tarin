using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using Repository.DAL;
using Telerik.Web.UI;

namespace MehranPack
{
    public partial class Cardex : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindDrpCustomers();
                BindDrpProducts();
                Session["Result"] = null;
                h3Header.InnerText = "گزارش کاردکس کالا";
            }

            BindGrid();
        }

        private void BindDrpProducts()
        {
            var products = new ProductRepository().GetAllWithCatRecursive(true);

            drpProducts.DataSource = products;
            drpProducts.DataValueField = "Id";
            drpProducts.DataTextField = "Name";
            drpProducts.DataBind();
        }

        private void BindDrpCustomers()
        {
            var customers = new CustomerRepository().GetAll().OrderBy(a => a.Name).ToList();

            var emptyCustomer = new Repository.Entity.Domain.Customer();
            emptyCustomer.Id = -1;
            emptyCustomer.Name = "انتخاب کنید...";

            customers.Insert(0, emptyCustomer);
            drpCustomer.DataSource = customers;
            drpCustomer.DataValueField = "Id";
            drpCustomer.DataTextField = "Name";
            drpCustomer.DataBind();
        }

        private void BindGrid()
        {
            RadGridReport.DataSource = Session["Result"];
            RadGridReport.DataBind();
        }

        protected string GetProductionQualityTextByValue(int value)
        {
            switch (value)
            {
                case 1: return "بد";
                case 2: return "متوسط";
                case 3: return "خوب";
                default: return "";
            }
        }

        protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.ExportToExcelCommandName)
            {
                RadGridReport.AllowPaging = false;
                RadGridReport.Rebind();
            }
        }

        protected void btnRun_OnClick(object sender, EventArgs e)
        {
            List<Filter> filters = new List<Filter>();
            var selectedCust = drpCustomer.SelectedValue.ToSafeInt();
            var selectedProduct = drpProducts.SelectedValue.ToSafeInt();
            
            if (selectedCust != -1 && selectedCust!=0) filters.Add(new Filter("CustomerId", OperationType.Equals, drpCustomer.SelectedValue.ToSafeInt()));
            if (selectedProduct != -1 && selectedProduct != 0) filters.Add(new Filter("ProductId", OperationType.Equals, drpProducts.SelectedValue.ToSafeInt()));
            if (dtFrom.Date != "") filters.Add(new Filter("InsertDateTimeDetail", OperationType.GreaterThanOrEqual,dtFrom.Date.ToEnDate() ));
            if (dtTo.Date != "") filters.Add(new Filter("InsertDateTimeDetail", OperationType.LessThanOrEqual,dtTo.Date.ToEnDate().AddDays(1).AddSeconds(-1) ));
            
            var whereClause = filters.Count> 0 ? ExpressionBuilder.GetExpression<InputOutputDetailHelper>(filters):null;
            
            Session["Result"] = new InputOutputDetailRepository().CardexReport(whereClause);
            BindGrid();
        }

        protected void btnExportToExcel_OnClick(object sender, EventArgs eventArgs)
        {
            RadGridReport.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            RadGridReport.ExportSettings.IgnorePaging = true;
            RadGridReport.ExportSettings.ExportOnlyData = true;
            RadGridReport.ExportSettings.OpenInNewWindow = true;
            RadGridReport.ExportSettings.FileName = "IOReport-" + DateTime.Now.ToFaDateTime();
            RadGridReport.MasterTableView.ExportToExcel();
        }
    }
}