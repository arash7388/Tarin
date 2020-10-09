using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using Energy;
using Repository.DAL;
using Repository.Entity.Domain;

namespace MehranPack
{
    public partial class Order : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                h3Title.InnerHtml = "سفارش " +
                                    (Page.RouteData.Values["Id"].ToSafeInt() == 0
                                        ? " جدید "
                                        : " " + GetOrderNoById(Page.RouteData.Values["Id"].ToSafeInt()).ToFaString());
                BindDrpCustomer();
                BindDrpProductType();

                if (Page.RouteData.Values["Id"].ToSafeInt() != 0)
                {
                    var repo = new OrderRepository();
                    var tobeEditedOrder = repo.GetById(Page.RouteData.Values["Id"].ToSafeInt());
                    LoadOrder(tobeEditedOrder);
                }
            }
        }

        private void BindDrpProductType()
        {
            var repo = new BaseRepository<ProductType>();

            drpProductType.DataSource = repo.GetAll();
            drpProductType.DataValueField = "Id";
            drpProductType.DataTextField = "Name";

            drpProductType.DataBind();
        }

        private void BindDrpCustomer()
        {
            var repo = new CustomerRepository();

            drpCustomer.DataSource = repo.GetAll().OrderBy(a=>a.Name);
            drpCustomer.DataValueField = "Id";
            drpCustomer.DataTextField = "Name";

            drpCustomer.DataBind();
        }

        private void ClearControls()
        {
            drpCustomer.SelectedIndex = -1;
            txtWorkTitle.Text = "";
            drpProductType.SelectedIndex = -1;
            txtGrammage.Text = "";
            chkIsForSent.Checked = false;
            chkIsExisted.Checked = false;
            txtCartonCount.Text = "";
            txtCartonSize.Text = "";
            txtCutSize.Text = "";
            txtTiraj.Text = "";
            chkZincExisted.Checked = false;
            chkZincSent.Checked =  false;
            txtZincDesc.Text = "";
            txtColorCount.Text = "";
            txtColorType.Text = "";
            txtMachineCount.Text = "";
            txtMachineDesc.Text = "";
            chkVernyDark.Checked = false;
            chkVernyClear.Checked = false;
            chkUVDark.Checked = false;
            chkUVClear.Checked = false;
            chkCelefonDark.Checked = false;
            chkCelefonClear.Checked = false;
            chkLabChasb.Checked =  false;
            chkGhalebTigh.Checked = false;
            chkKlisheBarjesteh.Checked = false;
            chkNimTigh.Checked = false;
            chkCutLine.Checked = false;
            txtGhalebCode.Text = "";
            txtKenareh.Text = "";
            txtDescription.Text = "";
            chkIsFactored.Checked = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(drpCustomer.SelectedValue)) throw new LocalException("Customer is empty", "مشتری  را وارد نمایید");
                if (string.IsNullOrEmpty(drpProductType.SelectedValue)) throw new LocalException("ProductType is empty", "نوع جنس را وارد نمایید");

                UnitOfWork uow = new UnitOfWork();

                if (Page.RouteData.Values["Id"].ToSafeInt() == 0)
                {
                    var newOrder = new Repository.Entity.Domain.Order();
                    newOrder.OrderNo = new OrderRepository().GetMaxOrderNo() + 1;
                    SetOrderProps(newOrder);
                    uow.Orders.Create(newOrder);
                }
                else
                {
                    var repo = uow.Orders;
                    var tobeEditedOrder = repo.GetById(Page.RouteData.Values["Id"].ToSafeInt());

                    SetOrderProps(tobeEditedOrder);
                }

                uow.SaveChanges();
                ClearControls();

                ((Main)Page.Master).SetGeneralMessage("اطلاعات با موفقیت ذخیره شد", MessageType.Success);
                ClearControls();
            }
            catch (LocalException ex)
            {
                ((Main)Page.Master).SetGeneralMessage("خطا در دخیره سازی -" + ex.ResultMessage, MessageType.Error);
            }
            catch (Exception ex)
            {
                ((Main)Page.Master).SetGeneralMessage("خطا در دخیره سازی -" + ex.Message, MessageType.Error);
            }

        }

        private void SetOrderProps(Repository.Entity.Domain.Order order)
        {
            order.CustomerId = drpCustomer.SelectedValue.ToSafeInt();
            order.WorkTitle = txtWorkTitle.Text;
            order.ProductTypeId = drpProductType.SelectedValue.ToSafeInt();
            order.Grammage = txtGrammage.Text.ToSafeInt();
            order.IsForSent = chkIsForSent.Checked;
            order.IsExisted = chkIsExisted.Checked;
            order.CartonCount = txtCartonCount.Text.ToSafeString();
            order.CartonSize = txtCartonSize.Text.ToSafeString();
            order.CutSize = txtCutSize.Text;
            order.Tiraj = txtTiraj.Text.ToSafeInt();
            order.ZincExisted = chkZincExisted.Checked;
            order.ZincSent = chkZincSent.Checked;
            order.ZincDesc = txtZincDesc.Text;
            order.ColorCount = txtColorCount.Text.ToSafeInt();
            order.ColorType = txtColorType.Text;
            order.MachineCount = txtMachineCount.Text.ToSafeInt();
            order.MachineDesc = txtMachineDesc.Text;

            order.VernyDark = chkVernyDark.Checked;
            order.VernyClear = chkVernyClear.Checked;

            order.UvDark = chkUVDark.Checked;
            order.UvClear = chkUVClear.Checked;

            order.CelefonDark = chkCelefonDark.Checked;
            order.CelefonClear = chkCelefonClear.Checked;

            order.LabChasb = chkLabChasb.Checked;
            order.GhalebTigh = chkGhalebTigh.Checked;
            order.KlisheBarjesteh = chkKlisheBarjesteh.Checked;
            order.NimTigh = chkNimTigh.Checked;
            order.CutLine = chkCutLine.Checked;
            order.GhalebCode = txtGhalebCode.Text;
            order.Kenareh = txtKenareh.Text;
            order.Description = txtDescription.Text;
            order.IsFactored = chkIsFactored.Checked;
            if(radUploalCutImage.UploadedFiles.Count>0)
            {
                var imgFile = radUploalCutImage.UploadedFiles[0];

                byte[] bytes = new byte[imgFile.ContentLength];
                imgFile.InputStream.Read(bytes, 0, (int)imgFile.ContentLength);
                order.CutImage = bytes;
            }
        }

        private void LoadOrder(Repository.Entity.Domain.Order order)
        {
            drpCustomer.SelectedValue = order.CustomerId.ToString();
            txtWorkTitle.Text = order.WorkTitle;
            drpProductType.SelectedValue = order.ProductTypeId.ToString();
            txtGrammage.Text = order.Grammage.ToString();
            chkIsForSent.Checked = order.IsForSent ?? false;
            chkIsExisted.Checked = order.IsExisted ?? false;
            txtCartonCount.Text = order.CartonCount.ToString();
            txtCartonSize.Text = order.CartonSize.ToString();
            txtCutSize.Text = order.CutSize;
            txtTiraj.Text = order.Tiraj.ToString();
            chkZincExisted.Checked = order.ZincExisted ?? false;
            chkZincSent.Checked = order.ZincSent ?? false;
            txtZincDesc.Text = order.ZincDesc;
            txtColorCount.Text = order.ColorCount.ToString();
            txtColorType.Text = order.ColorType;
            txtMachineCount.Text = order.MachineCount.ToString();
            txtMachineDesc.Text = order.MachineDesc;
            chkVernyDark.Checked = order.VernyDark ?? false;
            chkVernyClear.Checked = order.VernyClear ?? false;
            chkUVDark.Checked = order.UvDark ?? false;
            chkUVClear.Checked = order.UvClear ?? false;
            chkCelefonDark.Checked = order.CelefonDark ?? false;
            chkCelefonClear.Checked = order.CelefonClear ?? false;
            chkLabChasb.Checked = order.LabChasb ?? false;
            chkGhalebTigh.Checked = order.GhalebTigh ?? false;
            chkKlisheBarjesteh.Checked = order.KlisheBarjesteh ?? false;
            chkNimTigh.Checked = order.NimTigh ?? false;
            chkCutLine.Checked = order.CutLine ?? false;
            txtGhalebCode.Text = order.GhalebCode;
            txtKenareh.Text = order.Kenareh;
            txtDescription.Text = order.Description;
            chkIsFactored.Checked = order.IsFactored;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.RedirectToRoute("Home");
        }


        protected int GetOrderNoById(int orderId)
        {
            var order = new OrderRepository().GetById(orderId);

            if (order != null)
                return order.OrderNo;

            return 0;

        }
    }
}