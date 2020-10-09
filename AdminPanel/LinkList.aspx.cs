using System;
using System.Web.UI.WebControls;
using Common;
using Repository.DAL;

namespace AdminPanel
{
    public partial class LinkRepositoryList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var repo = new LinkRepository();
                gridLinks.DataSource = repo.GetAll();
                gridLinks.DataBind();
            }
        }

        protected void gridLinks_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditLink")
            {
                Response.Redirect("Link.aspx?Id=" + e.CommandArgument);
            }
            else
                if (e.CommandName == "DeleteLink")
                {
                    var msg = Utility.AesEncrypt("آیا از حذف لینک اطمینان دارید؟");
                    //var aqn = Utility.AesEncrypt(typeof(Category).AssemblyQualifiedName);
                    var table = Utility.AesEncrypt("Posts");
                    Response.Redirect("Confirmation.aspx?Id=" + e.CommandArgument + "&m=" + msg + "&t=" + table);
                }
        }

        protected void btnAddLink_Click(object sender, EventArgs e)
        {
            Response.Redirect("Link.aspx");
        }
    }
}