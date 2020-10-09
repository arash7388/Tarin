using Common;
using Repository.DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace MehranPack
{
    public partial class ManualWorkLine : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string AddRow(string input)
        {
            //WID,OperatorID,ProcessID
            var parts = input.Split(',');

            var worksheetId = parts[0].ToSafeInt();
            var operatorId = parts[1].ToSafeInt();
            var processId = parts[2].ToSafeInt();

            var workLinerepo = new WorkLineRepository();
            var thisWorksheetWorkLines = workLinerepo.Get(a => a.WorksheetId == worksheetId);
                       
            var prevProcessOfThisWorksheet = thisWorksheetWorkLines.Any() ? thisWorksheetWorkLines.Max(a=>a.ProcessId) : 0;

            if (thisWorksheetWorkLines != null && thisWorksheetWorkLines.Any())
                if (prevProcessOfThisWorksheet != 0 && prevProcessOfThisWorksheet >= processId)
                    return "عدم رعایت ترتیب فرآیند";

            if (HttpContext.Current.Session["worksheetProcesses" + "#" + worksheetId] == null)
            {
                var wsheetProcesses = new WorksheetRepository().GetWorksheetProcesses(worksheetId);
                if (wsheetProcesses == null)
                    return "کاربرگ ردیف ندارد";

                HttpContext.Current.Session["worksheetProcesses" + "#" + worksheetId] = wsheetProcesses;
            }

            var thisWorksheetProcesses = (List<int>)HttpContext.Current.Session["worksheetProcesses" + "#" + worksheetId];
            var indexOfPrevProcess = thisWorksheetProcesses.IndexOf(prevProcessOfThisWorksheet);
            var indexOfNextProcess = indexOfPrevProcess + 1;
            var nextProcessOfThisWorksheet = thisWorksheetProcesses[indexOfNextProcess];

            if(processId != nextProcessOfThisWorksheet)
                return "عدم رعایت ترتیب فرآیند"; 

            //if (prevOrder == 0 && order  != 1)
            //    return "عدم رعایت ترتیب فرآیند";

            //var repo = new WorkLineRepository();
            //if (repo.Get(a => a.WorksheetId == worksheetId && a.OperatorId == operatorId
            //    && a.ProductId == productId).Any())
            //{
            //    return "ردیف تکراری";
            //}

            var uow = new UnitOfWork();
            uow.WorkLines.Create(new Repository.Entity.Domain.WorkLine()
            {
                InsertDateTime = DateTime.Now,
                WorksheetId = worksheetId,
                OperatorId = operatorId,
                //ProductId = productId,
                ProcessId = processId
            }
            );

            var result = uow.SaveChanges();
            if (result.IsSuccess)
            {
                HttpContext.Current.Session[worksheetId + "#" + operatorId] = processId;
                return "OK";
            }
            else
            {
                //((Main)Page.Master).SetGeneralMessage("خطا در ذخیره اطلاعات", MessageType.Error);
                Debuging.Error(result.ResultCode + "," + result.Message + "," + result.Message);
                return "خطا در اضافه کردن ردیف";
            }
        }

        protected void gridWorkLine_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                //Response.Redirect("Category.aspx?Id=" + e.CommandArgument);
            }
            else
            if (e.CommandName == "Delete")
            {
                var data = new ConfirmData();

                data.Command = "Delete";
                data.Id = e.CommandArgument.ToSafeInt();
                data.Msg = "آیا از حذف اطمینان دارید؟";
                data.Table = "Worklines";
                data.RedirectAdr = "Workline.aspx";

                Session["ConfirmData"] = data;
                Response.RedirectToRoute("Confirmation");
                Response.End();
            }
        }

        public DataTable SessionDataSource
        {
            get
            {
                string sessionKey = "SessionDataSource";

                if (Session[sessionKey] == null || !IsPostBack)
                {
                    Session[sessionKey] = OrdersTable();
                }
                return (DataTable)Session[sessionKey];
            }
        }

        // READ (data binding)
        protected void RadGrid1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            RadGrid1.DataSource = SessionDataSource;
        }

        // CREATE (Add New Record)
        protected void RadGrid1_InsertCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;

            DataRow newRow = SessionDataSource.NewRow();

            //As this example demonstrates only in-memory editing, a new primary key value should be generated
            //This should not be applied when updating directly the database
            DataRow[] allValues = SessionDataSource.Select("OrderID = MAX(OrderID)");

            if (allValues.Length > 0)
            {
                newRow["OrderID"] = int.Parse(allValues[0]["OrderID"].ToString()) + 1;
            }
            else
            {
                newRow["OrderID"] = 1; //the table is empty;
            }

            //Set new values
            Hashtable newValues = new Hashtable();
            //The GridTableView will fill the values from all editable columns in the hash
            e.Item.OwnerTableView.ExtractValuesFromItem(newValues, editedItem);

            try
            {
                foreach (DictionaryEntry entry in newValues)
                {
                    newRow[(string)entry.Key] = entry.Value;
                }
            }
            catch (Exception ex)
            {
                //Label1.Text += string.Format("<br />Unable to insert into Orders. Reason: {0}", ex.Message);
                e.Canceled = true;
                return;
            }

            SessionDataSource.Rows.Add(newRow);
            //Code for updating the database ca go here...
            //Label1.Text += string.Format("<br />Order {0} inserted", newRow["OrderID"]);
        }

        // UPDATE
        protected void RadGrid1_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;

            if (!UpdateRow(editedItem))
            {
                e.Canceled = true;
            }
        }
        private bool UpdateRow(GridEditableItem editableItem)
        {
            //Locate the changed row in the DataSource
            DataRow[] changedRows = SessionDataSource.Select(string.Format("OrderID = {0}", editableItem.GetDataKeyValue("OrderID")));

            if (changedRows.Length != 1)
            {
                //this.Label1.Text += "Unable to locate the Order for updating.";
                return false;
            }

            //Update new values
            Hashtable newValues = new Hashtable();
            editableItem.OwnerTableView.ExtractValuesFromItem(newValues, editableItem);
            changedRows[0].BeginEdit();
            try
            {
                foreach (DictionaryEntry entry in newValues)
                {
                    changedRows[0][(string)entry.Key] = entry.Value;
                }
                changedRows[0].EndEdit();
            }
            catch (Exception ex)
            {
                changedRows[0].CancelEdit();
                //Label1.Text += string.Format("Unable to update Orders. Reason: {0}", ex.Message);
                return false;
            }

            return true;
        }

        // DELETE
        protected void RadGrid1_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            GridDataItem dataItem = e.Item as GridDataItem;
            string ID = dataItem.GetDataKeyValue("OrderID").ToString();

            if (SessionDataSource.Rows.Find(ID) != null)
            {
                SessionDataSource.Rows.Find(ID).Delete();
            }
        }

        private DataTable OrdersTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("OrderID", typeof(int)));
            dt.Columns.Add(new DataColumn("OrderDate", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("Freight", typeof(decimal)));
            dt.Columns.Add(new DataColumn("ShipName", typeof(string)));
            dt.Columns.Add(new DataColumn("ShipCountry", typeof(string)));
            dt.Columns.Add(new DataColumn("IsChecked", typeof(bool)));

            dt.PrimaryKey = new DataColumn[] { dt.Columns["OrderID"] };

            for (int i = 0; i < 10; i++)
            {
                int index = i + 1;

                DataRow row = dt.NewRow();

                row["OrderID"] = index;
                row["OrderDate"] = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0).AddHours(index);
                row["Freight"] = index * 0.1 + index * 0.01;
                row["ShipName"] = "Name " + index;
                row["ShipCountry"] = "Country " + index;
                row["IsChecked"] = i % 3 == 0;

                dt.Rows.Add(row);
            }

            return dt;
        }

        protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case RadGrid.InitInsertCommandName:
                case RadGrid.EditCommandName:

                    MakeColumnsReadOnly(false);

                    break;

                case RadGrid.CancelCommandName:
                case RadGrid.CancelAllCommandName:

                    MakeColumnsReadOnly(true);

                    break;

                case "EditColumn":

                    MakeColumnsReadOnly(true);

                    string ColumnName = e.CommandArgument.ToString();

                    if (ColumnName != "OrderID")
                        ((GridTemplateColumn)RadGrid1.MasterTableView.GetColumn(ColumnName)).ReadOnly = false;

                    foreach (GridItem item in RadGrid1.MasterTableView.Items)
                    {
                        if (item is GridEditableItem)
                        {
                            GridEditableItem editableItem = item as GridDataItem;
                            editableItem.Edit = true;
                        }
                    }
                    RadGrid1.Rebind();
                    break;
            }
        }

        private void MakeColumnsReadOnly(bool readOnly = false)
        {
            foreach (GridTemplateColumn column in RadGrid1.MasterTableView.RenderColumns.OfType<GridTemplateColumn>())
            {
                if (column.UniqueName != "OrderID")
                    column.ReadOnly = readOnly;
            }
        }

    }
}