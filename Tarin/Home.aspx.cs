using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Repository.Entity.Domain;
using Repository.DAL;
using System.Web.Services;
using System.Text;
using Common;

namespace Tarin
{
    public partial class Home : System.Web.UI.Page
    {
        public static readonly int PageSize = 5;
        public static int catId;
        public static int FirstCount = 0;
        public string myStr = "has-border";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var mainCats = new CategoryRepository().GetAllMainCats();
                
                var backMenu = new Category();
                backMenu.Name = "همه";
                backMenu.Id = -1;
                catId = 0;

                mainCats.Insert(0, backMenu);

                listViewCats.DataSource = mainCats;
                listViewCats.DataBind();
            }
        }

        [WebMethod]
        public static string GetData(int pageIndex)
        {
            var ads = new AdvertisementRepository().GetByPageIndex(pageIndex, PageSize);
            FirstCount += PageSize;
            StringBuilder sb = new StringBuilder();

            foreach (Advertisement ad in ads)
            {
                {
                    sb.AppendFormat(@"<div class=""col-sm-2 un-pad"">");
                    {
                        sb.AppendFormat(@"<div class=""list-item-bg"">");

                        {
                            sb.AppendFormat(@"<div class=""frame-size"">");
                            {
                                sb.AppendFormat(@"<div>");
                                sb.AppendFormat(@"<img src=""Content/Images/" + (ad.Id - 1).ToString() + @".jpg"" class="" img-size""/>");
                                sb.AppendFormat(@"</div>");
                            }

                            sb.AppendFormat(@"</div>");
                        }
                        {
                            sb.AppendFormat(@"<div class=""item-border""></div>");
                        }
                        {
                            sb.AppendFormat(@"<div style=""background: #f5f5f5; border-bottom-left-radius: 4px; border-bottom-right-radius: 4px;"">");
                            {
                                sb.AppendFormat(@"<p style=""text-align: center"">");
                                sb.AppendFormat(@ad.Name);
                                sb.AppendFormat(@"</p>");
                            }
                            {
                                sb.AppendFormat(@"<div class=""item-border""></div>");
                            }
                            // <%--محله:--%>
                            {
                                sb.AppendFormat(@"<div style=""background: #dcebf9; border-bottom-left-radius: 4px; border-bottom-right-radius: 4px;"">");
                                {
                                    sb.AppendFormat(@"<p style=""padding-right: 5%; padding-bottom: 5%; color: #8c9fe4"">");
                                    //sb.AppendFormat(ad.AdvArea);
                                    sb.AppendFormat(@"</p>");
                                }
                                sb.AppendFormat(@"</div>");
                            }
                            sb.AppendFormat(@"</div>");
                        }
                        sb.AppendFormat(@"</div>");
                    }
                    sb.AppendFormat(@"</div>");
                }
            }
            return sb.ToString();
        }

         public void MyButtonHandler(Object sender, CommandEventArgs e)
        {
            var source = new List<Category>();
            
            var clickedCatId = e.CommandArgument.ToSafeInt();
            if (clickedCatId == 0) return;

            bool backClicked = e.CommandArgument.ToSafeInt() == -1;
            Category backBtn = new Category();

            if (backClicked)
            {
               
            }
            else
            {
                var subCats = new CategoryRepository().GetAllByParentId(clickedCatId);

                backBtn = new Category();
                backBtn.Name = "برگشت";
                backBtn.Id = -1;
                subCats.Insert(0,backBtn);

                source = subCats;
            }
            
            listViewCats.DataSource = source;
            listViewCats.DataBind();
        }
    }
}