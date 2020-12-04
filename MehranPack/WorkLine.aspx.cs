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
            var processId = parts[2].Replace("#","").ToSafeInt();

            var workLinerepo = new WorkLineRepository();
            var thisWorksheetWorkLines = workLinerepo.Get(a => a.WorksheetId == worksheetId);

            var prevProcessIsEsghat = thisWorksheetWorkLines.Any() ? thisWorksheetWorkLines.Any(a => a.ProcessId == 1001) : false;
            if (prevProcessIsEsghat)
                return "پس از اسقاظ قادر به ثبت فرآیند دیگری نیستید";

            var prevProcessOfThisWorksheet = thisWorksheetWorkLines.Any() ? thisWorksheetWorkLines.Where(a=>a.ProcessId != 999 && a.ProcessId != 1000 && a.ProcessId != 1001).Max(a => a.ProcessId) : 0;

            if(processId != 1000 && processId != 1001)
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

            if (processId != 999 && processId != 1000 && processId != 1001)
            {
                var thisWorksheetProcesses = (List<int>)HttpContext.Current.Session["worksheetProcesses" + "#" + worksheetId];
                var indexOfPrevProcess = thisWorksheetProcesses.IndexOf(prevProcessOfThisWorksheet);
                var indexOfNextProcess = indexOfPrevProcess + 1;
                var nextProcessOfThisWorksheet = thisWorksheetProcesses[indexOfNextProcess];

                if(processId != nextProcessOfThisWorksheet)
                   return "عدم رعایت ترتیب فرآیند" + "- کاربر:" + new UserRepository().GetById(operatorId)?.Username + "- کاربرگ:" + worksheetId;
            }
           
            var uow = new UnitOfWork();

            var wl = uow.WorkLines.Get(a => a.WorksheetId == worksheetId && a.OperatorId == operatorId && a.ProcessId == processId);
            if (wl?.Count != 0)
                return $"فرآیند با این مشخصات قبلا ثبت شده کاربر:{new UserRepository().GetById(operatorId)?.Username} ";

            uow.WorkLines.Create(new Repository.Entity.Domain.WorkLine()
            {
                InsertDateTime = DateTime.Now,
                WorksheetId = worksheetId,
                OperatorId = operatorId,
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

        [WebMethod]
        public static string CheckReworkEsghatPassword(string input)
        {
            var userRepo = new UserRepository();
            var user = userRepo.Get(a => a.ReworkPassword!=null && a.ReworkPassword != "").FirstOrDefault();

            if (user.ReworkPassword == input)
                return "OK";

            return "0";
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