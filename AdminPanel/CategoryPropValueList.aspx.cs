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
    public partial class CategoryPropValueList : System.Web.UI.Page
    {
        //public List<KeyValuePair<int, string>> CategoryList { get; set; }
        //public List<KeyValuePair<int, string>> CategoryPropList { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //CategoryList = new CategoryRepository().GetAllIdName();
                //CategoryPropList = new CategoryPropRepository().GetAllIdName();
                gridCategoryPropValue.DataSource = new CategoryPropValueRepository().GetAllByOrder();
                gridCategoryPropValue.DataBind();
                Session["Result"] = gridCategoryPropValue.DataSource;
            }

        }

        protected void gridCategoryPropValue_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                Response.Redirect("CategoryPropValue.aspx?Id=" + e.CommandArgument);
            }
            else
                if (e.CommandName == "Delete")
                {
                    var msg = Utility.AesEncrypt("آیا از حذف مقدار ویژگی اطمینان دارید؟");
                    //var aqn = Utility.AesEncrypt(typeof(Category).AssemblyQualifiedName);
                    var table = Utility.AesEncrypt("CategoryPropVlaues");
                    Response.Redirect("Confirmation.aspx?Id=" + e.CommandArgument + "&m=" + msg + "&t=" + table);
                }
        }

        protected void gridCategoryPropValue_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridCategoryPropValue.PageIndex = e.NewPageIndex;
            gridCategoryPropValue.DataSource = Session["Result"];
            gridCategoryPropValue.DataBind(); 
        }

        protected void btnAddCat_Click(object sender, EventArgs e)
        {
            Response.Redirect("CategoryPropValue.aspx");
        }
    }
}