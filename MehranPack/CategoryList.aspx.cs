using System;
using System.Web.UI.WebControls;
using Common;
using Repository.DAL;

namespace MehranPack
{
    public partial class CategoryList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CategoryRepository repo = new CategoryRepository();
                if (Utility.IsInOutMode())
                    gridCategory.DataSource = repo.GetAllWithParentsForInOut();
                else
                    gridCategory.DataSource = repo.GetAllWithParents();


                gridCategory.DataBind();
                Session["Result"] = gridCategory.DataSource;
            }
        }

        protected void gridCategory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditCat")
            {
                Response.Redirect("Category.aspx?Id=" + e.CommandArgument);
            }
            else
            if (e.CommandName == "DeleteCat")
            {
                var data = new ConfirmData();

                data.Command = "Delete";
                data.Id = e.CommandArgument.ToSafeInt();
                data.Msg = "آیا از حذف اطمینان دارید؟";
                data.Table = "Categories";
                data.RedirectAdr = "CategoryList.aspx";

                Session["ConfirmData"] = data;
                Response.RedirectToRoute("Confirmation");
                Response.End();
            }
        }

        protected void btnAddCat_Click(object sender, EventArgs e)
        {
            Response.Redirect("Category.aspx");
        }

        protected void gridCategory_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridCategory.PageIndex = e.NewPageIndex;
            gridCategory.DataSource = Session["Result"];
            gridCategory.DataBind();
        }
    }
}