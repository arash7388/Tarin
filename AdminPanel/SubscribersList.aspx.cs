using System;
using System.Web;
using System.Web.Services;
using Common;
using Repository.DAL;
using Telerik.Web.UI;

namespace AdminPanel
{
    public partial class SubscribersList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var repo = new SubscriberRepository();
            gridSubscribers.DataSource = repo.GetAll();
            gridSubscribers.DataBind();
        }

        [WebMethod]
        public static bool DeleteSubscriber(int id)
        {
            try
            {
                var repo = new SubscriberRepository();
                repo.Delete(id);
                return true;

            }
            catch (LocalException ex)
            {

            }

            return false;
        }

        private void Refresh(object sender, EventArgs e)
        {

            Page.Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);

        }
       
       
        protected void gridSubscribers_OnDeleteCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridDataItem item = e.Item as GridDataItem;
                int id = item.GetDataKeyValue("Id").ToSafeInt();
                var msg = Utility.AesEncrypt("آیا از حذف مشترک اطمینان دارید؟");
                var table = Utility.AesEncrypt("Subscribers");
                Response.Redirect("Confirmation.aspx?Id=" + id + "&m=" + msg + "&t=" + table);
            }
            catch (LocalException ex)
            {
                ((AdminPanel) Master).GeneralMessage = ex.ResultMessage;
            }
            catch (Exception ex)
            {
                ((AdminPanel)Master).GeneralMessage = "بروز خطا در سیستم";
            }
            
        }
    }
}