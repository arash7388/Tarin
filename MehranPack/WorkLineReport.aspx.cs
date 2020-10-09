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
    public partial class WorkLineReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindDrpOp();

                List<Filter> filters = new List<Filter>();
                filters.Add(new Filter("InsertDateTime", OperationType.GreaterThanOrEqual, DateTime.Now.AddDays(-7)));
                filters.Add(new Filter("InsertDateTime", OperationType.LessThanOrEqual, DateTime.Now));

                var whereClause = ExpressionBuilder.GetExpression<WorkLineHelper>(filters);

                Session["Result"] = new WorkLineRepository().GetAllForReport(whereClause);
            }

            BindGrid();
        }

        private void BindDrpOp()
        {
            var opSource = new UserRepository().GetAll().OrderBy(a => a.FriendlyName).ToList();

            var emptyUser = new Repository.Entity.Domain.User();
            emptyUser.Id = -1;
            emptyUser.FriendlyName = "انتخاب کنید...";
            opSource.Insert(0, emptyUser);
            drpOperator.DataSource = opSource;
            drpOperator.DataValueField = "Id";
            drpOperator.DataTextField = "FriendlyName";
            drpOperator.DataBind();
        }

        private void BindGrid()
        {
            RadGridReport.DataSource = Session["Result"];
            RadGridReport.DataBind();
        }

        protected void btnRun_OnClick(object sender, EventArgs e)
        {
            List<Filter> filters = new List<Filter>();
            var selectedOp = drpOperator.SelectedValue.ToSafeInt();
            if (selectedOp != -1 && selectedOp != 0) filters.Add(new Filter("OperatorId", OperationType.Equals, drpOperator.SelectedValue.ToSafeInt()));
            if (dtFrom.Date != "") filters.Add(new Filter("InsertDateTime", OperationType.GreaterThanOrEqual, dtFrom.Date.ToEnDate()));
            if (dtTo.Date != "") filters.Add(new Filter("InsertDateTime", OperationType.LessThanOrEqual, dtTo.Date.ToEnDate().AddDays(1).AddSeconds(-1)));

            var whereClause = filters.Count > 0 ? ExpressionBuilder.GetExpression<WorkLineHelper>(filters) : null;

            Session["Result"] = new WorkLineRepository().GetAllForReport(whereClause);
            BindGrid();
        }

        protected void btnExportToExcel_OnClick(object sender, ImageClickEventArgs e)
        {
            RadGridReport.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            RadGridReport.ExportSettings.IgnorePaging = true;
            RadGridReport.ExportSettings.ExportOnlyData = true;
            RadGridReport.ExportSettings.OpenInNewWindow = true;
            RadGridReport.ExportSettings.FileName = "WorkLineReport-" + DateTime.Now.ToFaDateTime();
            RadGridReport.MasterTableView.ExportToExcel();
        }

        protected void btnExportToPdf_OnClick(object sender, ImageClickEventArgs e)
        {
            RadGridReport.ExportSettings.Pdf.Title = "گزارش کارهای تولیدی";
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