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
    public partial class City : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["Id"] != null)
                {
                    var repo = new CityRepository();
                    var toBeEditedCity = repo.GetById(Request.QueryString["Id"].ToSafeInt());

                    if (toBeEditedCity != null)
                    {
                        txtName.Text = toBeEditedCity.Name;
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text == string.Empty) throw new LocalException("Name is empty", "نام را وارد نمایید");

                UnitOfWork u = new UnitOfWork();

                if (Request.QueryString["Id"] == null)
                {
                    u.Cities.Create(new Repository.Entity.Domain.City()
                    {
                        Name = txtName.Text,
                    });
                }
                else
                {
                    var toBeEditedCity = u.Cities.GetById(Request.QueryString["Id"].ToSafeInt());
                    toBeEditedCity.Name = txtName.Text;
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
        }
    }
}