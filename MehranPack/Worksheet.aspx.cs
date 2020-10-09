using Common;
using Repository.DAL;
using Repository.Entity.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Web.Script.Services;

namespace MehranPack
{
    public partial class Worksheet : Page
    {
        [WebMethod]
        public static string GetProductCodeAndName(int productId) //it sets p id too
        {
            var p = new ProductRepository().GetById(productId);
            if (p != null)
            {
                HttpContext.Current.Session["ProductId"] = p.Id;
                var catRepo = new CategoryRepository();
                var cat = catRepo.Find(p.ProductCategoryId)?.Name;
                return p.Code + "," + cat + " " + p.Name;
            }

            return "";
        }

        [WebMethod]
        public static string HasDuplicateACode(string ACode)
        {
            //var q = HttpContext.Current.Request.QueryString[0];
            //todo in edit mode it counts the edited worksheet and is not right
            var existingAcode = new WorksheetDetailRepository().Get(a => a.ACode == ACode).FirstOrDefault();

            if (existingAcode != null)
            {
                return "true#" + existingAcode.WorksheetId;
            }

            return "false";
        }

        [WebMethod]
        public static string GetCategoryName(int catId)
        {
            return new CategoryRepository().GetById(catId)?.Name;
        }

        [WebMethod]
        public static string AddProduct(int productId)
        {
            return "";
            //((List<WorksheetDetailHelper>)Session["GridSource"]).Add();
            //return new CategoryRepository().GetById(catId)?.Name;
        }

        [WebMethod]
        public static string GetDetails2(int id)
        {
            var repo = new WorksheetDetailRepository();
            var js = new JavaScriptSerializer();
            return js.Serialize(repo.Get(a => a.WorksheetId == id).ToList());
            //return new
        }

        [WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public static object GetDetails()
        {
            var repo = new WorksheetDetailRepository();
            var js = new JavaScriptSerializer();
            return js.Serialize(repo.GetAll().ToList());
            //return new
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindDrpUsers();
                BindDrpColors();

                if (Page.RouteData.Values["Id"].ToSafeInt() != 0)
                {
                    var repo = new WorksheetRepository();
                    var tobeEditedWorksheet = repo.GetByIdWithDetails(Page.RouteData.Values["Id"].ToSafeInt());

                    dtWorksheet.Date = tobeEditedWorksheet.Date.ToFaDateTime().ToString();
                    txtPart.Text = tobeEditedWorksheet.PartNo.ToString();
                    txtWaxNo.Text = tobeEditedWorksheet.WaxNo.ToSafeString();
                    drpColor.SelectedValue = tobeEditedWorksheet.ColorId.ToString();
                    drpOperator.SelectedValue = tobeEditedWorksheet.OperatorId.ToString();

                    var details = new List<WorksheetDetailHelper>();

                    foreach (WorksheetDetail d in tobeEditedWorksheet.WorksheetDetails)
                    {
                        details.Add(new WorksheetDetailHelper()
                        {
                            Id = d.Id,
                            ProductId = d.ProductId,
                            ProductName = d.Product.Name,
                            CategoryId = d.Product.ProductCategoryId,
                            CategoryName = new CategoryRepository().GetById(d.Product.ProductCategoryId).Name,
                            ACode = d.ACode
                            //InsertDateTime = ((DateTime)d.InsertDateTime).ToFaDate(),
                        });
                    }

                    Session["GridSource"] = details;
                }
                else
                {
                    dtWorksheet.LoadCurrentDateTime = true;
                    Session["GridSource"] = new List<InputHelper>();
                }


            }

            BindTreeview();
            tv1.CollapseAll();

            if (Session["GridSource"] == null)
                Session["GridSource"] = new List<WorksheetDetailHelper>();

        }

        private void BindDrpColors()
        {
            var source = new BaseRepository<Color>().GetAll().ToList();
            drpColor.DataSource = source;
            drpColor.DataValueField = "Id";
            drpColor.DataTextField = "Name";
            drpColor.DataBind(); ;
        }

        private void BindDrpUsers()
        {
            var source = new UserRepository().GetAll().ToList();
            drpOperator.DataSource = source;
            drpOperator.DataValueField = "Id";
            drpOperator.DataTextField = "FriendlyName";
            drpOperator.DataBind();
        }


