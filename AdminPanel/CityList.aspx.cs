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
    public partial class CityList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var repo = new CityRepository();
                gridCity.DataSource = repo.GetAllByOrder();
                gridCity.DataBind();
                Session["Result"] = gridCity.DataSource;
            }
        }

        protected void gridCity_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridCity.PageIndex = e.NewPageIndex;
            gridCity.DataSource = Session["Result"];
            gridCity.DataBind();
        }

        protected void gridCity_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                Response.Redirect("City.aspx?Id=" + e.CommandArgument);
            }
            else
                if (e.CommandName == "Delete")
                {
                    var msg = Utility.AesEncrypt("آیا از حذف شهر اطمینان دارید؟");
                    //var aqn = Utility.AesEncrypt(typeof(Category).AssemblyQualifiedName);
                    var table = Utility.AesEncrypt("Cities");
                    Response.Redirect("Confirmation.aspx?Id=" + e.CommandArgument + "&m=" + msg + "&t=" + table);
                }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("City.aspx");
        }
    }
}