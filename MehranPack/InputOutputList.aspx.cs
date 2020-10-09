using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using Repository.DAL;
using ActionResult = Common.ActionResult;

namespace MehranPack
{
    public partial class InputOuputList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Page.RouteData.Values["Type"].ToString() == "In")
                {
                    Session["Result"] = gridList.DataSource = new InputOutputRepository().GetAllIns().OrderByDescending(a => a.Id).ToList();
                    h3Header.InnerText = "لیست کالاهای ورودی";
                }
                else
                {
                    Session["Result"] = gridList.DataSource = new InputOutputRepository().GetAllOuts().OrderByDescending(a => a.Id).ToList();
                    h3Header.InnerText = "لیست کالاهای خروجی";
                }
                
                gridList.DataBind();

                //after saving a io user should see factor list.
                if (Page.RouteData.Values["ActionResult"].ToSafeBool())
                {
                    var saveResult = (ActionResult)Session["SaveIOActionResult"];

                    if (saveResult != null)
                        ((Main)(Page.Master)).SetGeneralMessage(saveResult.ResultMessage, MessageType.Success);
                }

                if (Session["ValidationBeforeDelete"] != null)
                {
                    (Master as Main).SetGeneralMessage("موجودی این کالا(ها) در رسیدهای بعدی منفی خواهد شد : "
                           + Session["ValidationBeforeDelete"], MessageType.Error);

                    Session["ValidationBeforeDelete"] = null;
                }
            }
        }

        protected void gridList_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            var routeValues = new RouteValueDictionary();
            routeValues.Add("Id", e.CommandArgument);

            if (e.CommandName == "Edit")
            {
                routeValues.Add("Type", Page.RouteData.Values["Type"].ToSafeString());
                Response.RedirectToRoute("InputOutput", routeValues);
                Response.End();
            }
            else if (e.CommandName == "Delete")
            {
                if (Page.RouteData.Values["Type"].ToString() == "In")
                {
                    var negativePIDs = new InputOutputDetailRepository().CheckSupplyValidationBeforeDeleteInput(e.CommandArgument.ToSafeInt());

                    if (negativePIDs.Any())
                    {
                        string listOfProductCodes = "";
                        negativePIDs.ForEach(a => { listOfProductCodes += GetProductCode(a) + ","; });

                        Session["ValidationBeforeDelete"] = listOfProductCodes.Remove(listOfProductCodes.Length - 1, 1);

                        var rv = new RouteValueDictionary();
                        rv.Add("Type", "In");
                        Response.RedirectToRoute("InputOutputList", rv);
                    }
                    else
                        DoDelete(e);
                }
                else
                    DoDelete(e);
            }
        }

        private void DoDelete(GridViewCommandEventArgs e)
        {
            var data = new ConfirmData();

            data.Command = "Delete";
            data.Id = e.CommandArgument.ToSafeInt();
            data.Msg = "آیا از حذف اطمینان دارید؟";
            data.Table = "InputOutputs";
            data.RedirectRoute = "InputOutputList";
            data.RedirectRouteValueDictionary = Page.RouteData.Values;
            Session["ConfirmData"] = data;
            Response.RedirectToRoute("Confirmation");
            Response.End();
        }

        private string GetProductCode(int pid)
        {
            return new ProductRepository().GetCodeById(pid);
        }

        protected void gridList_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridList.PageIndex = e.NewPageIndex;
            gridList.DataSource = Session["Result"];
            gridList.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            var routeValues = new RouteValueDictionary();
            routeValues.Add("Id", 0);
            routeValues.Add("Type",Page.RouteData.Values["Type"].ToString());
            Response.RedirectToRoute("InputOutput", routeValues);
        }
    }
}