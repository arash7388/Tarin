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

                if (txtSharePercent.Text.ToSafeString() != "" && !int.TryParse(txtSharePercent.Text, out int _))
                    throw new LocalException("", "درصد سهام باید عدد بین 0 تا صد باشد");

                if(txtEqualPercent.Text.ToSafeString() != "" && !int.TryParse(txtEqualPercent.Text, out int _))
                    throw new LocalException("", "درصد مساوی باید عدد بین 0 تا صد باشد");

                if(txtPrPercent.Text.ToSafeString() != "" && !int.TryParse(txtPrPercent.Text, out int _))
                    throw new LocalException("", "درصد اولویت باید عدد بین 0 تا صد باشد");

                if (txtSharePercent.Text.ToSafeInt() + txtEqualPercent.Text.ToSafeInt() + txtPrPercent.Text.ToSafeInt() != 100)
                    throw new LocalException("Sum percent is wrong", "مجموع درصد ها باید 100 باشد");

                UnitOfWork u = new UnitOfWork();

                if (Request.QueryString["Id"] == null)
                {
                    var newShareDivision = new Repository.Entity.Domain.Tashim.ShareDivision()
                    {
                        Type = Convert.ToByte(drpType.SelectedValue),
                        Amount = txtShareAmount.Text.ToSafeLong(),
                        SharePercent = txtSharePercent.Text.ToSafeInt(),
                        EqualPercent = txtEqualPercent.Text.ToSafeInt(),
                        PriorityPercent = txtPrPercent.Text.ToSafeInt()
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

                    newShareDivision.ShareDivisionDetails = new List<Repository.Entity.Domain.Tashim.ShareDivisionDetail>();

                    foreach (Repository.Entity.Domain.Tashim.Member member in relatedMembers)
                    {
                        var newDetail = new Repository.Entity.Domain.Tashim.ShareDivisionDetail
                        {
                            MemberId = member.Id,
                            ShareAmount = 0
                        };

                        newShareDivision.ShareDivisionDetails.Add(newDetail);
                    }

                    var totalProfit = txtShareAmount.Text.ToSafeLong();//to be divided value
                    var sumMemberShareCounts = relatedMembers.Sum(m => m.ShareCount);
                    var relatedMembersCount = relatedMembers.Count();
                    

                    var profitForShareCountPercent = (totalProfit * txtSharePercent.Text.ToSafeInt()) / 100;
                    var profitForEqualPercent = (totalProfit * txtEqualPercent.Text.ToSafeInt()) / 100;
                    var profitForPriorityPercent = (totalProfit * txtPrPercent.Text.ToSafeInt()) / 100;

                    var ratioBasedOnMemberShareCounts = profitForShareCountPercent / sumMemberShareCounts;

                    if (profitForEqualPercent != 0)
                        foreach (var det in newShareDivision.ShareDivisionDetails)
                        {
                            det.ShareAmount = profitForEqualPercent / relatedMembersCount;
                        }

                    if (profitForShareCountPercent != 0)
                        foreach (var det in newShareDivision.ShareDivisionDetails)
                        {
                            det.ShareAmount += ratioBasedOnMemberShareCounts * relatedMembers.Where(a=>a.Id==det.MemberId).FirstOrDefault().ShareCount;
                        }

                    if (profitForPriorityPercent != 0)
                    {
                        var memberWithPr = relatedMembers.Where(a => a.HasPriority).ToList();
                        var memberWithPrCount = memberWithPr.Count();

                        foreach (var det in newShareDivision.ShareDivisionDetails)
                        {
                            if (memberWithPr.Select(a => a.Id).Contains(det.MemberId))
                                det.ShareAmount += profitForPriorityPercent / memberWithPrCount;
                        }
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