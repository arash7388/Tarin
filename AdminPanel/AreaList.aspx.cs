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
    public partial class AreaList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var repo = new AreaRepository();
                gridArea.DataSource = repo.GetAllByOrder();
                gridArea.DataBind();
                Session["Result"] = gridArea.DataSource;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("Area.aspx");
        }

        protected void gridArea_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                Response.Redirect("Area.aspx?Id=" + e.CommandArgument);
            }
            else
                if (e.CommandName == "Delete")
                {
                    var msg = Utility.AesEncrypt("آیا از حذف منطقه اطمینان دارید؟");
                    //var aqn = Utility.AesEncrypt(typeof(Category).AssemblyQualifiedName);
                    var table = Utility.AesEncrypt("Areas");
                    Response.Redirect("Confirmation.aspx?Id=" + e.CommandArgument + "&m=" + msg + "&t=" + table);
                }
        }

        protected void gridArea_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridArea.PageIndex = e.NewPageIndex;
            gridArea.DataSource = Session["Result"];
            gridArea.DataBind();
        }
    }
}