using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using MehranPack.UserControls;
using Repository.DAL;
using Repository.Entity.Domain;
using Telerik.Web.UI;

namespace MehranPack
{
    public partial class Payment : System.Web.UI.Page
    {
        //public List<KeyValuePair<int, string>> PaymentTypeList
        //{
        //    get
        //    {
        //        var values = Enum.GetValues(typeof (PaymentTypeEnum)).Cast<int>();
                
        //        var result = new List<KeyValuePair<int, string>>();
                
        //        foreach (int item in values)
        //        {
        //            result.Add(new KeyValuePair<int, string>(item,Enum.GetName(typeof(PaymentTypeEnum),item)));
        //        }

        //        return result;
        //    } 
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Environment.MachineName.ToLower() == "arash-laptop")
                Session["User"] = new BaseRepository<User>().GetById(1);
            
            try
            {
                Debuging.Info("Payment->Try to Page_Load");

                if (!Page.IsPostBack)
                {
                     BindDrpFactor();

                    if (Page.RouteData.Values["Id"].ToSafeInt() != 0)
                    {
                        var paymentId = Page.RouteData.Values["Id"].ToSafeInt();

                        var paymentRepo = new PaymentRepository();
                        var payment = paymentRepo.GetById(paymentId);

                        txtDescription.Text = payment.Description;
                        txtPaymentNo.Text = payment.PaymentNo.ToString();
                        drpFactor.SelectedValue = payment.FactorId.ToString();
                        drpFactor_OnSelectedIndexChanged(sender,new RadComboBoxSelectedIndexChangedEventArgs("","",payment.FactorId.ToString(),""));
                        ucDate.Date = payment.PaymentDate.ToFaDate();
                        var paymentDetailRepo = new PaymentDetailRepository();

                        gridPayment.DataSource = Session["GridSource"] = paymentDetailRepo.GetPaymentDetails(paymentId);
                        gridPayment.DataBind();
                    }
                    else
                    {
                        ucDate.Date = DateTime.Now.ToFaDate();
                        gridPayment.DataSource = Session["GridSource"] = new List<PaymentDetail>();
                        gridPayment.DataBind();

                        txtPaymentNo.Text = (new PaymentRepository().GetMaxPaymentNo()+ 1).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Debuging.Error(ex, MethodBase.GetCurrentMethod().Name);
            }
            Debuging.Info("Payment->End of Page_Load");
        }

        private void BindDrpFactor()
        {
            var repo = new FactorRepository();
            var factors = repo.GetAllFactors().OrderBy(a => a.FactorNo);
        
            drpFactor.DataSource = factors;
            drpFactor.DataValueField = "Id";
            drpFactor.DataTextField = "FactorNo";
            drpFactor.DataBind();
        }

        protected void drpFactor_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var factor = new FactorRepository().GetById(e.Value.ToSafeInt());
            if (factor != null)
            {
                lblFactorPrice.Visible = lblFactorCustomer.Visible = 
                    lblPriceCaption.Visible = lblCustomerCaption.Visible = true;
                lblFactorPrice.Text = factor.TotalPrice.ToFaGString();
                lblFactorCustomer.Text = new CustomerRepository().GetById(factor.CustomerId).Name;
            }
            else
            {
                lblFactorPrice.Visible = lblFactorCustomer.Visible =
                    lblPriceCaption.Visible = lblCustomerCaption.Visible = false;
                lblFactorPrice.Text = "";
                lblFactorCustomer.Text = "";
            }
        }

