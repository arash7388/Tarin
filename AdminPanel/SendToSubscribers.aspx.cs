using System;
using System.Collections.Generic;
using Common;
using Repository.DAL;
using Repository.Entity.Domain;

namespace AdminPanel
{
    public partial class SendToSubscribers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSendToSubscribers_Click(object sender, EventArgs e)
        {
            if (RadEditor1.Content == string.Empty)
            {
                ((AdminPanel) Master).GeneralMessage = "متن مطلب خالی است";
                 ((AdminPanel) Master).MessageType=MessageType.Error;
                return;
            }

            SubscriberRepository repo = new SubscriberRepository();
            IList<Subscriber> subscribers = repo.GetAll();
            foreach (Subscriber subscriber in subscribers)
            {
                Utility.SendEmail("Customer-Support@tiaraelc.ir", subscriber.EMail, RadEditor1.Text, "تیارا الکتریک",
                    "atc@8873");
            }
        }
    }
}