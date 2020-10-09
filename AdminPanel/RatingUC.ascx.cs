using System;
using Common;
using Repository.DAL;
using Repository.Entity.Domain;

namespace MTO.Admin
{
    public partial class RatingUC : System.Web.UI.UserControl
    {
        public int RatingGroupId { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var repo = new RatingGroupRepository();
                var rg = repo.GetById(RatingGroupId);
                if (rg==null) throw new LocalException("Rating Group not found","آیتم رای گیری یافت نشد");

                //lblRatingText.Text = rg.Name;
                //RadRating1.Value = repo.GetRatingGroupCurrentValue(RatingGroupId).ToSafeDecimal();
            }
            catch (LocalException ex)
            {
                //Console.WriteLine(localException);
            }

        }

        protected void RadRating1_OnRate(object sender, EventArgs e)
        {
            try
            {
                var uow = new UnitOfWork();
                uow.RatingItems.Create(new RatingItem()
                {
                    RatingGroupId = this.RatingGroupId,
                   // Value = RadRating1.Value

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