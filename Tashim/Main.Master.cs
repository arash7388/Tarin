using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Routing;
using System.Web.UI;
using Common;
using Repository.DAL;
using Repository.Entity.Domain;

namespace Tashim
{
    public partial class Main : System.Web.UI.MasterPage
    {
        public string GeneralMessage
        {
            get { return generalMessage.Text; }
            set { generalMessage.Text = value; }
        }

        public MessageType MessageType
        {

            set
            {
                generalMessage.CssClass = "alert ";

                switch (value)
                {
                    case MessageType.Info:
                        generalMessage.CssClass += " alert-info ";
                        break;
                    case MessageType.Warning:
                        generalMessage.CssClass += " alert-warning ";
                        break;
                    case MessageType.Error:
                        generalMessage.CssClass += " alert-danger ";
                        break;
                    case MessageType.Success:
                        generalMessage.CssClass += " alert-success ";
                        break;
                }

                generalMessage.CssClass += " master-alert";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Debuging.Info("Start of Main.master.cs");

            if (Environment.MachineName.ToLower() == "DESKTOP-6700VR9".ToLower())
                Session["User"] = new UserRepository().GetById(1);

            if (Session["User"] == null)
                Response.Redirect("Login.aspx");

            var user = (User)Session["User"];
            if (user != null)
            {
                lblCurrentUser.Text = "کاربر:" + user.FriendlyName;
                hfUserId.Value = user.Id.ToString();
            }

            if(Session["PostProcessMessage"]!=null)
            {
                var postPrMsg = (PostProcessMessage)Session["PostProcessMessage"];
                SetGeneralMessage(postPrMsg.Message, postPrMsg.MessageType);
                Session["PostProcessMessage"] = null;
            }

            if(Session["User"] != null && ((User)Session["User"]).Username == "inoutop")
            {
                var items = GetControlHierarchy(mainMenuDiv);
                foreach (var item in items)
                {
                    item.Visible = false;
                }

                mainMenuDiv.Visible = true;
                lbtnExit.Visible = true;
            }
        }

        private IEnumerable<Control> GetControlHierarchy(Control root)
        {
            var queue = new Queue<Control>();

            queue.Enqueue(root);

            do
            {
                var control = queue.Dequeue();

                yield return control;

                foreach (var child in control.Controls.OfType<Control>())
                    queue.Enqueue(child);

            } while (queue.Count > 0);

        }

        public void SetGeneralMessage(string text, MessageType messageType)
        {
            generalMessage.Visible = true;
            GeneralMessage = text;
            MessageType = messageType;
        }

        public void HideGeneralMessage()
        {
            generalMessage.Visible = false;
        }

        protected void lbtnDrafts_OnClick(object sender, EventArgs e)
        {
            var routeValues = new RouteValueDictionary();
            routeValues.Add("DocumentType", "Draft");
            Response.RedirectToRoute("Home", routeValues);
        }

        protected void lbtnOrders_OnClick(object sender, EventArgs e)
        {
            Response.RedirectToRoute("OrderList");
        }

        protected void lbtnFactors_OnClick(object sender, EventArgs e)
        {
            Response.RedirectToRoute("FactorList");
        }

        protected void lbtnProductType_OnClick(object sender, EventArgs e)
        {

        }

        protected void lbtnShareDivList_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("ShareDivList.aspx");
        }

        protected void lbtnExit_OnClick(object sender, EventArgs e)
        {
            Session["User"] = null;
            Response.RedirectToRoute("Login");
        }

        protected void lbtnOrdersReport_OnClick(object sender, EventArgs e)
        {
            Response.RedirectToRoute("OrdersReport");
        }

        protected void lbtnFactorsReport_OnClick(object sender, EventArgs e)
        {
            Response.RedirectToRoute("FactorsReport");
        }

        protected void lbtnPayments_OnClick(object sender, EventArgs e)
        {
            Response.RedirectToRoute("PaymentList");
        }

