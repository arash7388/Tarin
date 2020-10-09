using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using Repository.DAL;
using Repository.Entity.Domain;

namespace MehranPack
{
    public partial class InputOutput : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Page.RouteData.Values["Id"].ToSafeInt() != 0)
                {
                    var repo = new InputOutputRepository();
                    var tobeEditedIO = repo.GetByIdWithDetails(Page.RouteData.Values["Id"].ToSafeInt());

                    dtInputOutput.Date = tobeEditedIO.InsertDateTime.ToString();
                    txtInReceiptId.Text = tobeEditedIO.ReceiptId.ToString();

                    var details = new List<InputHelper>();

                    foreach (InputOutputDetail iod in tobeEditedIO.InputOutputDetails)
                    {
                        details.Add(new InputHelper()
                        {
                            ProductId = iod.ProductId,
                            Code = iod.Product.Code,
                            Name = iod.Product.Name,
                            CategoryName = new CategoryRepository().GetById(iod.Product.ProductCategoryId).Name,
                            Count = iod.Count,
                            CustomerName = new CustomerRepository().GetById(iod.CustomerId).Name,
                            CustomerId = (int)iod.CustomerId,
                            InsertDateTime = ((DateTime)iod.InsertDateTime).ToFaDate(),
                            ProductionQuality = GetProductionQualityTextByValue((int)iod.ProductionQuality),
                            ProductionQualityId = (int) iod.ProductionQuality
                        });
                    }

                    Session["GridSource"] = details;
                }
                else
                    Session["GridSource"] = new List<InputHelper>();

                dtInputOutputDetail.LoadCurrentDateTime = true;
                dtInputOutput.LoadCurrentDateTime = true;

                BindDrpCustomer();

                h3Header.InnerText = Page.RouteData.Values["Type"].ToSafeString() == "In" ? "ثبت ورود کالا" : "ثبت خروج کالا";
                divReceiptId.Visible = Page.RouteData.Values["Type"].ToSafeString() == "Out";
            }

            BindTreeview();
            tv1.ExpandAll();

            if (Session["GridSource"] == null)
                Session["GridSource"] = new List<InputHelper>();

            gridInput.DataSource = Session["GridSource"];
            gridInput.DataBind();
        }

        private bool IsInputReceipt()
        {
            return Page.RouteData.Values["Type"].ToSafeString() == "In";
        }
        private string GetProductionQualityTextByValue(int value)
        {
            switch (value)
            {
                case 1: return "بد";
                case 2: return "متوسط";
                case 3: return "خوب";
                default: return "";
            }
        }

        private void BindDrpCustomer()
        {
            var source = new CustomerRepository().GetAll().ToList();
            drpCustomer.DataSource = source;
            drpCustomer.DataValueField = "Id";
            drpCustomer.DataTextField = "Name";
            drpCustomer.DataBind();
        }

        private void BindTreeview()
        {
            var hierarchicalData = new CategoryRepository().GethierarchicalTree(null,true);
            tv1.Nodes.Clear();
            var root = new CustomTreeNode("گروه ها", "0", "", "", "");
            tv1.Nodes.Add(root);
            BindTreeRecursive(hierarchicalData, root);
        }

        private void BindTreeRecursive(List<Repository.Entity.Domain.Category> hierarchicalData, TreeNode node)
        {
            foreach (Repository.Entity.Domain.Category category in hierarchicalData)
            {
                if (category.Children.Any())
                {
                    var n = new CustomTreeNode(TreeNodeSelectAction.None, category.Name + "(" + category.Code + ")", "", "", category.Id.ToString(), category.Name);
                    node.ChildNodes.Add(n);
                    BindTreeRecursive(category.Children.ToList(), n);
                }
                else
                {
                    var n = new CustomTreeNode(category.Name, "", "", category.Id.ToString(), category.Name);
                    node.ChildNodes.Add(n);
                    if (n.Parent != null) n.Parent.SelectAction = TreeNodeSelectAction.None;

                    if (new ProductRepository().Get(a => a.ProductCategoryId == category.Id).Any())
                    {
                        var catRelatedProducts = new ProductRepository().Get(a => a.ProductCategoryId == category.Id).ToList();
                        n.SelectAction = TreeNodeSelectAction.None;

                        foreach (Repository.Entity.Domain.Product product in catRelatedProducts)
                        {
                            if (string.IsNullOrWhiteSpace(txtSearchTree.Text))
                                n.ChildNodes.Add(new CustomTreeNode(product.Name + "(" + product.Code + ")", product.Id.ToString(), product.Name, product.ProductCategoryId.ToString(), product.ProductCategory?.Name));
                            else if (product.Name.Contains(txtSearchTree.Text) || product.Code.Contains(txtSearchTree.Text))
                                n.ChildNodes.Add(new CustomTreeNode(product.Name + "(" + product.Code + ")", product.Id.ToString(), product.Name, product.ProductCategoryId.ToString(), product.ProductCategory?.Name));
                        }
                    }
                }
            }
        }

        protected void b1_OnClick(object sender, EventArgs e)
        {
            BindTreeview();
            tv1.ExpandAll();
        }

        protected void gridSource_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                var s = (List<InputHelper>)Session["GridSource"];
                var tobeDeleted = s.SingleOrDefault(a => a.ProductId == e.CommandArgument.ToSafeInt());
                if (tobeDeleted != null)
                    s.Remove(tobeDeleted);
                BindGrid();
            }
        }

        private void BindGrid()
        {
            gridInput.DataSource = Session["GridSource"];
            gridInput.DataBind();
        }

        protected void gridSource_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void tv1_OnSelectedNodeChanged(object sender, EventArgs e)
        {

        }

        protected void btnAdd_OnClick(object sender, EventArgs e)
        {
            if (!ValidateForm(HttpContext.Current.Session["ProductId"].ToSafeInt()))
                return;

            var p = new ProductRepository().GetById(HttpContext.Current.Session["ProductId"].ToSafeInt());
            var addedInput = new InputHelper()
            {
                ProductId = p.Id,
                Name = p.Name,
                Code = p.Code,
                Count = txtCount.Text.ToSafeInt(),
                CategoryName = new CategoryRepository().GetById(p.ProductCategoryId).Name,
                CustomerId = drpCustomer.SelectedValue.ToSafeInt(),
                CustomerName = drpCustomer.SelectedItem.Text,
                InsertDateTime = dtInputOutputDetail.Date,
                ProductionQuality = GetProductionQualityText(),
                ProductionQualityId = rbQuality.SelectedValue.ToSafeInt()
            };

            if (Session["GridSource"] == null)
                Session["GridSource"] = new List<InputHelper>();

            ((List<InputHelper>)Session["GridSource"]).Add(addedInput);
            gridInput.DataSource = Session["GridSource"];
            gridInput.DataBind();
            ClearTextBoxes();
        }

        private string GetProductionQualityText()
        {
            switch (rbQuality.SelectedValue.ToSafeInt())
            {
                case 1:
                    return "بد";

                case 2:
                    return "متوسط";

                case 3:
                    return "خوب";

                 default:
                    return "";
            }
        }

        private bool ValidateForm(int pid)
        {
            if (txtProductCode.Text.ToSafeString() == "" || txtCount.Text.ToSafeString() == "")
            {
                ((Main)Page.Master).SetGeneralMessage("اطلاعات کالا را وارد نمایید", MessageType.Error);
                return false;
            }

            if (((List<InputHelper>)Session["GridSource"]).Any(a => a.ProductId == pid))
            {
                ((Main)Page.Master).SetGeneralMessage("کالای وارد شده تکراری است", MessageType.Error);
                return false;
            }

            if (drpCustomer.SelectedValue.ToSafeInt() == 0)
            {
                ((Main)Page.Master).SetGeneralMessage("مشتری را انتخاب نمایید", MessageType.Error);
                return false;
            }

            if(rbQuality.SelectedIndex==-1)
            {
                ((Main)Page.Master).SetGeneralMessage("کیفیت را انتخاب نمایید", MessageType.Error);
                return false;
            }

            if (!IsInputReceipt())
            {
                var supply = GetProductSupply(pid);
                if (supply - txtCount.Text.ToSafeInt() < 0)
                {
                    ((Main)Page.Master).SetGeneralMessage("موجودی ناکافی ، موجودی فعلی : " + supply, MessageType.Error);
                    return false;
                }

                var negativePIDs = new InputOutputDetailRepository().CheckSupplyForNextOutputsBeforeInsertOutput(
                    txtCount.Text.ToSafeInt(), pid, Utility.AdjustTimeOfDate(dtInputOutputDetail.Date.ToEnDate()));

                if (negativePIDs.Any())
                {
                    ((Main)Page.Master).SetGeneralMessage("رسید های بعدی نامعتبر(دارای موجودی منفی) خواهند شد" , MessageType.Error);
                    return false;
                }
            }

            ((Main)Page.Master).HideGeneralMessage();

            return true;
        }

        private int GetProductSupply(int pid)
        {
            return new InputOutputDetailRepository().GetProductSupply(pid, Utility.AdjustTimeOfDate(dtInputOutputDetail.Date.ToEnDate()));
        }

        private void ClearTextBoxes()
        {
            txtSearchTree.Text = txtCount.Text = txtProductName.Text = txtProductCode.Text = "";
            hfProductId.Value = null;
        }

        [WebMethod]
        public static string GetProductCodeAndName(int productId) //it sets p id too
        {
            var p = new ProductRepository().GetById(productId);
            if (p != null)
            {
                HttpContext.Current.Session["ProductId"] = p.Id;
                return p.Code + "," + p.Name;
            }

            return "";
        }

        [WebMethod]
        public static string GetCategoryName(int catId)
        {

            return new CategoryRepository().GetById(catId)?.Name;
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            var source = (List<InputHelper>)Session["GridSource"];

            if (source.Count == 0)
            {
                ((Main)Page.Master).SetGeneralMessage("اطلاعاتی برای ذخیره کردن یافت نشد", MessageType.Error);
                return;
            }
            var uow = new UnitOfWork();

            if (Page.RouteData.Values["Id"].ToSafeInt() == 0)
            {
                var io = new Repository.Entity.Domain.InputOutput();
                if (divReceiptId.Visible)
                    io.ReceiptId = txtInReceiptId.Text.ToSafeInt();

                var insDateTime = Utility.AdjustTimeOfDate(dtInputOutput.Date.ToEnDate());
                io.InsertDateTime = insDateTime;
                io.UserId = ((User)Session["User"]).Id;
                io.Status = -1;
                io.InOutType = Page.RouteData.Values["Type"].ToSafeString() == "In" ? (int)InOutType.In : (int)InOutType.Out;
                io.InputOutputDetails = CastToIODetail((List<InputHelper>)Session["GridSource"]);
                
                uow.InputOutputs.Create(io);
            }
            else
            {
                var tobeEdited = uow.InputOutputs.GetById(Page.RouteData.Values["Id"].ToSafeInt());

                if (divReceiptId.Visible)
                    tobeEdited.ReceiptId = txtInReceiptId.Text.ToSafeInt();

                uow.InputOutputDetails.Delete(a=>a.InputOutputId== tobeEdited.Id);
                
                tobeEdited.InsertDateTime = Utility.AdjustTimeOfDate(dtInputOutput.Date.ToEnDate());

                foreach (InputHelper item in source)
                {
                    uow.InputOutputDetails.Create(new InputOutputDetail()
                    {
                        Count = item.Count,
                        CustomerId = item.CustomerId,
                        InsertDateTime = Utility.AdjustTimeOfDate(item.InsertDateTime.ToEnDate()),
                        Status = -1,
                        ProductId = item.ProductId,
                        UpdateDateTime = DateTime.Now,
                        InputOutputId = tobeEdited.Id,
                        ProductionQuality = item.ProductionQualityId
                    });
                }
            }

            var result = uow.SaveChanges();
            if (result.IsSuccess)
            {
                RedirectFactorListActionResultWithMessage();
            }
            else
            {
                ((Main)Page.Master).SetGeneralMessage("خطا در ذخیره اطلاعات", MessageType.Error);
                Debuging.Error(result.Message);
            }
        }

       
        private void RedirectFactorListActionResultWithMessage()
        {
            var res = new Common.ActionResult();

            res.ResultMessage = MessageText.SUCCESS;
            res.IsSuccess = true;
            Session["SaveIOActionResult"] = res;

            var routeValues = new RouteValueDictionary();
            routeValues.Add("ActionResult", true);
            routeValues.Add("Type", Page.RouteData.Values["Type"].ToString());
            Response.RedirectToRoute("InputOutputListActionResult", routeValues);
        }

        private ICollection<InputOutputDetail> CastToIODetail(List<InputHelper> inputHelpers)
        {
            var result = new List<InputOutputDetail>();

            foreach (InputHelper inputHelper in inputHelpers)
            {
                result.Add(new InputOutputDetail()
                {
                    Count = inputHelper.Count,
                    CustomerId = inputHelper.CustomerId,
                    ProductId = inputHelper.ProductId,
                    InsertDateTime = Utility.AdjustTimeOfDate(inputHelper.InsertDateTime.ToEnDate()),
                    Status = -1,
                    ProductionQuality = inputHelper.ProductionQualityId
                });
            }

            return result;
        }

        protected void gridInput_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gridInput_OnRowDeleted(object sender, GridViewDeletedEventArgs e)
        {

        }

        protected void btnClearSearch_OnClick(object sender, EventArgs e)
        {
            txtSearchTree.Text = "";
            BindTreeview();
        }
    }

    public class InputHelper
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public int Count { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string InsertDateTime { get; set; }
        public string Code { get; set; }
        public string ProductionQuality { get; set; }
        public int ProductionQualityId { get; set; }
    }

    public class CustomTreeNode : TreeNode
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string CatId { get; set; }
        public string CatName { get; set; }
        public CustomTreeNode()
        {
        }

        public CustomTreeNode(TreeNodeSelectAction selectAction, string text, string productId, string productName, string catId, string catName) : base(text, productId)
        {
            SelectAction = selectAction;
            this.CatId = catId;
            this.CatName = catName;
            this.ProductId = productId;
            this.ProductName = productName;
        }

        public CustomTreeNode(string text, string productId, string productName, string catId, string catName) : base(text, productId)
        {
            this.CatId = catId;
            this.CatName = catName;
            this.ProductId = productId;
            this.ProductName = productName;
        }

        protected override void RenderPreText(HtmlTextWriter writer)
        {
            writer.Write("<span onclick='onNodeClicked(" + this.Value + "," + this.CatId + ")' >");
            base.RenderPreText(writer);
        }

        protected override void RenderPostText(HtmlTextWriter writer)
        {
            writer.Write("</span>");
            //base.RenderPostText(writer);

        }


    }
}