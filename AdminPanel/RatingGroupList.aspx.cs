using System;
using Common;
using Repository.DAL;
using Repository.Entity.Domain;

namespace AdminPanel
{
    public partial class RatingGroupList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var repo = new RatingGroupRepository();
                gridRatingGroups.DataSource = repo.GetAll();
                gridRatingGroups.DataBind();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("RatingGroup.aspx");
        }

        protected void RadRating1_OnRate(object sender, EventArgs e)
        {
        
            try
            {
                var uow = new UnitOfWork();
                uow.RatingItems.Create(new RatingItem()
                {
                    RatingGroupId =1,
                    Value = RadRating1.Value

                });

                uow.SaveChanges();
            }
            catch (LocalException ex)
            {

            }
            catch (Exception ex)
            {
                
            }
        
        }
    }
}