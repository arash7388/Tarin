using Common;
using Repository.DAL;
using Repository.Entity.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace MehranPack
{
    public partial class ManualWorkLineInput : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Session["Result"] = null;
        }

        public DataTable SessionDataSource
        {
            get
            {
                string sessionKey = "Result";

                if (Session[sessionKey] == null || !IsPostBack)
                {
                    Session[sessionKey] = WorklineDT();
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
            DataRow[] allValues = SessionDataSource.Select("Id = MAX(Id)");

            if (allValues.Length > 0)
            {
                newRow["Id"] = int.Parse(allValues[0]["Id"].ToString()) + 1;
            }
            else
            {
                newRow["Id"] = 1; //the table is empty;
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
            DataRow[] changedRows = SessionDataSource.Select(string.Format("Id = {0}", editableItem.GetDataKeyValue("Id")));

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
            string ID = dataItem.GetDataKeyValue("Id").ToString();

            if (SessionDataSource.Rows.Find(ID) != null)
            {
                SessionDataSource.Rows.Find(ID).Delete();
            }
        }

        private DataTable WorklineDT()
        {
            if (Session["Result"] == null)
            {
                DataTable dt = new DataTable();

                dt.Columns.Add(new DataColumn("Id", typeof(int)));
                dt.Columns.Add(new DataColumn("ProcessId", typeof(int)));
                dt.Columns.Add(new DataColumn("ProcessName", typeof(string)));
                dt.Columns.Add(new DataColumn("OperatorId", typeof(int)));
                dt.Columns.Add(new DataColumn("OperatorName", typeof(int)));
                dt.Columns.Add(new DataColumn("WorksheetId", typeof(int)));
                dt.Columns.Add(new DataColumn("InsertDateTime", typeof(DateTime)));
                dt.Columns.Add(new DataColumn("Date", typeof(string)));
                dt.Columns.Add(new DataColumn("Time", typeof(string)));
                dt.Columns.Add(new DataColumn("Hour", typeof(int)));
                dt.Columns.Add(new DataColumn("Min", typeof(int)));

                var pr = dt.Columns[0];
                var prs = new DataColumn[1];
                prs[0] = pr;
                dt.PrimaryKey = prs;

                Session["Result"] = dt;

                return dt;
            }
            else
                return (DataTable)Session["Result"];
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

                    if (ColumnName != "Id")
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
                if (column.UniqueName != "Id")
                    column.ReadOnly = readOnly;
            }
        }

        public string GetProcessName(int prId)
        {
            var repo = new ProcessRepository();
            return repo.GetById(prId)?.Name;
        }

        public string GetOpName(int pId)
        {
            var repo = new UserRepository();
            return repo.GetById(pId)?.Username;
        }

        protected void RadDropDownProcesses_ItemDataBound(object sender, DropDownListItemEventArgs e)
        {

        }

        protected void RadDropDownWorksheets_ItemDataBound(object sender, DropDownListItemEventArgs e)
        {

        }

        protected void RadDropDownWorksheets_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            var id = e.Value.ToSafeInt();

            var repo = new WorkLineRepository();

            if (repo.Get(a => a.WorksheetId == id).Any())
            {
                lblAlert.Text = "توجه:برای این کاربرگ قبلا تایمینگ وارد شده است";
                lblAlert.Visible = true;
            }
            else
            {
                lblAlert.Text = "";
                lblAlert.Visible = false;
            }

            RadGrid1.Enabled = true;

            LoadDT(id);
            BindGrid();
        }

        private void LoadDT(int wid)
        {
            WorklineDT().Rows.Clear();

            var allowedPrTimes = new WorksheetRepository().GetWorksheetAllowedPrTimes(wid);

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Anaraki"].ToString()))
            {
                var selectCommand =
                "select WID,[Date],PartNo,WaxNo,ColorName,OperatorId,OperatorName,ProcessId,ProcessName,Cast('' as char(5)) Time,Cast(0 as int) Hour,Cast(0 as int) Min from (" +
                "SELECT distinct w.Id WID, dbo.shamsidate(w.Date) as [Date] ,w.PartNo,w.WaxNo,c.Name ColorName, u.FriendlyName OperatorName," +
                "    pro.Name ProcessName," +
                "    pro.Id ProcessId," +
                "    cat.Id CatId," +
                "    u.Id OperatorId " +
                "FROM worksheets w " +
                "join Colors c on c.Id = w.ColorId " +
                "join Users u on u.Id = w.OperatorId " +
                "join WorksheetDetails d on w.Id = d.WorksheetId " +
                "join Products p on p.Id = d.ProductId " +
                "join Categories cat on cat.Id = p.ProductCategoryId " +
                "join ProcessCategories pcat on pcat.CategoryId = cat.Id " +
                "join Processes pro on pro.Id = pcat.ProcessId" +
                ") s1 " +
                "where WId = @id " +
                "group by WID,[Date],PartNo,WaxNo,ColorName,OperatorId,OperatorName,ProcessId,ProcessName " +
                "order by ProcessId";

                connection.Open();

                SqlCommand sqlCommand = new SqlCommand(selectCommand, connection);
                sqlCommand.Parameters.AddWithValue("@id", wid);

                SqlDataReader reader = sqlCommand.ExecuteReader();

                var now = DateTime.Now;

                var zeroTime = new DateTime(now.Year, now.Month, now.Day, 8, 0, 0);

                if (reader.HasRows)
                {
                    var dt = WorklineDT();
                    int i = 0;

                    while (reader.Read())
                    {
                        var newRow = dt.NewRow();

                        //dt.Columns.Add(new DataColumn("Id", typeof(int)));
                        //dt.Columns.Add(new DataColumn("ProcessId", typeof(int)));
                        //dt.Columns.Add(new DataColumn("ProcessName", typeof(string)));
                        //dt.Columns.Add(new DataColumn("OperatorId", typeof(int)));
                        //dt.Columns.Add(new DataColumn("OperatorName", typeof(int)));
                        //dt.Columns.Add(new DataColumn("WorksheetId", typeof(int)));
                        //dt.Columns.Add(new DataColumn("InsertDateTime", typeof(DateTime)));
                        //dt.Columns.Add(new DataColumn("Date", typeof(string)));
                        //dt.Columns.Add(new DataColumn("Time", typeof(string)));

                        newRow["Hour"] = zeroTime.Hour;
                        newRow["Min"] = zeroTime.Minute;

                        var prId = reader.GetInt32(7);

                        newRow["Id"] = i++;
                        newRow["Date"] = reader.GetString(1).ToString().Substring(0, 10);
                        newRow["ProcessId"] = prId;
                        newRow["ProcessName"] = reader.GetString(8);
                        newRow["OperatorId"] = reader.GetInt32(5);

                        var allowedPrTime = allowedPrTimes.FirstOrDefault(a => a.ProcessId == prId).ProcessTime;
                        zeroTime = zeroTime.AddMinutes(allowedPrTime);

                        dt.Rows.Add(newRow);
                    }
                }

                reader.Close();
            }
        }

        private void BindGrid()
        {
            RadGrid1.DataSource = Session["Result"];
            RadGrid1.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (RadDropDownWorksheets.SelectedValue.ToSafeString() == "")
            {
                lblMessage.Visible = true;
                lblMessage.Text = "اطلاعاتی برای ذخیره کردن یافت نشد";
                return;
            }

            var selectedWSInsertDateTime = (DateTime)new WorksheetRepository().GetById(RadDropDownWorksheets.SelectedValue.ToSafeInt()).InsertDateTime;

            var dt = (DataTable)Session["Result"];

            if (dt == null)
            {
                lblMessage.Visible = true;
                lblMessage.Text = "اطلاعاتی برای ذخیره کردن یافت نشد";
                return;
            }

            var uow = new UnitOfWork();

            DateTime? prevItemDt = null;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var wlDateTime = new DateTime(selectedWSInsertDateTime.Date.Year, selectedWSInsertDateTime.Date.Month, selectedWSInsertDateTime.Date.Day, dt.Rows[i].Field<int>("Hour").ToSafeInt(), dt.Rows[i].Field<int>("Min").ToSafeInt(), 0);

                if(wlDateTime < (prevItemDt ?? DateTime.MinValue))
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "زمان ها باید به ترتیب به صورت صعودی ثبت شوند";
                    return;
                }

                var newWorkline = new Repository.Entity.Domain.WorkLine()
                {
                    OperatorId = dt.Rows[i].Field<int>("OperatorId"),
                    ProcessId = dt.Rows[i].Field<int>("ProcessId"),
                    InsertDateTime = wlDateTime,
                    WorksheetId = RadDropDownWorksheets.SelectedValue.ToSafeInt(),
                    Manual = true
                };

                prevItemDt = wlDateTime;
                uow.WorkLines.Create(newWorkline);
            }

            var result = uow.SaveChanges();

            if (result.IsSuccess)
            {
                lblMessage.Visible = false;

                Session["PostProcessMessage"] = new PostProcessMessage()
                {
                    Message = "عملیات با موفقیت انجام شد",
                    MessageType = MessageType.Success
                };
                Response.Redirect("ManualWorkLineInput.aspx");
            }
            else
            {
                Session["PostProcessMessage"] = null;
                lblMessage.Visible = true;
                lblMessage.Text = result.ResultMessage;
            }
        }
    }
}