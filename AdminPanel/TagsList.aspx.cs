using System;
using System.Web.UI.WebControls;
using Common;
using Repository.DAL;

namespace AdminPanel
{
    public partial class TagsList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                TagRepository productRepository = new TagRepository();
                gridTags.DataSource = productRepository.GetAll();
                gridTags.DataBind();
            }
        }

        protected void btnAddTag_Click(object sender, EventArgs e)
        {
            Response.Redirect("Tag.aspx");
        }

        protected void gridTags_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditTag")
            {
                Response.Redirect("Tag.aspx?Id=" + e.CommandArgument);
            }
            else
                if (e.CommandName == "DeleteTag")
                {
                    var msg = Utility.AesEncrypt("آیا از حذف تگ اطمینان دارید؟");
                    var table = Utility.AesEncrypt("Tags");
                    Response.Redirect("Confirmation.aspx?Id=" + e.CommandArgument + "&m=" + msg + "&t=" + table);
                }
        }
    }
}