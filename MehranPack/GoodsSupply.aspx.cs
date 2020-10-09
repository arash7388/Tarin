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
    public partial class GoodsSupply : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                
                BindDrpProducts();
                Session["Result"] = null;
                h3Header.InnerText = "گزارش موجودی کالا";
            }

            RadGridReport.ClientSettings.AllowDragToGroup = true;
            BindGrid();
        }

        private void BindDrpProducts()
        {
            var products = new ProductRepository().GetAllWithCatRecursive(true);
            products.Insert(0,new ProductHelper() {Id = -1, Name = "انتخاب کنید..."});
            drpProducts.DataSource = products;
            drpProducts.DataValueField = "Id";
            drpProducts.DataTextField = "Name";
            drpProducts.DataBind();
        }
       

        private void BindGrid()
        {
            RadGridReport.DataSource = Session["Result"];
            RadGridReport.DataBind();
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
            var selectedProduct = drpProducts.SelectedValue.ToSafeInt();
            
            if (selectedProduct != -1 && selectedProduct != 0) filters.Add(new Filter("Id", OperationType.Equals, drpProducts.SelectedValue.ToSafeInt()));
            
            var whereClause = filters.Count> 0 ? ExpressionBuilder.GetExpression<ProductSupplyHelper>(filters):null;
            
            Session["Result"] = new ProductRepository().GetProductSupply(whereClause);
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