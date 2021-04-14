using System;
using System.Web.UI.HtmlControls;
using Common;

namespace MehranPack.UserControls
{
    public partial class PersianCalender : System.Web.UI.UserControl
    {
        private bool _allowEmpty = false;
        private bool _loadCurrentDateTime;

        protected string ClientIDPerfix { get { return ClientID + ClientIDSeparator; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            year.Attributes.Add("onblur","spilitDate('"+ ClientIDPerfix +"');");
            hfAllowEmpty.Value = _allowEmpty ? "1" : "0";

            if(_loadCurrentDateTime)
                SetPersianDate();
        }

        public bool AllowEmpty
        {
            get { return _allowEmpty; }
            set { _allowEmpty = value; }
        }
        public string Date
        {
            get { return (_allowEmpty && string.IsNullOrEmpty(year.Text)) ? "" : year.Text + "/" + mounth.Text + "/" + day.Text; }
            set
            {
                if (value != "")
                {
                    year.Text = value.Substring(0, 4);
                    mounth.Text = value.Substring(5, 2);
                    day.Text = value.Substring(8, 2);
                } 
                
            }
        }
        public string Time
        {
            get { return (_allowEmpty && string.IsNullOrEmpty(year.Text)) ? "" : hour.Text + ":" + minute.Text + ":" + second.Text; }
        }
        public HtmlTable TimePanel
        {
            get { return _timePanel; }
        }
        public string Datetime
        {
            get { return Date + " " + Time; }
        }
        public short TabIndex
        {
            get { return year.TabIndex; }
            set {year.TabIndex = value;}
        }
        public void SetPersianDate()
        {
            string faDate = Common.Utility.ToFaDate(DateTime.Now);
            year.Text = faDate.Substring(0, 4);
            mounth.Text = faDate.Substring(5, 2);
            day.Text = faDate.Substring(8, 2);
        }

        public bool LoadCurrentDateTime
        {
            get { return _loadCurrentDateTime; }
            set { _loadCurrentDateTime = value; }
        }

        public void SetStartOfYearDate()
        {
            string faDate = Common.Utility.ToFaDate(DateTime.Now);
            year.Text = faDate.Substring(0, 4);
            mounth.Text = "01";
            day.Text = "01";
        }
    }
}