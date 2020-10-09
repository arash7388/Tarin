using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using Repository.DAL;
using Telerik.Web.UI;

namespace MehranPack
{
    public partial class FactorsReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindDrpCustomer();
                Session["Result"] = new FactorRepository().GetAllForReport();
            }

            BindGrid();
        }

        private void BindDrpCustomer()
        {
            var customersSource = new CustomerRepository().GetAll().OrderBy(a => a.Name).ToList();

            var emptyCustomer = new Repository.Entity.Domain.Customer();
            emptyCustomer.Id = -1;
            emptyCustomer.Name = "انتخاب کنید...";
            customersSource.Insert(0, emptyCustomer);
            drpCustomer.DataSource = customersSource;
            drpCustomer.DataValueField = "Id";
            drpCustomer.DataTextField = "Name";
            drpCustomer.DataBind();
        }

        private void BindGrid()
        {
            RadGridReport.DataSource = Session["Result"];
            RadGridReport.DataBind();
        }

        protected void btnRun_OnClick(object sender, EventArgs e)
        {
            List<Filter> filters = new List<Filter>();
            var selectedCust = drpCustomer.SelectedValue.ToSafeInt();
            if (selectedCust != -1 && selectedCust != 0) filters.Add(new Filter("CustomerId", OperationType.Equals, drpCustomer.SelectedValue.ToSafeInt()));
            if (dtFrom.Date != "") filters.Add(new Filter("InsertDateTime", OperationType.GreaterThanOrEqual, dtFrom.Date.ToEnDate()));
            if (dtTo.Date != "") filters.Add(new Filter("InsertDateTime", OperationType.LessThanOrEqual, dtTo.Date.ToEnDate().AddDays(1).AddSeconds(-1)));

            var whereClause = filters.Count > 0 ? ExpressionBuilder.GetExpression<FactorHelper>(filters) : null;

            Session["Result"] = new FactorRepository().GetAllForReport(whereClause);
            BindGrid();
        }

        protected void btnExportToExcel_OnClick(object sender, ImageClickEventArgs e)
        {
            RadGridReport.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            RadGridReport.ExportSettings.IgnorePaging = true;
            RadGridReport.ExportSettings.ExportOnlyData = true;
            RadGridReport.ExportSettings.OpenInNewWindow = true;
            RadGridReport.ExportSettings.FileName = "FactorsReport-" + DateTime.Now.ToFaDateTime();
            RadGridReport.MasterTableView.ExportToExcel();
        }

        protected void btnExportToPdf_OnClick(object sender, ImageClickEventArgs e)
        {
            RadGridReport.ExportSettings.Pdf.Title = "گزارش فاکتورها";
            RadGridReport.ExportSettings.Pdf.DefaultFontFamily = "Arial Unicode MS";
            RadGridReport.MasterTableView.ExportToPdf();
        }

        protected void RadGridReport_OnItemCreated(object sender, GridItemEventArgs e)
        {
            //if (e.Item is GridDataItem)
            //{
            //    GridDataItem item = (GridDataItem)e.Item;
            //    foreach (TableCell cell in item.Cells)
            //    {
            //        cell.Style["font-family"] = "tahoma";
            //        cell.Style["text-align"] = "center";
            //        //cell.Style["font-size"] = (4 + e.Item.ItemIndex * 0.8) + "pt";

            //    }
            //}
            //else if (e.Item is GridHeaderItem)
            //{
            //    GridHeaderItem item = (GridHeaderItem)e.Item;
            //    foreach (TableCell cell in item.Cells)
            //    {
            //        cell.Style["font-family"] = "tahoma";
            //        cell.Style["text-align"] = "center";
            //        //cell.Style["font-size"] = (4 + e.Item.ItemIndex * 0.8) + "pt";
            //    }
            //}
        }
    }
}