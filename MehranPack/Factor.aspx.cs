using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using Energy;
using Repository.DAL;
using Repository.Entity.Domain;

namespace MehranPack
{
    public partial class Factor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Debuging.Info("Factor->Try to Page_Load");

                if (!Page.IsPostBack)
                {
                    BindDrpCustomer();

                    if (Page.RouteData.Values["Id"].ToSafeInt() != 0)
                    {
                        var factorId = Page.RouteData.Values["Id"].ToSafeInt();

                        var factorRepo = new FactorRepository();
                        var factor = factorRepo.GetById(factorId);

                        txtFactorNumber.Text = factor.FactorNo.ToString();
                        txtDescription.Text = factor.Description;
                        drpCustomer.SelectedValue = factor.CustomerId.ToString();
                        txtDate.Text = factor.FactorDate.ToFaDate();

                        var factorDetailRepo = new FactorDetailRepository();

                        gridFactor.DataSource = Session["GridSource"] = factorDetailRepo.GetFactorDetails(factorId);
                        gridFactor.DataBind();
                    }
                    else
                    {
                        txtDate.Text = DateTime.Now.ToFaDate();
                        gridFactor.DataSource = Session["GridSource"] = new List<FactorDetail>();
                        gridFactor.DataBind();

                        txtFactorNumber.Text = (new FactorRepository().GetMaxFactorNo() + 1).ToString();
                    }

                    //BindFooterGoodsUnitDrp();
                }
            }
            catch (Exception ex)
            {
                Debuging.Error(ex,MethodBase.GetCurrentMethod().Name);
            }
            Debuging.Info("Factor->End of Page_Load");
        }

        private void ShowErrorMsg(string msg)
        {
            ((Main)Page.Master).SetGeneralMessage(msg, MessageType.Error);
        }
        private void BindDrpCustomer()
        {
            try
            {
                var repo = new CustomerRepository();
                var source = repo.GetAll();

                var customer = new Repository.Entity.Domain.Customer();
                customer.Id = -1;
                customer.Name = "انتخاب کنید...";
                source.Insert(0, customer);
                drpCustomer.DataSource = source;
                drpCustomer.DataValueField = "Id";
                drpCustomer.DataTextField = "Name";
                drpCustomer.DataBind();
            }
            catch (Exception ex)
            {
                Debuging.Error(ex, MethodBase.GetCurrentMethod().Name);
            }
        }

        //private void BindFooterGoodsUnitDrp()
        //{
        //    try
        //    {
        //        if (gridFactor.FooterRow != null)
        //        {
        //            var footergoodsUnit = ((DropDownList)gridFactor.FooterRow.Cells[2].Controls[1]);
        //            var unitRepo = new GoodsUnitRepository();
        //            footergoodsUnit.DataSource = unitRepo.GetAll();
        //            footergoodsUnit.DataValueField = "Id";
        //            footergoodsUnit.DataTextField = "Name";
        //            footergoodsUnit.DataBind();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debuging.Error(ex, MethodBase.GetCurrentMethod().Name);
        //    }
        //}

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(!ValidateData()) return;

                if (Page.RouteData.Values["Id"].ToSafeInt() == 0)
                {
                    var uow = new UnitOfWork();

                    var factor = new Repository.Entity.Domain.Factor();

                    factor.FactorNo = txtFactorNumber.Text.ToSafeInt();
                    factor.CustomerId = drpCustomer.SelectedValue.ToSafeInt();
                    factor.InsertDateTime = DateTime.Now;
                    factor.FactorDate = txtDate.Text.ToEnDate();
                    factor.Description = txtDescription.Text;
                    var fd = ((List<FactorDetail>) Session["GridSource"]);
                    factor.TotalPrice = fd.Sum(a => a.Count*a.Price);
                    factor.FactorDetails = fd;
                    uow.Factors.Create(factor);

                    var result = uow.SaveChanges();

                    if (result.IsSuccess)
                        RedirectFactorListActionResultWithMessage();
                    else
                    {
                        Debuging.Error(MethodBase.GetCurrentMethod().Name + "->" + result.ResultMessage);
                        ShowErrorMsg(MessageText.UNKNOWN_ERROR);
                    }
                }
                else
                {
                    var uow = new UnitOfWork();
                    
                    var toBeEditedfactor = uow.Factors.Find(Page.RouteData.Values["Id"].ToSafeInt());

                    toBeEditedfactor.FactorNo = txtFactorNumber.Text.ToSafeInt();
                    toBeEditedfactor.CustomerId = drpCustomer.SelectedValue.ToSafeInt();
                    toBeEditedfactor.UpdateDateTime = DateTime.Now;
                    toBeEditedfactor.FactorDate = txtDate.Text.ToEnDate();
                    toBeEditedfactor.Description = txtDescription.Text;

                    var fd = ((List<FactorDetail>)Session["GridSource"]);

                    toBeEditedfactor.TotalPrice = fd.Sum(a => a.Count * a.Price);
                    toBeEditedfactor.FactorDetails = fd;

                    var oldFdIds = new FactorDetailRepository().GetFactorDetails(toBeEditedfactor.Id).Select(a => a.Id);

                    uow.FactorDetails.Delete(a => oldFdIds.Contains(a.Id));
                    
                    var result = uow.SaveChanges();

                    if (result.IsSuccess)
                        RedirectFactorListActionResultWithMessage();
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

        private void RedirectFactorListActionResultWithMessage()
        {
            var res = new ActionResult();

            res.ResultMessage = MessageText.SUCCESS;
            res.IsSuccess = true;
            Session["SaveFactorActionResult"] = res;

            var routeValues = new RouteValueDictionary();
            routeValues.Add("ActionResult", true);
            Response.RedirectToRoute("FactorListActionResult", routeValues);
        }

        private bool ValidateData()
        {
            if (txtFactorNumber.Text.ToSafeString() == "")
            {
               ShowErrorMsg("شماره فاکتور را وارد نمایید");
               return false;
            }

            if (drpCustomer.SelectedValue.ToSafeInt() == -1)
            {
                ShowErrorMsg("مشتری را انتخاب نمایید");
                return false;
            }

            //todo
            if (txtDate.Text.ToSafeString() == "")
            {
                ShowErrorMsg("تاریخ فاکتور را وارد نمایید");
                return false;
            }

            if (Page.RouteData.Values["Id"].ToSafeInt()==0  && 
                 new FactorRepository().GetByFactorNo(txtFactorNumber.Text.ToSafeInt()) != null)
            {
                ShowErrorMsg("شماره فاکتور تکراری است");
                return false;
            }

            var source = ((List<FactorDetail>) Session["GridSource"]);

            if (!source.Any())
            {
                ShowErrorMsg("فاکتور هیچ ردیفی ندارد");
                return false;
            }

            for (int i = 0; i < source.Count; i++)
            {
                if (source[i].Count.ToSafeDecimal() == 0)
                {
                    ShowErrorMsg("تعداد را وارد نمایید(ردیف " + (i + 1).ToString() + ")");
                    return false;
                }
                
                if (source[i].Price.ToSafeDecimal() == 0)
                {
                    ShowErrorMsg("فی را وارد نمایید(ردیف " + (i + 1).ToString() + ")");
                    return false;
                }

                if (source[i].GoodsUnitId.ToSafeInt() == 0)
                {
                    ShowErrorMsg("واحد را وارد نمایید(ردیف " + (i + 1).ToString() + ")");
                    return false;
                }
            }

            return true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.RedirectToRoute("FactorList");
        }

        protected void gridFactor_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Insert")
            {

            }
            else if (e.CommandName == "Update")
            {

            }
        }

        protected void gridFactor_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void gridFactor_OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            gridFactor.EditIndex = e.NewEditIndex;
            BindData();
        }

        private void BindData()
        {
            gridFactor.DataSource = Session["GridSource"];
            gridFactor.DataBind();
        }
        protected void gridFactor_OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                var desValue = ((TextBox)gridFactor.Rows[gridFactor.EditIndex].Cells[1].Controls[1]).Text;
                var unitValue = ((DropDownList)gridFactor.Rows[gridFactor.EditIndex].Cells[2].Controls[1]).SelectedValue;
                var countValue = ((TextBox)gridFactor.Rows[gridFactor.EditIndex].Cells[3].Controls[1]).Text;
                //var weightValue = ((TextBox)gridFactor.Rows[gridFactor.EditIndex].Cells[4].Controls[1]).Text;
                var priceValue = ((TextBox)gridFactor.Rows[gridFactor.EditIndex].Cells[5].Controls[1]).Text;

                var fd = ((List<FactorDetail>)Session["GridSource"]).SingleOrDefault(a => a.RowNumber == e.RowIndex + 1);

                fd.Description = desValue;
                fd.GoodsUnitId = unitValue.ToSafeInt();
                fd.Count = countValue.ToSafeDecimal();
                //fd.Weight = weightValue.ToSafeDecimal();
                fd.Price = priceValue.ToSafeDecimal();

                gridFactor.EditIndex = -1;
                BindData();
            }
            catch (Exception ex)
            {
                Debuging.Error(ex, MethodBase.GetCurrentMethod().Name);
                ShowErrorMsg(MessageText.UNKNOWN_ERROR);
            }
        }

        protected string GetGoodsUnitName(int goodsUnitId)
        {
            var res = new GoodsUnitRepository().GetById(goodsUnitId);

            if (res != null)
                return res.Name;

            return "";
        }

        protected void gridFactor_OnRowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gridFactor.EditIndex = -1;
            BindData();
        }

        protected void gridFactor_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var fd = ((List<FactorDetail>)Session["GridSource"]).SingleOrDefault(a => a.RowNumber == e.RowIndex + 1);
            ((List<FactorDetail>)Session["GridSource"]).Remove(fd);
            ((List<FactorDetail>)Session["GridSource"]).Where(a => a.RowNumber > fd.RowNumber).ToList().ForEach(a => a.RowNumber = a.RowNumber - 1);
            gridFactor.EditIndex = -1;
            BindData();
        }

        protected void btnAddFirstRow_OnClick(object sender, ImageClickEventArgs e)
        {
            try
            {
                var newRow = new FactorDetail();
                newRow.RowNumber = 1;
                var source = new List<FactorDetail>();
                source.Add(newRow);

                Session["GridSource"] = source;

                gridFactor.EditIndex = 0;
                BindData();
                //((TextBox)gridFactor.Rows[gridFactor.EditIndex].Cells[1].Controls[1]).Focus();
                //BindFooterGoodsUnitDrp();
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
                {
                    var count = ((Label)e.Row.Cells[3].Controls[1]).Text.ToSafeDecimal(); //count
                    var price = ((Label)e.Row.Cells[5].Controls[1]).Text.ToSafeDecimal(); //price
                    ((Label) e.Row.Cells[6].Controls[1]).Text = (count*price).ToString("N0");

                    //((Label)gridFactor.FooterRow.Cells[6].Controls[1]).Text = fds.Sum(a => a.Count * a.Price).ToSafeString().ToFaGString();

                    UpdateTxtTotalPrice();
                }
            }
            catch (Exception ex)
            {
                Debuging.Error(ex, MethodBase.GetCurrentMethod().Name);
                ShowErrorMsg(MessageText.UNKNOWN_ERROR);
            }
        }

        private void UpdateTxtTotalPrice()
        {
            var fds = ((List<FactorDetail>) Session["GridSource"]);
            txtTotalPrice.Text = fds.Sum(a => a.Count*a.Price).ToSafeString().ToFaGString();
        }

        protected void btnAddFooter_OnClick(object sender, ImageClickEventArgs e)
        {
            try
            {
                var desValue = ((TextBox)gridFactor.FooterRow.Cells[1].Controls[1]).Text;
                var unitValue = ((DropDownList)gridFactor.FooterRow.Cells[2].Controls[1]).SelectedValue;
                var countValue = ((TextBox)gridFactor.FooterRow.Cells[3].Controls[1]).Text;
                //var weightValue = ((TextBox)gridFactor.FooterRow.Cells[4].Controls[1]).Text;
                var priceValue = ((TextBox)gridFactor.FooterRow.Cells[5].Controls[1]).Text;

                var fd = new FactorDetail();
           
                fd.RowNumber = ((List<FactorDetail>) Session["GridSource"]).Max(a => a.RowNumber) + 1;
                fd.Description = desValue;
                fd.GoodsUnitId = unitValue.ToSafeInt();
                fd.Count = countValue.ToSafeDecimal();
                //fd.Weight = weightValue.ToSafeDecimal();
                fd.Price = priceValue.ToSafeDecimal();

                ((List<FactorDetail>) Session["GridSource"]).Add(fd);
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
    }
}