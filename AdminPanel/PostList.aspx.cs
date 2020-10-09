using System;
using System.Web.UI.WebControls;
using Common;
using Repository.DAL;
using Repository.Entity.Domain;

namespace AdminPanel
{
    public partial class PostList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var repo = new PostRepository();
                gridPosts.DataSource = repo.GetUserPosts(((User) Session["UserName"]).Id);
                gridPosts.DataBind();
            }
        }

        protected void gridPost_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditPost")
            {
                Response.Redirect("Post.aspx?Id=" + e.CommandArgument);
            }
            else
                if (e.CommandName == "DeletePost")
                {
                    var msg = Utility.AesEncrypt("آیا از حذف مطلب اطمینان دارید؟");
                    //var aqn = Utility.AesEncrypt(typeof(ProductCategory).AssemblyQualifiedName);
                    var table = Utility.AesEncrypt("Posts");
                    Response.Redirect("Confirmation.aspx?Id=" + e.CommandArgument + "&m=" + msg + "&t=" + table);
                }
        }

        protected void btnAddPost_Click(object sender, EventArgs e)
        {
            Response.Redirect("Post.aspx");
        }
    }
}