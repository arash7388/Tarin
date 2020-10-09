using System;
using Common;
using Repository.DAL;

namespace AdminPanel
{
    public partial class Link : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["Id"] != null)
                {
                    LinkRepository repo = new LinkRepository();
                    var toBeEditedLink = repo.GetById(Request.QueryString["Id"].ToSafeInt());

                    if (toBeEditedLink != null)
                    {
                        txtTitle.Text = toBeEditedLink.Title;
                        txtHref.Text = toBeEditedLink.Href;
                    }
                }
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTitle.Text == string.Empty) throw new LocalException("Title is empty", "عنوان لینک را وارد نمایید");
                if (txtHref.Text == string.Empty) throw new LocalException("Href is empty", "آدرس لینک را وارد نمایید");

                
                UnitOfWork u = new UnitOfWork();

                if (Request.QueryString["Id"] == null)
                {
                    u.Links.Create(new Repository.Entity.Domain.Link()
                    {
                        Title = txtTitle.Text,
                        Href = txtHref.Text,
                    });
                }
                else
                {
                    var toBeEditedLink = u.Links.GetById(Request.QueryString["Id"].ToSafeInt());
                    toBeEditedLink.Title = txtTitle.Text;
                    toBeEditedLink.Href = txtHref.Text;
                }

                u.SaveChanges();

                lblResult.InnerText = "اطلاعات با موفقیت ذخیره شد";
                ClearControls();
            }
            catch (LocalException ex)
            {
                lblResult.InnerText = ex.ResultMessage;
            }
            catch (Exception ex)
            {
                lblResult.InnerText = ex.Message;
            }
        }

        private void ClearControls()
        {
            txtTitle.Text = "";
            txtHref.Text = "";
        }
    }
}