        [WebMethod]
        public static string Save(int userId, string date, int id, Repository.Entity.Domain.Worksheet model)
        {
            if (model == null)
            {
                //((Main)Page.Master).SetGeneralMessage("اطلاعاتی برای ذخیره کردن یافت نشد", MessageType.Error);
                return "اطلاعاتی برای ذخیره کردن یافت نشد";
            }

            if (!model.WorksheetDetails.Any())
                return "هیچ ردیفی ثبت نشده است";

            //if(model.WorksheetDetails.GroupBy(a=>a.ProductId).Where(a => a.Count() > 1).Count()>0)
            //    return "ردیف با کالای تکراری ثبت شده است";

            var uow = new UnitOfWork();

            if (model.WorksheetDetails.GroupBy(a => a.ACode).Where(a => a.Count() > 1).Any())
                return "شناسه کالای تکراری در ردیف ها";

            if (id.ToSafeInt() == 0)
            {
                var w = new Repository.Entity.Domain.Worksheet();

                var insDateTime = model.InsertDateTime;
                w.Date = Utility.AdjustTimeOfDate(date.ToEnDate());
                w.PartNo = model.PartNo;
                w.WaxNo = model.WaxNo;
                w.InsertDateTime = insDateTime;
                w.OperatorId = model.OperatorId;
                w.ColorId = model.ColorId;
                w.UserId = userId;
                w.Status = -1;
                w.WorksheetDetails = model.WorksheetDetails;

                uow.Worksheets.Create(w);
            }
            else
            {
                var tobeEdited = uow.Worksheets.GetById(id.ToSafeInt());

                uow.WorksheetDetails.Delete(a => a.WorksheetId == tobeEdited.Id);

                tobeEdited.UpdateDateTime = DateTime.Now;
                tobeEdited.Date = Utility.AdjustTimeOfDate(date.ToEnDate());
                tobeEdited.OperatorId = model.OperatorId;
                tobeEdited.UserId = userId;
                tobeEdited.ColorId = model.ColorId;
                tobeEdited.PartNo = model.PartNo;
                tobeEdited.WaxNo = model.WaxNo;

                if(tobeEdited.OperatorId != model.OperatorId)
                {
                    var workLineRepo = new WorkLineRepository();
                    if(workLineRepo.Get(a=>a.WorksheetId== tobeEdited.Id).Any())
                        return "برای این کاربرگ در صف تولید دیتا ثبت شده و اوپراتور آن قابل تغییر نیست";
                }

                foreach (WorksheetDetail item in model.WorksheetDetails)
                {
                    uow.WorksheetDetails.Create(new WorksheetDetail()
                    {
                        Status = -1,
                        ProductId = item.ProductId,
                        UpdateDateTime = DateTime.Now,
                        WorksheetId = tobeEdited.Id,
                        ACode = item.ACode
                    });
                }
            }

            var result = uow.SaveChanges();
            if (result.IsSuccess)
            {
                //RedirectToWorksheetListActionResultWithMessage();
                return "OK";
            }
            else
            {
                //((Main)Page.Master).SetGeneralMessage("خطا در ذخیره اطلاعات", MessageType.Error);
                Debuging.Error(result.ResultCode + "," + result.Message + "," + result.Message);
                return "خطا در ذخیره اطلاعات";
            }
        }




        protected void gridSource_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                var s = (List<WorksheetDetailHelper>)Session["GridSource"];
                var tobeDeleted = s.SingleOrDefault(a => a.ProductId == e.CommandArgument.ToSafeInt());
                if (tobeDeleted != null)
                    s.Remove(tobeDeleted);
                BindGrid();
            }
        }

        private void BindGrid()
        {
            //gridInput.DataSource = Session["GridSource"];
            //gridInput.DataBind();
        }

        protected void b1_OnClick(object sender, EventArgs e)
        {
            BindTreeview();
            tv1.ExpandAll();
        }

        private void BindTreeview()
        {
            var hierarchicalData = new CategoryRepository().GethierarchicalTree();
            tv1.Nodes.Clear();
            var root = new CustomTreeNode("گروه ها", "0", "", "", "");
            tv1.Nodes.Add(root);
            BindTreeRecursive(hierarchicalData, root);
        }

        protected void tv1_OnSelectedNodeChanged(object sender, EventArgs e)
        {

        }

        protected void TreeView_TreeNodeExpanded(object sender, TreeNodeEventArgs e)
        {
            if (e.Node.Parent == null)
                return;
            string strNodeValue = e.Node.Value;
            foreach (TreeNode node in e.Node.Parent.ChildNodes)
            {
                if (node.Value != strNodeValue)
                {
                    node.Collapse();
                }
                else
                {
                    node.Expand();
                }
            }
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
    }
}
