using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using Repository.DAL;
using Telerik.Reporting;

namespace MehranPack
{
    public partial class FactorPrint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.RouteData.Values["Id"].ToSafeString() != "")
            {
                Report report = new ReportFactor();

                // Assigning the ObjectDataSource component to the DataSource property of the report.
                report.DataSource = new FactorRepository().GetFactorForPrint(Page.RouteData.Values["Id"].ToSafeInt());
                
                // Use the InstanceReportSource to pass the report to the viewer for displaying
                InstanceReportSource reportSource = new InstanceReportSource();
                reportSource.ReportDocument = report;
                
                //Assigning the report to the report viewer.
                ReportViewer1.ReportSource = reportSource;

                //Calling the RefreshReport method (only in WinForms applications).
                ReportViewer1.RefreshReport();

                //Telerik.Reporting.UriReportSource uriReportSource = new Telerik.Reporting.UriReportSource();

                //// Specifying an URL or a file path
                //uriReportSource.Uri = "~/Reports/ord.trdx";

                //// Adding the initial parameter values
                //uriReportSource.Parameters.Add(new Telerik.Reporting.Parameter("Id", "1"));
                //ReportViewer1.ReportSource = uriReportSource;
                //ReportViewer1.RefreshReport();
            }
        }
    }
}