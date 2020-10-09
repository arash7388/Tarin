using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using Repository.DAL;

namespace AdminPanel
{
    public partial class Area : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BinddrpCity();

                if (Request.QueryString["Id"] != null)
                {
                    var repo = new AreaRepository();
                    var tobeEditedArea = repo.GetById(Request.QueryString["Id"].ToSafeInt());

                    txtName.Text = tobeEditedArea.Name;
                    drpCity.SelectedValue = tobeEditedArea.CityId.ToString();
                }
            }
        }

        private void BinddrpCity()
        {
            var  repo = new CityRepository();
            var source = new List<Repository.Entity.Domain.City>();
            source.Add(new Repository.Entity.Domain.City() { Id = -1, Name = "انتخاب کنید" });
            source.AddRange(repo.GetAll());

            drpCity.DataSource = source;
            drpCity.DataValueField = "Id";
            drpCity.DataTextField = "Name";
            drpCity.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtName.Text)) throw new LocalException("Name is empty", "نام  را وارد نمایید");
                if (Convert.ToInt32(drpCity.SelectedItem.Value) == -1) throw new LocalException("Category is empty", "شهر را وارد نمایید");

                UnitOfWork uow = new UnitOfWork();

                if (Request.QueryString["Id"] == null)
                {
                    var newArea = new Repository.Entity.Domain.Area();

                    newArea.Name = txtName.Text;
                    newArea.CityId = Convert.ToInt32(drpCity.SelectedItem.Value);
                    uow.Areas.Create(newArea);
                }
                else
                {
                    var repo = uow.Areas;
                    var tobeEditedArea = repo.GetById(Request.QueryString["Id"].ToSafeInt());
                    tobeEditedArea.Name = txtName.Text;
                    tobeEditedArea.CityId = drpCity.SelectedValue.ToSafeInt();
                }

                uow.SaveChanges();
                lblResult.InnerText = "اطلاعات با موفقیت ذخیره شد";
                ClearControls();
            }
            catch (LocalException ex)
            {
                lblResult.InnerText = ex.ResultMessage;
            }
        }

        private void ClearControls()
        {
            txtName.Text = "";
            drpCity.SelectedValue = "-1";
        }
    }
}