        protected void gridFactor_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            
        }

        protected void gridFactor_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            
        }

        protected void gridFactor_OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            gridPayment.EditIndex = e.NewEditIndex;
            BindData();
        }

        private void BindData()
        {
            gridPayment.DataSource = Session["GridSource"];
            gridPayment.DataBind();
        }
        protected void gridFactor_OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
           try
            {
                var dateValue = ((PersianCalender)gridPayment.Rows[gridPayment.EditIndex].Cells[1].Controls[1]).Date;
                var priceValue = ((TextBox)gridPayment.Rows[gridPayment.EditIndex].Cells[2].Controls[1]).Text;
                var typeValue = ((DropDownList)gridPayment.Rows[gridPayment.EditIndex].Cells[3].Controls[1]).SelectedValue;
                var desValue = ((TextBox)gridPayment.Rows[gridPayment.EditIndex].Cells[4].Controls[1]).Text;

                var fd = ((List<PaymentDetail>)Session["GridSource"]).SingleOrDefault(a => a.RowNumber == e.RowIndex + 1);

                fd.DetailDate = dateValue.ToEnDate();
                fd.Price = priceValue.ToSafeDecimal();
                fd.PaymentType = typeValue.ToSafeInt();
                fd.Description = desValue;

                gridPayment.EditIndex = -1;
                BindData();
            }
            catch (Exception ex)
            {
                Debuging.Error(ex, MethodBase.GetCurrentMethod().Name);
                ShowErrorMsg(MessageText.UNKNOWN_ERROR);
            }
        }

        private void ShowErrorMsg(string msg)
        {
            ((Main)Page.Master).SetGeneralMessage(msg, MessageType.Error);
        }

        protected void gridFactor_OnRowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gridPayment.EditIndex = -1;
            BindData();
        }

        protected void gridFactor_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var fd = ((List<PaymentDetail>)Session["GridSource"]).SingleOrDefault(a => a.RowNumber == e.RowIndex + 1);
            ((List<PaymentDetail>)Session["GridSource"]).Remove(fd);

            ((List<PaymentDetail>)Session["GridSource"]).Where(a=>a.RowNumber>fd.RowNumber).ToList().ForEach(a =>  a.RowNumber = a.RowNumber - 1);
            gridPayment.EditIndex = -1;
            BindData();
        }

        protected void btnAddFirstRow_OnClick(object sender, ImageClickEventArgs e)
        {
            try
            {
                var newRow = new PaymentDetail();
                newRow.RowNumber = 1;
                var source = new List<PaymentDetail>();
                source.Add(newRow);

                Session["GridSource"] = source;

                gridPayment.EditIndex = 0;
                BindData();
            }
            catch (Exception ex)
            {
                Debuging.Error(ex, MethodBase.GetCurrentMethod().Name);
                ShowErrorMsg(MessageText.UNKNOWN_ERROR);
            }
        }

        protected void gridFactor_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate))
                    UpdateTxtTotalPrice();
            }
            catch (Exception ex)
            {
                Debuging.Error(ex, MethodBase.GetCurrentMethod().Name);
                ShowErrorMsg(MessageText.UNKNOWN_ERROR);
            }
        }

        protected void btnAddFooter_OnClick(object sender, ImageClickEventArgs e)
        {
            try
            {
                var dateValue = ((PersianCalender)gridPayment.FooterRow.Cells[1].Controls[1]).Date;
                var priceValue = ((TextBox)gridPayment.FooterRow.Cells[2].Controls[1]).Text;
                var typeValue = ((DropDownList)gridPayment.FooterRow.Cells[3].Controls[1]).SelectedValue;
                var desValue = ((TextBox)gridPayment.FooterRow.Cells[4].Controls[1]).Text;

                var fd = new PaymentDetail();

                fd.RowNumber = ((List<PaymentDetail>)Session["GridSource"]).Max(a => a.RowNumber) + 1;
                fd.DetailDate = dateValue.ToEnDate();
                fd.Price = priceValue.ToSafeDecimal();
                fd.PaymentType = typeValue.ToSafeInt();
                fd.Description = desValue;
                
                ((List<PaymentDetail>)Session["GridSource"]).Add(fd);
                BindData();
            }
            catch (Exception ex)
            {
                Debuging.Error(ex, MethodBase.GetCurrentMethod().Name);
                ShowErrorMsg(MessageText.UNKNOWN_ERROR);
            }
        }

        protected void gridFactor_OnRowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            UpdateTxtTotalPrice();
        }

        private void UpdateTxtTotalPrice()
        {
            var fds = ((List<PaymentDetail>)Session["GridSource"]);
            txtTotalPrice.Text = fds.Sum(a => a.Price).ToSafeString().ToFaGString();
        }

        protected string GetPaymentTypeName(int type)
        {
            var repo = new BaseRepository<PaymentType>();
            if (repo.GetById(type)!=null)
                return repo.GetById(type).Name;

            return "";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateData()) return;

                if (Page.RouteData.Values["Id"].ToSafeInt() == 0)
                {
                    var uow = new UnitOfWork();

                    var payment = new Repository.Entity.Domain.Payment();

                    payment.PaymentNo = txtPaymentNo.Text.ToSafeInt();
                    payment.FactorId = drpFactor.SelectedValue.ToSafeInt();
                    payment.InsertDateTime = DateTime.Now;
                    payment.PaymentDate = ucDate.Date.ToEnDate();
                    payment.Description = txtDescription.Text;
                    
                    var fd = ((List<PaymentDetail>)Session["GridSource"]);
                    payment.TotalPrice = fd.Sum(a => a.Price );
                    payment.PaymentDetails = fd;
                    uow.Payments.Create(payment);

                    var result = uow.SaveChanges();

                    if (result.IsSuccess)
                        RedirectPaymentListActionResultWithMessage();
                    else
                    {
                        Debuging.Error(MethodBase.GetCurrentMethod().Name + "->" + result.ResultMessage);
                        ShowErrorMsg(MessageText.UNKNOWN_ERROR);
                    }
                }
                else
                {
                    var uow = new UnitOfWork();

                    var toBeEditedPayment = uow.Payments.Find(Page.RouteData.Values["Id"].ToSafeInt());

                    toBeEditedPayment.PaymentNo = txtPaymentNo.Text.ToSafeInt();
                    toBeEditedPayment.FactorId = drpFactor.SelectedValue.ToSafeInt();
                    toBeEditedPayment.UpdateDateTime = DateTime.Now;
                    //toBeEditedPayment.FactorDate = txtDate.Text.ToEnDate();
                    toBeEditedPayment.Description = txtDescription.Text;
                    toBeEditedPayment.PaymentDate = ucDate.Date.ToEnDate();

                    var pd = ((List<PaymentDetail>)Session["GridSource"]);

                    toBeEditedPayment.TotalPrice = pd.Sum(a => a.Price);
                    toBeEditedPayment.PaymentDetails = pd;

                    var oldPdIds = new PaymentDetailRepository().GetPaymentDetails(toBeEditedPayment.Id).Select(a => a.Id);

                    uow.PaymentDetails.Delete(a => oldPdIds.Contains(a.Id));

                    var result = uow.SaveChanges();

                    if (result.IsSuccess)
                        RedirectPaymentListActionResultWithMessage();
                    else
                    {
                        Debuging.Error(MethodBase.GetCurrentMethod().Name + "->" + result.ResultMessage);
                        ShowErrorMsg(MessageText.UNKNOWN_ERROR);
                    }
                }
            }
            catch (Exception ex)
            {
                Debuging.Error(ex, MethodBase.GetCurrentMethod().Name);
                ShowErrorMsg(MessageText.UNKNOWN_ERROR);
            }
        }

        private void RedirectPaymentListActionResultWithMessage()
        {
            var res = new ActionResult();

            res.ResultMessage = MessageText.SUCCESS;
            res.IsSuccess = true;
            Session["SavePaymentActionResult"] = res;

            var routeValues = new RouteValueDictionary();
            routeValues.Add("ActionResult", true);
            Response.RedirectToRoute("PaymentListActionResult", routeValues);
        }

        private bool ValidateData()
        {
            if (txtPaymentNo.Text.ToSafeString() == "")
            {
                ShowErrorMsg("شماره را وارد نمایید");
                return false;
            }

            if (drpFactor.SelectedValue.ToSafeInt() == 0)
            {
                ShowErrorMsg("فاکتور را انتخاب نمایید");
                return false;
            }

            //todo
            //if (txtDate.Text.ToSafeString() == "")
            //{
            //    ShowErrorMsg("تاریخ فاکتور را وارد نمایید");
            //    return false;
            //}

            if (Page.RouteData.Values["Id"].ToSafeInt() == 0 &&
                 new PaymentRepository().GetByPaymentNo(txtPaymentNo.Text.ToSafeInt()) != null)
            {
                ShowErrorMsg("شماره تکراری است");
                return false;
            }

            var source = ((List<PaymentDetail>)Session["GridSource"]);

            if (!source.Any())
            {
                ShowErrorMsg("سند دریافتی هیچ ردیفی ندارد");
                return false;
            }

            for (int i = 0; i < source.Count; i++)
            {
                if (source[i].Price.ToSafeDecimal() == 0)
                {
                    ShowErrorMsg("مبلغ را وارد نمایید(ردیف " + (i + 1).ToString() + ")");
                    return false;
                }

                if (source[i].DetailDate ==DateTime.MinValue || source[i].DetailDate.ToString() == "")
                {
                    ShowErrorMsg("تاریخ را وارد نمایید(ردیف " + (i + 1).ToString() + ")");
                    return false;
                }
            }

            return true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.RedirectToRoute("PaymentList");
        }
    }
}