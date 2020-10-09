using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using Repository.DAL;
using Repository.Data;
using Repository.Entity.Domain;

namespace MehranPack
{
    public partial class ProcessCategoryTiming : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["Result"] = gridList.DataSource = new ProcessCategoryRepository().GetCatProcessTimingList().OrderBy(a => a.CategoryName).ThenBy(a=>a.ProcessName).ToList();
                gridList.DataBind();
            }
        }

        
        protected void gridList_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridList.PageIndex = e.NewPageIndex;
            gridList.DataSource = Session["Result"];
            gridList.DataBind();
        }

        
    }
}