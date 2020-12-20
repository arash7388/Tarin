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

            if (!IsPostBack)
            {
                BindDrpACode();
                BindDrpOp();
                BindDrpReason();
            }
        }

        [WebMethod]
        public static string AddRow(string input,string reworkACode,string reworkReasonId,string reworkDesc,string reworkEsghatMode)
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

            if (processId != 999 && processId != 1000 && processId != 1001) // 999 etmame movaghat, 1000 rework, 1001 esghat
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

            var newWorkLine = uow.WorkLines.Create(new Repository.Entity.Domain.WorkLine()
            {
                InsertDateTime = DateTime.Now,
                WorksheetId = worksheetId,
                OperatorId = operatorId,
                ProcessId = processId
            }
            );

            if (reworkEsghatMode != "")
            {
                if (reworkEsghatMode.ToLower() == "rework")
                {
                    var newRework = uow.Reworks.Create(new Repository.Entity.Domain.Rework()
                    {
                        ACode = reworkACode.ToEnglishNumber(),
                        InsertDateTime = DateTime.Now,
                        OperatorId = operatorId,
                        InsertedUserId = operatorId,  //?
                        ReworkReasonId = reworkReasonId.ToSafeInt(),
                        Desc = reworkDesc,
                        Status = -1
                    });

                    newWorkLine.Rework = newRework;
                }
                else
                {
                    var newEsghat = uow.Esghats.Create(new Repository.Entity.Domain.Esghat()
                    {
                        ACode = reworkACode.ToEnglishNumber(),
                        InsertDateTime = DateTime.Now,
                        OperatorId = operatorId,
                        InsertedUserId = operatorId,  //?
                        ReworkReasonId = reworkReasonId.ToSafeInt(),
                        Desc = reworkDesc,
                        Status = -1
                    });

                    newWorkLine.Esghat = newEsghat;
                }
            }

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

                data.RawCommand = $"Delete from Worklines where Id = {e.CommandArgument.ToSafeString()}";

                var wl = new WorkLineRepository().GetById(e.CommandArgument.ToSafeInt());
                                
                if(wl.ReworkId!=null)
                   data.RawCommand += $" Delete from Reworks where Id = {wl.ReworkId}";
                else if (wl.EsghatId != null)
                    data.RawCommand += $" Delete from Esghats where Id = {wl.EsghatId}";

                data.Msg = "آیا از حذف اطمینان دارید؟";
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

        private void BindDrpOp()
        {
            var repo = new UserRepository();
            var source = new List<Repository.Entity.Domain.User>();
            source.Add(new Repository.Entity.Domain.User() { Id = -1, FriendlyName = "انتخاب کنید" });
            source.AddRange(repo.GetAll().Where(a => a.Type == 1).ToList());

            drpOp.DataSource = source;
            drpOp.DataValueField = "Id";
            drpOp.DataTextField = "FriendlyName";
            drpOp.DataBind();
        }

        private void BindDrpACode()
        {
            var repo = new WorksheetDetailRepository();
            var source = new List<Repository.Entity.Domain.WorksheetDetail>();
            source.Add(new Repository.Entity.Domain.WorksheetDetail() { ACode = "انتخاب کنید" });
            source.AddRange(repo.GetAll());

            drpACode.DataSource = source;
            drpACode.DataValueField = "ACode";
            drpACode.DataTextField = "ACode";
            drpACode.DataBind();
        }

        private void BindDrpReason()
        {
            var repo = new ReworkReasonRepository();
            var source = new List<Repository.Entity.Domain.ReworkReason>();
            source.Add(new Repository.Entity.Domain.ReworkReason() { Id = -1, Name = "انتخاب کنید" });
            source.AddRange(repo.GetAll());

            drpReworkReason.DataSource = source;
            drpReworkReason.DataValueField = "Id";
            drpReworkReason.DataTextField = "Name";
            drpReworkReason.DataBind();
        }
    }
}