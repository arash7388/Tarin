using System;
using Common;

namespace AdminPanel
{
    public partial class AdminPanel : System.Web.UI.MasterPage
    {
        public string GeneralMessage
        {
            get { return lblGeneralMessage.Text; }
            set { lblGeneralMessage.Text = value; }
        }

        public MessageType MessageType
        {
           set
            {
                switch ((MessageType)value)
                {
                    case MessageType.Info:
                        lblGeneralMessage.Attributes.Add("class", "alert-info");
                        break;
                    case MessageType.Warning:
                        lblGeneralMessage.Attributes.Add("class", "alert-warning");
                        break;
                    case MessageType.Error:
                        lblGeneralMessage.Attributes.Add("class", "alert-danger");
                        break;
                    default:
                        break;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Environment.MachineName!="ADMIN-PC" && Environment.UserName!="admin" && Session["Username"] == null)
                Response.Redirect("Login.aspx");
        }
    }
}