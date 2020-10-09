using System;

namespace AdminPanel
{
    public partial class AdminPanelMain : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

     protected void sendToSubscribers_OnClick(object sender, EventArgs e)
      {
            //string html = RadEditor1.Content;
            //Thread t = new Thread(send());
            //for (int i = 0; i < 10; i++)
            //{
            //    BackgroundWorker bg = new BackgroundWorker();
            //    bg.DoWork += bg_DoWork;
            //    bg.RunWorkerAsync(i);
                
            //}
        }

        //private ThreadStart send()
        //{
        //    //var i = e.Argument;
        //    Utility.SendEmail("aaa@a.com", "arash.masir@gmail.com", RadEditor1.Content, "testtt");
        //}

        //void bg_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    var i = e.Argument;
        //    Utility.SendEmail("aaa@a.com", "arash.masir@gmail.com", RadEditor1.Content, "testtt");
        //}
    }
}