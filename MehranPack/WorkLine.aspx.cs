using Common;
using Newtonsoft.Json;
using Repository.DAL;
using Repository.Entity.Domain;
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
                BindDrpPrevProcess();
                BindDrpReason();
            }
        }

        [WebMethod]
        public static string AddRow(string input,string reworkACodes,string reworkReasons, string reworkDesc,string reworkEsghatMode,string reworkEsghatPrevProcessId)
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
                return "پس از اسقاط قادر به ثبت فرآیند دیگری نیستید";

            var actualPrevProcess = thisWorksheetWorkLines.Any() ? thisWorksheetWorkLines.OrderByDescending(a => a.Id).FirstOrDefault().ProcessId : 0;
            //var breforePrevProcessOfThisWorksheet = thisWorksheetWorkLines.Any() ? thisWorksheetWorkLines.Where(a=>a.ProcessId != 999 && a.ProcessId != 1000 && a.ProcessId != 1001 && a.ProcessId != 1002).Max(a => a.ProcessId) : 0;
            var prevProcessOfThisWorksheet = thisWorksheetWorkLines.Any() ? thisWorksheetWorkLines.Where(a=>a.ProcessId != 999 && a.ProcessId != 1000 && a.ProcessId != 1001 && a.ProcessId != 1002).Max(a => a.ProcessId) : 0;

            
            if(actualPrevProcess==1002 && prevProcessOfThisWorksheet == processId)
            {
                //after launch previous process can be continued
            }
            else
            if(processId != 1000 && processId != 1001 && processId != 1002)
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

            if (processId != 999 && processId != 1000 && processId != 1001 && processId != 1002) // 999 etmame movaghat, 1000 rework, 1001 esghat,1002 nahar
            {
                var thisWorksheetProcesses = (List<int>)HttpContext.Current.Session["worksheetProcesses" + "#" + worksheetId];
                var indexOfPrevProcess = thisWorksheetProcesses.IndexOf(prevProcessOfThisWorksheet);
                var indexOfNextProcess = indexOfPrevProcess + 1;
                var nextProcessOfThisWorksheet = thisWorksheetProcesses[indexOfNextProcess];

                if(processId != nextProcessOfThisWorksheet && !(actualPrevProcess == 1002 && prevProcessOfThisWorksheet == processId))
                   return "عدم رعایت ترتیب فرآیند" + "- کاربر:" + new UserRepository().GetById(operatorId)?.Username + "- کاربرگ:" + worksheetId;
            }
           
            var uow = new UnitOfWork();

            if (processId != 999 && processId != 1000 && processId != 1001)
            {
                var wl = uow.WorkLines.Get(a => a.WorksheetId == worksheetId && a.OperatorId == operatorId && a.ProcessId == processId);
                if (wl?.Count != 0 && !(actualPrevProcess == 1002 && prevProcessOfThisWorksheet == processId))
                    return $"فرآیند با این مشخصات قبلا ثبت شده کاربر:{new UserRepository().GetById(operatorId)?.Username} ";
            }
            else
            {
                var intReworkEsghatPrevProcessId = reworkEsghatPrevProcessId.ToSafeInt();

                if (processId == 1000)
                {
                    var rw = uow.Reworks.Get(a => a.WorksheetId == worksheetId && a.PrevProcessId == intReworkEsghatPrevProcessId);
                    if (rw?.Count != 0)
                        return "برای این فرآیند قبلا دوباره کاری ثبت شده است";
                }
                else if (processId == 1001)
                {
                    var rw = uow.Esghats.Get(a => a.WorksheetId == worksheetId && a.PrevProcessId == intReworkEsghatPrevProcessId);
                    if (rw?.Count != 0)
                        return "برای این فرآیند قبلا اسقاط ثبت شده است";
                }
            }

            var newWorkLine = uow.WorkLines.Create(new Repository.Entity.Domain.WorkLine()
            {
                InsertDateTime = DateTime.Now,
                WorksheetId = worksheetId,
                OperatorId = operatorId,
                ProcessId = processId
            }
            );

            var acodes = reworkACodes.Split(',');
            var reasons = reworkReasons.Split(',');

            if (acodes.Contains("-1"))
                return "علت انتخاب نشده";

            if (reworkEsghatMode != "")
            {
                if (reworkEsghatMode.ToLower() == "rework")
                {
                    var newRework = uow.Reworks.Create(new Repository.Entity.Domain.Rework()
                    {
                        InsertDateTime = DateTime.Now,
                        OperatorId = operatorId,
                        InsertedUserId = operatorId,  //?
                        Desc = reworkDesc,
                        PrevProcessId = reworkEsghatPrevProcessId.ToSafeInt(),
                        WorksheetId = worksheetId,
                        Status = -1,
                    });

                    if(acodes.Any())
                    {
                        newRework.ReworkDetails = new List<ReworkDetail>();

                        for (int i = 0; i < acodes.Length; i++)
                        {
                            newRework.ReworkDetails.Add(new ReworkDetail { ACode = acodes[i], ReworkReasonId = reasons[i].ToSafeInt() });
                        }
                        
                    }

                    newWorkLine.Rework = newRework;
                }
                else
                {
                    var newEsghat = uow.Esghats.Create(new Repository.Entity.Domain.Esghat()
                    {
                        InsertDateTime = DateTime.Now,
                        OperatorId = operatorId,
                        InsertedUserId = operatorId,  //?
                        Desc = reworkDesc,
                        PrevProcessId = reworkEsghatPrevProcessId.ToSafeInt(),
                        WorksheetId = worksheetId,
                        Status = -1
                    });

                    if (acodes.Any())
                    {
                        newEsghat.EsghatDetails = new List<EsghatDetail>();

                        for (int i = 0; i < acodes.Length; i++)
                        {
                            newEsghat.EsghatDetails.Add(new EsghatDetail { ACode = acodes[i], ReworkReasonId = reasons[i].ToSafeInt() });
                        }
                    }

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
        public static string GetLastProcessOfWorksheet(int worksheetId,int operatorId)
        {
            var result = HttpContext.Current.Session[worksheetId + "#" + operatorId].ToSafeString();
            
            if(string.IsNullOrWhiteSpace(result) || result=="999" || result == "1000" || result == "1001")
                return new WorkLineRepository().Get(a => a.WorksheetId == worksheetId && a.ProcessId != 999 && a.ProcessId != 1000 && a.ProcessId != 1001).OrderByDescending(a => a.Id).FirstOrDefault()?.ProcessId.ToString();

            return result;
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

        [WebMethod]
        public static string GetRelatedACodes(int worksheetId)
        {
            var repo = new WorksheetDetailRepository();
            var source = repo.Get(a=>a.WorksheetId== worksheetId).Select(a => a.ACode).Distinct();

            return JsonConvert.SerializeObject(source);
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

        private void BindDrpPrevProcess()
        {
            var source = new ProcessRepository().GetAll();
            
            drpPrevProcess.DataSource = source;
            drpPrevProcess.DataValueField = "Id";
            drpPrevProcess.DataTextField = "Name";
            drpPrevProcess.DataBind();
        }
                
        private void BindDrpACode()
        {
            var repo = new WorksheetDetailRepository();
            var source = new List<Repository.Entity.Domain.WorksheetDetail>();
            source.Add(new Repository.Entity.Domain.WorksheetDetail() { ACode = "انتخاب کنید" });
            source.AddRange(repo.GetAll());

            //drpACode.DataSource = source;
            //drpACode.DataValueField = "ACode";
            //drpACode.DataTextField = "ACode";
            //drpACode.DataBind();
        }

        private void BindDrpReason()
        {
            var repo = new ReworkReasonRepository();
            var source = new List<Repository.Entity.Domain.ReworkReason>();
            source.Add(new Repository.Entity.Domain.ReworkReason() { Id = -1, Name = "انتخاب کنید" });
            source.AddRange(repo.GetAll());

            drpReworkReason0.DataSource = source;
            drpReworkReason0.DataValueField = "Id";
            drpReworkReason0.DataTextField = "Name";
            drpReworkReason0.DataBind();
        }
    }
}