        protected void lbtnShareDiv_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("Sharediv.aspx");
        }

        protected void lbtnInOut_OnClick(object sender, EventArgs e)
        {
            //Response.RedirectToRoute("InputOutputList");
        }

        protected void lbtnIn_OnClick(object sender, EventArgs e)
        {
            var routeValues = new RouteValueDictionary();
            routeValues.Add("Type", "In");
            Response.RedirectToRoute("InputOutputList", routeValues);
        }

        protected void lbtnOut_OnClick(object sender, EventArgs e)
        {
            var routeValues = new RouteValueDictionary();
            routeValues.Add("Type", "Out");
            Response.RedirectToRoute("InputOutputList", routeValues);
        }

        protected void lbtnAdminPanel_OnClick(object sender, EventArgs e)
        {
            if (Session["User"] == null)
                Response.RedirectPermanent(ConfigurationManager.AppSettings["AdminPanelUrl"] + "/login.aspx");
            else
                Response.RedirectPermanent(ConfigurationManager.AppSettings["AdminPanelUrl"] + "/ProductList.aspx");
        }

        protected void lbtnRepIn_OnClick(object sender, EventArgs e)
        {
            var routeValues = new RouteValueDictionary();
            routeValues.Add("Type", "In");
            Response.RedirectToRoute("IOReport", routeValues);
        }

        protected void lbtnRepOut_OnClick(object sender, EventArgs e)
        {
            var routeValues = new RouteValueDictionary();
            routeValues.Add("Type", "Out");
            Response.RedirectToRoute("IOReport", routeValues);
        }

        protected void lbtnCardex_OnClick(object sender, EventArgs e)
        {
            var routeValues = new RouteValueDictionary();
            Response.RedirectToRoute("Cardex");
        }

        protected void lbtnGoodsSupply_OnClick(object sender, EventArgs e)
        {
            var routeValues = new RouteValueDictionary();
            Response.RedirectToRoute("GoodsSupply");
        }

        protected void lbtnGoodsGroupSupply_OnClick(object sender, EventArgs e)
        {
            var routeValues = new RouteValueDictionary();
            Response.RedirectToRoute("GoodsGroupSupply");
        }

        protected void lbtnUsers_OnClick(object sender, EventArgs e)
        {
            Response.RedirectToRoute("Userslist");
        }

        protected void lbtnProcesses_OnClick(object sender, EventArgs e)
        {
            Response.RedirectToRoute("Processlist");
        }

        protected void lbtnProcessCat_OnClick(object sender, EventArgs e)
        {
            Response.RedirectToRoute("ProcessCategorylist");
        }

        protected void lbtnWorksheets_OnClick(object sender, EventArgs e)
        {
            Response.RedirectToRoute("Worksheetlist");
        }

        protected void lbtnProducts_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("productlist.aspx");
        }

        protected void lbtnMembers_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("memberlist.aspx");
        }

        protected void lbtnWorkLines_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("WorkLine.aspx");
        }

        protected void lbtnWorkLineReport_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("WorkLineReport.aspx");
        }

        protected void lbtnWorkLineSummaryReport_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("WorkLineSummaryReport.aspx");
        }

        protected void lbtnWorksheetReport_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("WorksheetReport.aspx");
        }
        
        protected void lbtnManualWorklineInput_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("ManualWorklineInput.aspx");
        }

        protected void lblReworkReasons_OnClick(object sender, EventArgs e)
        {
            Response.RedirectToRoute("ReworkReasonList"); 
        }

        protected void lblReworkList_OnClick(object sender, EventArgs e)
        {
            Response.RedirectToRoute("ReworkList");
        }
        
        protected void lblEsghatList_OnClick(object sender, EventArgs e)
        {
            var routeValues = new RouteValueDictionary();
            routeValues.Add("Mode", "Es");
            Response.RedirectToRoute("EsghatList", routeValues);
        }
    }
}