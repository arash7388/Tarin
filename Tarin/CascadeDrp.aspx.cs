using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Common;
using Repository.DAL;
using Repository.Entity.Domain;
using Telerik.Web.UI;

namespace Tarin
{
    public partial class CascadeDrp : System.Web.UI.Page
    {
       protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack && catIdHF.Value.ToSafeInt()!=0)
            {
                rowProps.Style.Clear();
                rowProps.Attributes.Add("visibility","visible");

                var catId = catIdHF.Value.ToSafeInt();

                divAllCats.Visible = false;
                divTravercedDats.Visible = true;

                lblTraversedCats.Text = new CategoryRepository().GetFullName(catId);

                CreateCatProps(catId);
            }
            else
            {
                Session["CatProps"] = null;

                drpArea.DataSource = new AreaRepository().GetAllByOrder();
                drpArea.DataValueField = "Id";
                drpArea.DataTextField = "Name";
                drpArea.DataBind();
            }
          
        }
        
        protected string GetUploadAllowedFileExtensions()
        {
            string result = "";

            try
            {
                string[] extsWithoutDot = new string[asyncUploadPic.AllowedFileExtensions.Count()];

                for (int i = 0; i < asyncUploadPic.AllowedFileExtensions.Count(); i++)
                {
                    var withoutDot = asyncUploadPic.AllowedFileExtensions[i].Replace(".", "");
                    extsWithoutDot[i] = withoutDot;
                }

                result = String.Join(" , ", extsWithoutDot);

            }
            catch (Exception ex)
            {
                Debuging.Error(ex, MethodBase.GetCurrentMethod().Name);
            }

            return result;
        }
        
        private void CreateCatProps(int catId)
        {
            Session["CatProps"] = new List<KeyValuePair<int, object>>();

            var repo = new CategoryPropRepository();
            var props = repo.GetByCatId(catId);
            var label = new Label();
            var textBox = new TextBox();
            var reqValidator = new RequiredFieldValidator();
            var propValues = new List<CategoryPropValue>();

            HtmlGenericControl divRow, divColSm3Xs3, divColSm6Xs7,divColSm3Xs2;


            foreach (CategoryProp prop in props)
            {
               
                switch (prop.Type)
                {
                    case (int)CategoryPropType.Checkbox:
                        var chk = new CheckBox();
                        chk.Text = prop.Caption;
                        chk.ID = "chk" + prop.Id;

                        ((List<KeyValuePair<int, object>>)Session["CatProps"]).Add(new KeyValuePair<int, object>(prop.Id,chk));
                        
                        divRow = GetDivRow();
                        divColSm3Xs3 = GetDivColSm3Xs3();
                        divRow.Controls.Add(divColSm3Xs3);

                        divColSm6Xs7 = GetDivColSm6Xs7();
                        divColSm6Xs7.Controls.Add(chk);
                        divRow.Controls.Add(divColSm6Xs7);

                        divProps.Controls.Add(divRow);
                        
                        break;

                    case (int)CategoryPropType.Combo:
                        label = new Label();
                        label.Text = prop.Caption + " : ";
                        var drp = new DropDownList();
                        //drp.Width = 120;
                        drp.ID = "drp" + prop.Id;

                        ((List<KeyValuePair<int, object>>)Session["CatProps"]).Add(new KeyValuePair<int, object>(prop.Id,drp)); 
                        propValues = new CategoryPropValueRepository().GetPropValues(prop.Id);

                        drp.Attributes.Add("class", "form-control padding0");
                        drp.DataSource = propValues;
                        drp.DataValueField = "Id";
                        drp.DataTextField = "Value";
                        drp.DataBind();

                        divRow = GetDivRow();
                        divColSm3Xs3 = GetDivColSm3Xs3();

                        divColSm3Xs3.Attributes.Add("align", "left");
                        divRow.Controls.Add(divColSm3Xs3);
                        divColSm3Xs3.Controls.Add(label);

                        divColSm6Xs7 = GetDivColSm6Xs7();
                        divColSm6Xs7.Controls.Add(drp);
                        divRow.Controls.Add(divColSm6Xs7);

                        reqValidator = new RequiredFieldValidator();
                        reqValidator.ID = "pv" + prop.Id;
                        reqValidator.ControlToValidate = drp.ID;
                        reqValidator.ErrorMessage = "الزامی";
                        reqValidator.ValidationGroup = "saveValidation";
                        reqValidator.CssClass = "propReqValidator";
                        divColSm6Xs7.Controls.Add(reqValidator);
                        divProps.Controls.Add(divRow);
                        break;

                    case (int)CategoryPropType.Decimal:
                    case (int)CategoryPropType.Int:
                    case (int)CategoryPropType.String:

                        label = new Label();
                        label.Text = prop.Caption + " : ";

                        divRow = GetDivRow();
                        divColSm3Xs3 = GetDivColSm3Xs3();

                        divColSm3Xs3.Attributes.Add("align", "left");
                        divRow.Controls.Add(divColSm3Xs3);
                        divColSm3Xs3.Controls.Add(label);

                        textBox = new TextBox();
                        //textBox.Width = 120;
                        textBox.ID = "txt" + prop.Id;
                        ((List<KeyValuePair<int, object>>)Session["CatProps"]).Add(new KeyValuePair<int, object>(prop.Id, textBox)); 
                        
                        textBox.Attributes.Add("class", "form-control");

                        divColSm6Xs7 = GetDivColSm6Xs7();
                        divColSm6Xs7.Controls.Add(textBox);
                        divRow.Controls.Add(divColSm6Xs7);

                        divColSm3Xs2 = GetDivColSm3Xs2();

                        reqValidator = new RequiredFieldValidator();
                        reqValidator.ID = "pv" + prop.Id;
                        reqValidator.ControlToValidate = textBox.ID;
                        reqValidator.ErrorMessage = "الزامی";
                        reqValidator.ValidationGroup = "saveValidation";
                        reqValidator.CssClass = "propReqValidator";
                        divColSm3Xs2.Controls.Add(reqValidator);
                        divRow.Controls.Add(divColSm3Xs2);

                        divProps.Controls.Add(divRow);
                        
                        break;

                    case (int)CategoryPropType.Radio:
                        label = new Label();
                        label.Text = prop.Caption + " : ";

                        divRow = GetDivRow();
                        divColSm3Xs3 = GetDivColSm3Xs3();

                        divColSm3Xs3.Attributes.Add("align", "left");
                        divRow.Controls.Add(divColSm3Xs3);
                        divColSm3Xs3.Controls.Add(label);

                        var rb = new RadioButtonList();
                        rb.ID = "rb" + prop.Id;
                        ((List<KeyValuePair<int, object>>)Session["CatProps"]).Add(new KeyValuePair<int, object>(prop.Id, rb)); 
                        //rb.Width = 120;
                        rb.CssClass = "propRB";

                        propValues = new CategoryPropValueRepository().GetPropValues(prop.Id);

                        foreach (CategoryPropValue p in propValues)
                        {
                            rb.Items.Add(new ListItem(p.Value,p.Id.ToString()));
                        }

                        //todo validation
                        divColSm6Xs7 = GetDivColSm6Xs7();
                        divColSm6Xs7.Controls.Add(rb);
                        divRow.Controls.Add(divColSm6Xs7);

                        divProps.Controls.Add(divRow);
                        break;
                }

                Session["DivProps"] = divProps;
            }
        }

        private static HtmlGenericControl GetDivColSm(int cols)
        {
            HtmlGenericControl divColSm = new HtmlGenericControl("div");
            divColSm.Attributes.Add("class", "col-sm-" + cols);
            return divColSm;
        }

        private static HtmlGenericControl GetDivRow()
        {
            HtmlGenericControl divRow = new HtmlGenericControl("div");
            divRow.Attributes.Add("class", "row");
            return divRow;
        }

        private static HtmlGenericControl GetDivColSm3Xs3()
        {
            HtmlGenericControl divColSm = new HtmlGenericControl("div");
            divColSm.Attributes.Add("class", "col-sm-3 col-xs-3");
            return divColSm;
        }

        private static HtmlGenericControl GetDivColSm6Xs7()
        {
            HtmlGenericControl divColSm = new HtmlGenericControl("div");
            divColSm.Attributes.Add("class", "col-sm-6 col-xs-7");
            return divColSm;
        }

        private static HtmlGenericControl GetDivColSm3Xs2()
        {
            HtmlGenericControl divColSm = new HtmlGenericControl("div");
            divColSm.Attributes.Add("class", "col-sm-3 col-xs-2");
            return divColSm;
        }
        
        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            try
            {
                var uow = new UnitOfWork();
                
                var adv = new Advertisement();
                adv.CategoryId = catIdHF.Value.ToSafeInt();
                adv.Title = txtTitle.Text;
                adv.AreaId = drpArea.SelectedValue.ToSafeInt();
                adv.AdvEmail = txtEmail.Text;
                adv.Desc = txtDesc.Text;
                adv.HideEmail = chkHideEmail.Checked;
                adv.AdvTel = txtTel.Text;

                adv.AdvertisementPics = new List<AdvertisementPic>();

                foreach (UploadedFile file in asyncUploadPic.UploadedFiles)
                {
                    var p = new AdvertisementPic();

                    byte[] bytes = new byte[file.ContentLength];
                    file.InputStream.Read(bytes, 0, (int)file.ContentLength);
                    p.PicHigh = bytes;
                    adv.AdvertisementPics.Add(p);
                }
                
                    
                adv.AdvertisementPropValues = new List<AdvertisementPropValues>();

                if (Session["CatProps"] == null) return;

                var props = (List<KeyValuePair<int,object>>)Session["CatProps"];

                foreach (KeyValuePair<int,object> keyValue in props)
                {
                    var pvalue = new AdvertisementPropValues();
                    pvalue.Advertisement = adv;
                    pvalue.CategoryPropId = keyValue.Key;

                    if (keyValue.Value is CheckBox)
                    {
                        var chk = keyValue.Value as CheckBox;
                        pvalue.Value = chk.Checked.ToString();
                    }
                    else if (keyValue.Value is DropDownList)
                    {
                        var drp = keyValue.Value as DropDownList;
                        pvalue.Value = drp.SelectedValue;
                    }
                    else if (keyValue.Value is TextBox)
                    {
                        var txt = keyValue.Value as TextBox;
                        pvalue.Value = txt.Text;
                    }
                    else if (keyValue.Value is RadioButtonList)
                    {
                        var rb = keyValue.Value as RadioButtonList;
                        pvalue.Value = rb.SelectedValue.ToSafeString();
                    }

                    adv.AdvertisementPropValues.Add(pvalue);
                }

                uow.Advertisements.Create(adv);
                uow.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        public class DrpHelper : DropDownList
        {
            public Label Label { get; set; }
        }

        public class CategoryPropHelper
        {
            public object Object { get; set; }
            public int PropId { get; set; }

            public CategoryPropHelper(object o, int propId)
            {
                Object = o;
                PropId = propId;
            }

            public CategoryPropHelper()
            {
            }
        }

    }
}