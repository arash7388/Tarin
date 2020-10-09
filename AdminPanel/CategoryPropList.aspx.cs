using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using Repository.DAL;

namespace AdminPanel
{
    public partial class CategoryPropList : System.Web.UI.Page
    {
        public List<KeyValuePair<int,string>> CategoryList { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CategoryList = new CategoryRepository().GetAllIdName();
                gridCategoryProp.DataSource = new CategoryPropRepository().GetAll();
                gridCategoryProp.DataBind();
                Session["Result"] = gridCategoryProp.DataSource;
            }
        }

        protected void gridCategoryProp_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        { 
            gridCategoryProp.PageIndex = e.NewPageIndex;
            gridCategoryProp.DataSource = Session["Result"];
            gridCategoryProp.DataBind(); 
        }

        protected void gridCategoryProp_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                Response.Redirect("CategoryProp.aspx?Id=" + e.CommandArgument);
            }
            else
                if (e.CommandName == "Delete")
                {
                    var msg = Utility.AesEncrypt("آیا از حذف ویژگی اطمینان دارید؟");
                    //var aqn = Utility.AesEncrypt(typeof(Category).AssemblyQualifiedName);
                    var table = Utility.AesEncrypt("CategoryProps");
                    Response.Redirect("Confirmation.aspx?Id=" + e.CommandArgument + "&m=" + msg + "&t=" + table);
                }
        }

        protected void btnAddCat_Click(object sender, EventArgs e)
        {
            Response.Redirect("CategoryProp.aspx");
        }
    }
}