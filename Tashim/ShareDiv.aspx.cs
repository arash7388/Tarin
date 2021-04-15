using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Common;
using Repository.DAL;

namespace Tashim
{
    public partial class ShareDiv : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BinddrpType();

                if (Request.QueryString["Id"] != null)
                {
                    ShareDivRepository repo = new ShareDivRepository();
                    var toBeEditedShareDiv = repo.GetById(Request.QueryString["Id"].ToSafeInt());

                    if (toBeEditedShareDiv != null)
                    {
                        txtShareAmount.Text = toBeEditedShareDiv.Amount.ToSafeString();
                        drpType.SelectedValue = toBeEditedShareDiv.Type.ToString();
                    }
                }
            }
        }

        private void BinddrpType()
        {
            List<KeyValuePair<int, string>> source = new List<KeyValuePair<int, string>>();
            source.Add(new KeyValuePair<int, string>(-1, "انتخاب کنید"));

            source.Add(new KeyValuePair<int, string>(1, "مهرو پلاک"));
            source.Add(new KeyValuePair<int, string>(2, "لیتوگراف"));
            source.Add(new KeyValuePair<int, string>(3, "چاپ سیلک"));
            source.Add(new KeyValuePair<int, string>(4, "همه"));

            drpType.DataSource = source;
            drpType.DataValueField = "Key";
            drpType.DataTextField = "Value";
            drpType.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (drpType.SelectedValue == "-1") throw new LocalException("Type is empty", "نوع را وارد نمایید");
                if (txtShareAmount.Text == string.Empty) throw new LocalException("Share is empty", "مبلغ سود را وارد نمایید");


                UnitOfWork u = new UnitOfWork();

                if (Request.QueryString["Id"] == null)
                {
                    var newShareDivision = new Repository.Entity.Domain.Tashim.ShareDivision()
                    {
                        Type = Convert.ToByte(drpType.SelectedValue),
                        Amount = txtShareAmount.Text.ToSafeLong(),
                    };

                    var memberRepo = new MemberRepository();

                    List<Repository.Entity.Domain.Tashim.Member> relatedMembers = null;
                    
                    if (drpType.SelectedValue != "4")
                    {
                        var selectedType = Convert.ToInt32(drpType.SelectedValue);
                        relatedMembers = memberRepo.Get(m => m.Type == selectedType).ToList();
                    }
                    else
                    {
                        relatedMembers = memberRepo.GetAll().ToList();
                    }

                    var newProfit = txtShareAmount.Text.ToSafeLong();//to be divided value
                    var sum = relatedMembers.Sum(m => m.ShareAmount);
                    var ratio = newProfit / sum;

                    newShareDivision.ShareDivisionDetails = new List<Repository.Entity.Domain.Tashim.ShareDivisionDetail>();

                    foreach (Repository.Entity.Domain.Tashim.Member member in relatedMembers)
                    {
                        var newDetail = new Repository.Entity.Domain.Tashim.ShareDivisionDetail
                        {
                            MemberId = member.Id,
                            ShareAmount = ratio * member.ShareAmount
                        };

                        newShareDivision.ShareDivisionDetails.Add(newDetail);
                    }

                    u.ShareDivisions.Create(newShareDivision);
                }
                //else
                //{
                //    var toBeEditedItem = u.ShareDivisions.GetById(Request.QueryString["Id"].ToSafeInt());
                //    toBeEditedItem.Type = Convert.ToByte(drpType.SelectedValue);
                //}

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
            drpType.SelectedIndex = 0;
            txtShareAmount.Text = "";
        }

        protected void gridList_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gridList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
        }
    }
}