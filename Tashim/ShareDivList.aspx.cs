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
    public partial class ShareDivList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ShareDivRepository repo = new ShareDivRepository();

                //1, "مهرو پلاک"
                //2, "لیتوگراف"
                //3, "چاپ سیلک"

                gridList.DataSource = repo.GetAll().OrderByDescending(a => a.InsertDateTime).Select(a => new
                {
                    ShareAmount = a.Amount,
                    a.DeleteDateTime,
                    a.Id,
                    a.InsertDateTime,
                    a.Status,
                    a.Type,
                    TypeDesc = a.Type == 1 ? "مهرو پلاک" : (a.Type == 2 ? "لیتوگراف" : (a.Type == 3 ? "چاپ سیلک" : (a.Type == 4 ? "همه" : ""))),
                    a.UpdateDateTime,
                    PersianInsertDateTime = Convert.ToDateTime(a.InsertDateTime).ToFaDate(),
                    a.SharePercent,
                    a.EqualPercent,
                    a.PriorityPercent
                }).ToList();

                gridList.DataBind();
                Session["Result"] = gridList.DataSource;
            }
        }

        //protected string GetPersianDate(object input)
        //{
        //    return input.ToSafeString() != string.Empty ? DateTime.Parse(input.ToSafeString()).ToFaDate() : "";
        //}

        protected void gridList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                Response.Redirect("ShareDiv.aspx?Id=" + e.CommandArgument);
            }
            else if (e.CommandName == "Delete")
            {
                var data = new ConfirmData();

                data.Command = "Delete";
                data.Id = e.CommandArgument.ToSafeInt();
                data.Msg = "آیا از حذف اطمینان دارید؟";
                data.Table = "ShareDivisions";
                data.RedirectAdr = "ShareDivList.aspx";

                Session["ConfirmData"] = data;
                Response.Redirect("Confirmation.aspx");
                Response.End();
            }
            else if (e.CommandName == "Print")
            {
                Response.Redirect($"ShareDivPrint.aspx?id={e.CommandArgument.ToSafeInt()}");
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("ShareDiv.aspx");
        }

        protected void gridList_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridList.PageIndex = e.NewPageIndex;
            gridList.DataSource = Session["Result"];
            gridList.DataBind();
        }

        protected void gridList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[2].Text = e.Row.Cells[2].Text.ToSafeDecimal().ToString("#,##0");
            }
        }
    }
}