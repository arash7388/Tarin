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
    public partial class PaymentsReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindDrpCustomer();
                Session["Result"] = new PaymentRepository().GetAllForReport();
            }

            BindGrid();
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
            if (dtFrom.Date != "") filters.Add(new Filter("PaymentDate", OperationType.GreaterThanOrEqual, dtFrom.Date.ToEnDate()));
            if (dtTo.Date != "") filters.Add(new Filter("PaymentDate", OperationType.LessThanOrEqual, dtTo.Date.ToEnDate().AddDays(1).AddSeconds(-1)));

            var whereClause = filters.Count > 0 ? ExpressionBuilder.GetExpression<PaymentHelper>(filters) : null;

            Session["Result"] = new PaymentRepository().GetAllForReport(whereClause);
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

        protected void btnExportToExcel_OnClick(object sender, ImageClickEventArgs e)
        {
            RadGridReport.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            RadGridReport.ExportSettings.IgnorePaging = true;
            RadGridReport.ExportSettings.ExportOnlyData = true;
            RadGridReport.ExportSettings.OpenInNewWindow = true;
            RadGridReport.ExportSettings.FileName = "PaymentsReport-" + DateTime.Now.ToFaDateTime();
            RadGridReport.MasterTableView.ExportToExcel();
        }

        protected void btnExportToPdf_OnClick(object sender, ImageClickEventArgs e)
        {
            RadGridReport.ExportSettings.Pdf.Title = "گزارش مبالغ دریافتی";
            RadGridReport.ExportSettings.Pdf.DefaultFontFamily = "Arial Unicode MS";
            RadGridReport.MasterTableView.ExportToPdf();
        }
    }
}