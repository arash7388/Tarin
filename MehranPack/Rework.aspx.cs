using Common;
using Repository.DAL;
using Repository.Entity.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MehranPack
{
    public partial class Rework : System.Web.UI.Page
    {
        private bool IsEsghatMode()
        {
            return Page.RouteData.Values["Mode"].ToSafeString().ToLower() == "es";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsEsghatMode())
            {
                h3Title.InnerText = "اسقاط";

                if (!Page.IsPostBack)
                {
                    BindDrpOp();
                    BindDrpACode();
                    BindDrpReason();

                    if (Page.RouteData.Values["Id"].ToSafeInt() != 0)
                    {
                        var repo = new EsghatRepository();
                        var toBeEditedEs = repo.GetById(Page.RouteData.Values["Id"].ToSafeInt());

                        if (toBeEditedEs != null)
                        {
                            txtDesc.Text = toBeEditedEs.Desc;
                            //drpACode.SelectedValue = toBeEditedEs.ACode;
                            drpOp.SelectedValue = toBeEditedEs.OperatorId.ToSafeString();
                            drpReason.SelectedValue = toBeEditedEs.ReworkReasonId.ToSafeString();
                        }
                    }
                }
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    BindDrpOp();
                    BindDrpACode();
                    BindDrpReason();

                    if (Page.RouteData.Values["Id"].ToSafeInt() != 0)
                    {
                        var repo = new ReworkRepository();
                        var toBeEditedRework = repo.GetById(Page.RouteData.Values["Id"].ToSafeInt());

                        if (toBeEditedRework != null)
                        {
                            txtDesc.Text = toBeEditedRework.Desc;
                            //drpACode.SelectedValue = toBeEditedRework.ACode;
                            drpOp.SelectedValue = toBeEditedRework.OperatorId.ToSafeString();
                            drpReason.SelectedValue = toBeEditedRework.ReworkReasonId.ToSafeString();
                        }
                    }
                }
            }
        }

        private void BindDrpOp()
        {
            var repo = new UserRepository();
            var source = new List<Repository.Entity.Domain.User>();
            source.Add(new Repository.Entity.Domain.User() { Id = -1, FriendlyName = "انتخاب کنید" });
            source.AddRange(repo.GetAll().Where(a=>a.Type==1).ToList());

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

            drpReason.DataSource = source;
            drpReason.DataValueField = "Id";
            drpReason.DataTextField = "Name";
            drpReason.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (drpACode.SelectedValue.ToSafeString() == "انتخاب کنید") throw new LocalException("ACode is empty", "ACode  را وارد نمایید");
                if (drpOp.SelectedValue.ToSafeString() == "-1") throw new LocalException("Op is empty", "اوپراتور را وارد نمایید");
                if (drpReason.SelectedValue.ToSafeString() == "-1") throw new LocalException("Reason is empty", "علت را وارد نمایید");

                UnitOfWork uow = new UnitOfWork();

                if(IsEsghatMode())
                    SaveEsghat(uow);
                else 
                    SaveRework(uow);

                if (uow.SaveChanges().IsSuccess)
                {
                    ((Main)Page.Master).SetGeneralMessage("اطلاعات با موفقیت ذخیره شد", MessageType.Success);
                    ClearControls();
                }
                else
                {
                    ((Main)Page.Master).SetGeneralMessage("خطا در دخیره سازی", MessageType.Error);
                }
            }
            catch (LocalException ex)
            {
                ((Main)Page.Master).SetGeneralMessage("خطا در دخیره سازی -" + ex.ResultMessage, MessageType.Error);
            }
        }

        private void SaveRework(UnitOfWork uow)
        {
            if (Page.RouteData.Values["Id"].ToSafeInt() == 0)
            {
                var newRework = new Repository.Entity.Domain.Rework();

                //newRework.ACode = drpACode.SelectedValue.ToSafeString();
                newRework.OperatorId = drpOp.SelectedValue.ToSafeInt();
                newRework.ReworkReasonId = drpReason.SelectedValue.ToSafeInt();
                newRework.Desc = txtDesc.Text;
                newRework.InsertedUserId = ((User)Session["User"]).Id;

                uow.Reworks.Create(newRework);
            }
            else
            {
                var repo = uow.Reworks;
                var tobeEditedRework = repo.GetById(Page.RouteData.Values["Id"].ToSafeInt());

                //tobeEditedRework.ACode = drpACode.SelectedValue.ToSafeString();
                tobeEditedRework.OperatorId = drpOp.SelectedValue.ToSafeInt();
                tobeEditedRework.ReworkReasonId = drpReason.SelectedValue.ToSafeInt();
                tobeEditedRework.Desc = txtDesc.Text;
            }
        }


        private void SaveEsghat(UnitOfWork uow)
        {
            if (Page.RouteData.Values["Id"].ToSafeInt() == 0)
            {
                var newEsghat = new Repository.Entity.Domain.Esghat();

                //newEsghat.ACode = drpACode.SelectedValue.ToSafeString();
                newEsghat.OperatorId = drpOp.SelectedValue.ToSafeInt();
                newEsghat.ReworkReasonId = drpReason.SelectedValue.ToSafeInt();
                newEsghat.Desc = txtDesc.Text;
                newEsghat.InsertedUserId = ((User)Session["User"]).Id;

                uow.Esghats.Create(newEsghat);
            }
            else
            {
                var repo = uow.Esghats;
                var tobeEditedEsghat = repo.GetById(Page.RouteData.Values["Id"].ToSafeInt());

                //tobeEditedEsghat.ACode = drpACode.SelectedValue.ToSafeString();
                tobeEditedEsghat.OperatorId = drpOp.SelectedValue.ToSafeInt();
                tobeEditedEsghat.ReworkReasonId = drpReason.SelectedValue.ToSafeInt();
                tobeEditedEsghat.Desc = txtDesc.Text;
            }
        }

        private void ClearControls()
        {
            drpACode.SelectedValue = "انتخاب کنید";
            drpOp.SelectedValue = drpReason.SelectedValue= "-1";
            lblProductName.Text = "";
            txtDesc.Text = "";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if(IsEsghatMode())
            {
                var routeValues = new RouteValueDictionary();
                routeValues.Add("Mode", "Es");
                Response.RedirectToRoute("EsghatList");
            }
            else  Response.RedirectToRoute("ReworkList");
        }
    }
}