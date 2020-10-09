using Common;
using Repository.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MehranPack
{
    public partial class WorkLine : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var repo = new WorkLineRepository();

            if (chkShowAll.Checked)
                gridWorkLine.DataSource = repo.GetAllWorkLines();
            else
                gridWorkLine.DataSource = repo.GetTodayWorkLine();

            gridWorkLine.DataBind();

            lblCurrentDate.Text = Common.Utility.CastToFaDate(DateTime.Now).ToPersianNumber();
            Session["Result"] = gridWorkLine.DataSource;
            txtBarcodeInput.Focus();

            Session["InputBarcode"] = txtBarcodeInput.Text;
            txtBarcodeInput.Text = "";
        }

        [WebMethod]
        public static string AddRow(string input)
        {
            //WID,OperatorID,ProcessID
            var parts = input.Replace('و' , ',').Split(',');

            var worksheetId = parts[0].ToSafeInt();
            var operatorId = parts[1].ToSafeInt();
            var processId = parts[2].ToSafeInt();

            var workLinerepo = new WorkLineRepository();
            var thisWorksheetWorkLines = workLinerepo.Get(a => a.WorksheetId == worksheetId);

            var prevProcessOfThisWorksheet = thisWorksheetWorkLines.Any() ? thisWorksheetWorkLines.Where(a=>a.ProcessId != 999).Max(a => a.ProcessId) : 0;

            if(prevProcessOfThisWorksheet !=999)
               if (thisWorksheetWorkLines != null && thisWorksheetWorkLines.Any())
                if (prevProcessOfThisWorksheet != 0 && prevProcessOfThisWorksheet >= processId)
                    return "عدم رعایت ترتیب فرآیند" + "- کاربر:" + new UserRepository().GetById(operatorId)?.Username + "- کاربرگ:" + worksheetId;

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

            if (processId != 999 && processId != nextProcessOfThisWorksheet)
                return "عدم رعایت ترتیب فرآیند" + "- کاربر:" + new UserRepository().GetById(operatorId)?.Username + "- کاربرگ:" + worksheetId;

           
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

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //Response.Redirect("Category.aspx");
        }

        protected void gridWorkLine_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridWorkLine.PageIndex = e.NewPageIndex;
            gridWorkLine.DataSource = Session["Result"];
            gridWorkLine.DataBind();
        }

        protected void chkShowAll_CheckedChanged(object sender, EventArgs e)
        {
            Response.Redirect("workline.aspx");
        }
    }
}