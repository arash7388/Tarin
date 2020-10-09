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
    public partial class OrdersReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var customers = new CustomerRepository().GetAll().OrderBy(a=>a.Name).ToList();

                var emptyCustomer = new Repository.Entity.Domain.Customer();
                emptyCustomer.Id = -1;
                emptyCustomer.Name = "انتخاب کنید...";
                
                customers.Insert(0,emptyCustomer);
                drpCustomer.DataSource = customers;
                drpCustomer.DataValueField = "Id";
                drpCustomer.DataTextField = "Name";
                drpCustomer.DataBind();

                if (Page.RouteData.Values["Type"].ToSafeString() == "In")
                {
                    List<Filter> filters = new List<Filter>();
                    filters.Add(new Filter("InOutType", OperationType.Equals, (int)InOutType.In));
                    var whereClause = ExpressionBuilder.GetExpression<InputOutputDetailHelper>(filters);

                    Session["Result"] = new InputOutputDetailRepository().GetByFilter(whereClause);
                    h3Header.InnerText = "گزارش ورود کالا";

                    RadGridReport.MasterTableView.GetColumn("columnRInId").Visible = false;
                }
                else
                {
                    List<Filter> filters = new List<Filter>();
                    filters.Add(new Filter("InOutType", OperationType.Equals, (int)InOutType.Out));
                    var whereClause = ExpressionBuilder.GetExpression<InputOutputDetailHelper>(filters);

                    Session["Result"] = new InputOutputDetailRepository().GetByFilter(whereClause);
                    h3Header.InnerText = "گزارش خروج کالا";
                }
            }

            BindGrid();
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
            var IOType = (int)Page.RouteData.Values["Type"].ToSafeString().ToEnum<InOutType>();

            filters.Add(new Filter("InOutType", OperationType.Equals, IOType));
            if (selectedCust != -1 && selectedCust!=0) filters.Add(new Filter("CustomerId", OperationType.Equals, drpCustomer.SelectedValue.ToSafeInt()));
            if (dtFrom.Date != "") filters.Add(new Filter("InsertDateTimeDetailEn", OperationType.GreaterThanOrEqual,dtFrom.Date.ToEnDate() ));
            if (dtTo.Date != "") filters.Add(new Filter("InsertDateTimeDetailEn", OperationType.LessThanOrEqual,dtTo.Date.ToEnDate().AddDays(1).AddSeconds(-1) ));
            
            var whereClause = filters.Count>0 ? ExpressionBuilder.GetExpression<InputOutputDetailHelper>(filters):null;
            
            Session["Result"] = new InputOutputDetailRepository().GetByFilter(whereClause);
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