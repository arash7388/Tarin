using Common;
using Repository.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MehranPack
{
    public partial class ProcessCategory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                List<ProcessCategoryHelper> tobeEditedPC = new List<ProcessCategoryHelper>();

                if (Page.RouteData.Values["Id"].ToSafeInt() != 0)
                {
                    drpCat.Enabled = false;
                    var repo = new ProcessCategoryRepository();
                    tobeEditedPC = repo.GetByCatIdWithDetails(Page.RouteData.Values["Id"].ToSafeInt());
                    var details = new List<ProcessCategoryHelper>();

                    foreach (ProcessCategoryHelper pc in tobeEditedPC)
                    {
                        if(pc.ProcessId!=999)
                        details.Add(new ProcessCategoryHelper()
                        {
                            Id = pc.Id,
                            CategoryId = pc.CategoryId,
                            CategoryName = pc.CategoryName,
                            ProcessId = pc.ProcessId,
                            ProcessName = pc.ProcessName,
                            Order = pc.Order,
                            ProcessTime = pc.ProcessTime
                        });
                    }

                    Session["GridSource"] = details;
                }
                else
                    Session["GridSource"] = new List<ProcessCategoryHelper>();

                BindDrpCat();

                if (Page.RouteData.Values["Id"].ToSafeInt() != 0)
                {
                    drpCat.SelectedValue = tobeEditedPC.FirstOrDefault().CategoryId.ToString();
                }

                BindDrpProcess();
            }

            if (Session["GridSource"] == null)
                Session["GridSource"] = new List<ProcessCategoryHelper>();

            gridInput.DataSource = Session["GridSource"];
            gridInput.DataBind();
        }

        protected void gridSource_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                var s = (List<ProcessCategoryHelper>)Session["GridSource"];
                var tobeDeleted = s.SingleOrDefault(a => a.ProcessId == e.CommandArgument.ToSafeInt());
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

        private void BindDrpProcess()
        {
            var source = new ProcessRepository().GetAll().Where(a=>a.Id!=999).ToList();
            drpProcesses.DataSource = source;
            drpProcesses.DataValueField = "Id";
            drpProcesses.DataTextField = "Name";
            drpProcesses.DataBind();
        }

        private void BindDrpCat()
        {
            var source = new CategoryRepository().GetAllWithFullName().ToList();
            drpCat.DataSource = source;
            drpCat.DataValueField = "Id";
            drpCat.DataTextField = "Name";
            drpCat.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var catId = drpCat.SelectedValue.ToSafeInt();
                var existingCatPr = new ProcessCategoryRepository().Get(a => a.CategoryId == catId).FirstOrDefault();
                if (Page.RouteData.Values["Id"].ToSafeInt() == 0 && existingCatPr != null) throw new LocalException("duplicate cat", " فرآیندهای این گروه محصول قبلا ثبت شده اند");

                var gridSource = (List<ProcessCategoryHelper>)Session["GridSource"];
                var dupOrder = gridSource.GroupBy(a => a.Order).Where(a => a.Count() > 1).Count();

                if (dupOrder > 0)
                    throw new LocalException("duplicate order", "ترتیب تکراری در سطرهای ثبت شده");

                //foreach(var item in gridSource)
                //{

                //}

                UnitOfWork uow = new UnitOfWork();

                if (Page.RouteData.Values["Id"].ToSafeInt() == 0)
                {
                    foreach (var item in ((List<ProcessCategoryHelper>)Session["GridSource"]))
                    {
                        var newPC = new Repository.Entity.Domain.ProcessCategory()
                        {
                            CategoryId = item.CategoryId,
                            ProcessId = item.ProcessId,
                            Order = item.Order,
                            ProcessTime = item.ProcessTime
                        };

                        uow.ProcessCategories.Create(newPC);
                    }

                    if(!((List<ProcessCategoryHelper>)Session["GridSource"]).Any(a=>a.ProcessId==999)) //اتمام موقت
                        {
                        var newPC = new Repository.Entity.Domain.ProcessCategory()
                        {
                            CategoryId = drpCat.SelectedValue.ToSafeInt(),
                            ProcessId = 999,
                            Order = 999,
                            ProcessTime = 0
                        };

                        uow.ProcessCategories.Create(newPC);
                    }
                }
                else
                {
                    var repo = uow.ProcessCategories;
                    var exsitedPCs = repo.Get(a => a.CategoryId == catId && a.ProcessId!=999).ToList();

                    foreach (Repository.Entity.Domain.ProcessCategory item in exsitedPCs)
                    {
                        repo.Delete(item.Id);
                    }

                    var res = uow.SaveChanges();
                    if (!res.IsSuccess)
                    {
                        ((Main)Page.Master).SetGeneralMessage(res.ResultMessage, MessageType.Error);
                        return;
                    }

                    foreach (var item in ((List<ProcessCategoryHelper>)Session["GridSource"]))
                    {
                        var newPC = new Repository.Entity.Domain.ProcessCategory()
                        {
                            CategoryId = item.CategoryId,
                            ProcessId = item.ProcessId,
                            Order = item.Order,
                            ProcessTime = item.ProcessTime
                        };

                        uow.ProcessCategories.Create(newPC);
                    }
                }

                var result = uow.SaveChanges();
                if (result.IsSuccess)
                    ((Main)Page.Master).SetGeneralMessage("اطلاعات با موفقیت ذخیره شد", MessageType.Success);
                else
                    ((Main)Page.Master).SetGeneralMessage(result.ResultMessage, MessageType.Error);

                ClearControls();
            }
            catch (LocalException ex)
            {
                ((Main)Page.Master).SetGeneralMessage("خطا در دخیره سازی -" + ex.ResultMessage, MessageType.Error);
            }
        }

        private void ClearControls()
        {
            drpCat.Enabled = drpProcesses.Enabled = txtOrder.Enabled = txtProcessTime.Enabled= false;
            gridInput.Enabled = false;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.RedirectToRoute("Home");
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtOrder.Text)) throw new LocalException("order is empty", "ترتیب  را وارد نمایید");
                if (string.IsNullOrEmpty(txtProcessTime.Text)) throw new LocalException("time is empty", "زمان  را وارد نمایید");
                if (txtOrder.Text.ToSafeInt() == 0) throw new LocalException("order is empty", "ترتیب  باید عددی مثبت باشد ");
                if (txtProcessTime.Text.ToSafeInt() == 0) throw new LocalException("time is empty", "زمان  باید عددی مثبت باشد ");
                var gridSource = (List<ProcessCategoryHelper>)Session["GridSource"];

                if (gridSource != null && gridSource.Any() && gridSource.Any(a => a.Order == txtOrder.Text.ToSafeInt()))
                    throw new LocalException("duplicate order", "ترتیب نباید تکراری باشد");

                if (gridSource != null && gridSource.Any() && gridSource.Any(a => a.ProcessId == drpProcesses.SelectedValue.ToSafeInt()))
                    throw new LocalException("duplicate process", "فرآیند نباید تکراری باشد");

                var maxOrder = gridSource.Any() ? gridSource.Where(a => a.ProcessId != 999).Max(a => a.Order) : 0;

                if (txtOrder.Text.ToSafeInt() - maxOrder != 1)
                    throw new LocalException("invalid order(other than +1)", $"ترتیب ها باید پشت سر هم ثبت شوند. ترتیب قابل قبول بعدی {maxOrder + 1} است ");


                var addedInput = new ProcessCategoryHelper()
                {
                    CategoryId = drpCat.SelectedValue.ToSafeInt(),
                    ProcessId = drpProcesses.SelectedValue.ToSafeInt(),
                    ProcessName = new ProcessRepository().GetById(drpProcesses.SelectedValue.ToSafeInt()).Name,
                    Order = txtOrder.Text.ToSafeInt(),
                    ProcessTime=txtProcessTime.Text.ToSafeInt()
                };

                if (Session["GridSource"] == null)
                    Session["GridSource"] = new List<ProcessCategoryHelper>();

                ((List<ProcessCategoryHelper>)Session["GridSource"]).Add(addedInput);
                gridInput.DataSource = Session["GridSource"];
                gridInput.DataBind();
                ((Main)Page.Master).HideGeneralMessage();

            }
            catch (LocalException ex)
            {
                ((Main)Page.Master).SetGeneralMessage(ex.ResultMessage, MessageType.Error);
            }
        }

        protected void gridInput_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gridInput_OnRowDeleted(object sender, GridViewDeletedEventArgs e)
        {

        }
    }
}