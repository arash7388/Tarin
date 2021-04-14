using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Repository.DAL;

namespace Tashim
{
    public partial class Member : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BinddrpType();

                if (Request.QueryString["Id"] != null)
                {
                    MemberRepository repo = new MemberRepository();
                    var toBeEditedMember = repo.GetById(Request.QueryString["Id"].ToSafeInt());

                    if (toBeEditedMember != null)
                    {
                        txtCode.Text = toBeEditedMember.Code;
                        txtName.Text = toBeEditedMember.Name;
                        drpType.SelectedValue = toBeEditedMember.Type.ToString();
                        txtShareAmount.Text = toBeEditedMember.ShareAmount.ToString();
                    }
                }
            }
        }

        private void BinddrpType()
        {
            List<KeyValuePair<int, string>> source = new List<KeyValuePair<int,string>>();
            source.Add(new KeyValuePair<int, string> (-1, "انتخاب کنید"));

            source.Add(new KeyValuePair<int, string> (1, "مهرو پلاک"));
            source.Add(new KeyValuePair<int, string> (2, "لیتوگراف"));
            source.Add(new KeyValuePair<int, string> (3, "چاپ سیلک"));

            drpType.DataSource = source;
            drpType.DataValueField = "Key";
            drpType.DataTextField = "Value";
            drpType.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCode.Text==string.Empty) throw new LocalException("Code is empty","کد را وارد نمایید");
                if (txtName.Text==string.Empty) throw new LocalException("Name is empty","نام را وارد نمایید");
                if (drpType.SelectedValue=="-1") throw new LocalException("Type is empty","نوع را وارد نمایید");
                if (txtShareAmount.Text == string.Empty) throw new LocalException("Share is empty","سهم را وارد نمایید");
                

                UnitOfWork u = new UnitOfWork();

                if (Request.QueryString["Id"]==null)
                {
                    if(u.Members.Get(a=>a.Code==txtCode.Text).Any())
                        throw new LocalException("Duplicate code", "کد تکراری است");

                    var newMember = new Repository.Entity.Domain.Tashim.Member()
                    {
                        Code = txtCode.Text,
                        Name = txtName.Text,
                        Type = Convert.ToByte(drpType.SelectedValue),
                        ShareAmount = txtShareAmount.Text.ToSafeLong(),
                        ShareCount = txtShareAmount.Text.ToSafeLong() / 2000

                    };

                    u.Members.Create(newMember);
                }
                else
                {
                    var toBeEditedMember = u.Members.GetById(Request.QueryString["Id"].ToSafeInt());
                    toBeEditedMember.Code = txtCode.Text;
                    toBeEditedMember.Name = txtName.Text;
                    toBeEditedMember.Type = Convert.ToByte(drpType.SelectedValue);
                    toBeEditedMember.ShareAmount = txtShareAmount.Text.ToSafeLong();
                    toBeEditedMember.ShareCount = txtShareAmount.Text.ToSafeLong() / 2000;
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
            txtName.Text = "";
            drpType.SelectedIndex = 0;
            txtCode.Text = "";
            txtShareAmount.Text = "";
        }
    }
}