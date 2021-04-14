using Common;
using Repository.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tashim
{
    public partial class MemberList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                MemberRepository repo = new MemberRepository();
                
                gridList.DataSource = repo.GetAll().OrderBy(a=>Convert.ToInt64(a.Code)).ToList();

                gridList.DataBind();
                Session["Result"] = gridList.DataSource;

                txtSum.Text = repo.GetAll().Sum(a => a.ShareAmount).ToSafeString().ToFaGString();
            }
        }

        protected void gridList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                Response.Redirect("Member.aspx?Id=" + e.CommandArgument);
            }
            else
            if (e.CommandName == "Delete")
            {
                var data = new ConfirmData();

                data.Command = "Delete";
                data.Id = e.CommandArgument.ToSafeInt();
                data.Msg = "آیا از حذف اطمینان دارید؟";
                data.Table = "Members";
                data.RedirectAdr = "MemberList.aspx";

                Session["ConfirmData"] = data;
                Response.Redirect("Confirmation.aspx");
                Response.End();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("Member.aspx");
        }

        protected void gridList_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridList.PageIndex = e.NewPageIndex;
            gridList.DataSource = Session["Result"];
            gridList.DataBind();
        }
    }
}