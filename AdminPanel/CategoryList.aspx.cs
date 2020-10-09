using System;
using System.Web.UI.WebControls;
using Common;
using Repository.DAL;

namespace AdminPanel
{
    public partial class CategoryList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CategoryRepository repo = new CategoryRepository();
                gridCategory.DataSource= repo.GetAllWithParents();
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
                var msg = Utility.AesEncrypt("آیا از حذف گروه اطمینان دارید؟");
                //var aqn = Utility.AesEncrypt(typeof(Category).AssemblyQualifiedName);
                var table = Utility.AesEncrypt("Categories");
                Response.Redirect("Confirmation.aspx?Id=" + e.CommandArgument + "&m=" + msg + "&t=" + table);
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