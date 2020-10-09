using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Common;
using Repository.DAL;
using Repository.Entity.Domain;

namespace Tarin
{
    public partial class NewAdv : System.Web.UI.Page
    {
        protected string ClientIDPerfix { get { return Form.FindControl("mainContent").ClientID + ClientIDSeparator; } }

        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    if (!Page.IsPostBack)
        //    {
        //        var source = new CategoryRepository().GetAllMainCats().OrderBy(a=>a.Name).ToList();

        //        var cat = new Category();
        //        cat.Id = -1;
        //        cat.Name = "انتخاب نمایید...";
                
        //        source.Insert(0,cat);

        //        drpMainCat.DataValueField = "Id";
        //        drpMainCat.DataTextField = "Name";
        //        drpMainCat.DataSource = source;
        //        drpMainCat.DataBind();

        //        Test();
        //    }
        //}

        //private void Test()
        //{
        //    var cat = new CategoryRepository().GetById(3);

        //    var drp = new DropDownList();
        //    drp.ID = "drp" + cat.Id;
        //    drp.DataSource = cat.Children;
        //    drp.DataValueField = "Id";
        //    drp.DataTextField = "Name";
        //    drp.DataBind();
        //    drp.SelectedIndexChanged += drpCat_OnSelectedIndexChanged;
        //    drp.AutoPostBack = true;

        //    updPanelCats.ContentTemplateContainer.Controls.Add(drp);

        //    ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(drp);

        //    var indexChangedTrigger = new AsyncPostBackTrigger();
        //    indexChangedTrigger.ControlID = drp.ID;
        //    indexChangedTrigger.EventName = "SelectedIndexChanged";
        //    updPanelCats.Triggers.Add(indexChangedTrigger);

        //    updPanelCats.Update();
        //}

        //protected void drpCat_OnSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (drpMainCat.SelectedValue.ToSafeInt() != -1)
        //    {
        //        var catId = drpMainCat.SelectedValue.ToSafeInt();
        //        var cat = new CategoryRepository().GetById(catId);

        //        if (cat.Children.Any())
        //        {
        //            //HtmlGenericControl divRow = new HtmlGenericControl("div");
        //            ////newControl.ID = "divCat" + ;
        //            //divRow.Attributes.Add("class", "row");

        //            //HtmlGenericControl divCol12 = new HtmlGenericControl("div");
        //            //divCol12.Attributes.Add("class", "col-sm-12");

        //            //divRow.Controls.Add(divCol12);

        //            var drp = new DropDownList();
        //            drp.ID = "drp" + cat.Id;
        //            drp.DataSource = cat.Children;
        //            drp.DataValueField = "Id";
        //            drp.DataTextField = "Name";
        //            drp.DataBind();

        //            drp.SelectedIndexChanged += drpCat_OnSelectedIndexChanged;
        //            drp.AutoPostBack = true;

        //            updPanelCats.ContentTemplateContainer.Controls.Add(drp);
        //            //divCol12.Controls.Add(drp);
        //            //divCats.Controls.Add(divRow);

        //            ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(drp);
                    
        //            var indexChangedTrigger = new AsyncPostBackTrigger();
        //            indexChangedTrigger.ControlID = drp.UniqueID;
        //            indexChangedTrigger.EventName = "SelectedIndexChanged";
        //            updPanelCats.Triggers.Add(indexChangedTrigger);

        //            updPanelCats.Update();
        //        }
        //    }
        //}

          protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                Session["Drps"] = null;
                Session["DivProps"] = null;
                CreateDrp(null,true);
            }
           
            LoadCatsFromSession();
            LoadPropFromSession();
        }

        private void LoadPropFromSession()
        {
            //var c = (HtmlGenericControl)Session["DivProps"];
            if(Session["DivProps"]!=null)
            {
                divPropsContainer.Controls.Clear();
                divPropsContainer.Controls.Add((HtmlGenericControl)Session["DivProps"]);
            }
        }

        private void LoadCatsFromSession()
        {
            divMain.Controls.Clear();
 
            if (Session["Drps"] != null)
 
                for (int i = 0; i < ((List<DrpHelper>) Session["Drps"]).Count; i++)
                {
                    ((List<DrpHelper>)Session["Drps"])[i].SelectedIndexChanged += drp_SelectedIndexChanged;
                    divMain.Controls.Add(((List<DrpHelper>)Session["Drps"])[i].Label);
                    divMain.Controls.Add(((List<DrpHelper>)Session["Drps"])[i]);
                    divMain.Controls.Add(new LiteralControl("<br />"));
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
        private void CreateDrp(int? catId, bool saveInSession)
        {
            var drp = new DrpHelper();
            var cat = new Category();

            if(catId!=null)
            {
                cat = new CategoryRepository().GetById((int)catId);
                if (cat.Children == null || !cat.Children.Any())
                {
                    rowProps.Visible = true;
                    LoadProps(cat.Id);
                    panelCommonProps.Visible = true;
                    return;
                }
                
                drp.DataSource = cat.Children;
            }
            else
            {
                drp.DataSource = new CategoryRepository().GetAllMainCats();
            }
 
            drp.DataValueField = "Id";
            drp.DataTextField = "Name";
            drp.Width = 150;
            drp.Attributes.Add("class","drpCat");
 
            drp.ID = "i1" + DateTime.Now.Ticks;
            drp.DataBind();
            drp.AutoPostBack = true;
            drp.Label = new Label();
            //drp.Label.Text = cat.Name + ":";
            drp.SelectedIndexChanged += drp_SelectedIndexChanged;
 
            //divMain.Controls.Add(drp);
            //upd.ContentTemplateContainer.Controls.Add(drp);
 
            //var t = new AsyncPostBackTrigger();
            //t.ControlID = drp.ClientID;
            //t.EventName = "SelectedIndexChanged";
            //upd.Triggers.Add(t);
            //ScriptManager.GetCurrent(Page).RegisterAsyncPostBackControl(drp);
 
            if (saveInSession)
            {
                if (Session["Drps"] == null) Session["Drps"] = new List<DrpHelper>();
                ((List<DrpHelper>)Session["Drps"]).Add(drp);
            }
        }

        private void LoadProps(int catId)
        {
            Session["Props"] = new List<CategoryPropHelper>();

            var repo = new CategoryPropRepository();
            var props = repo.GetByCatId(catId);
            var label = new Label();
            var textBox = new TextBox();
            var reqValidator = new RequiredFieldValidator();
            var propValues = new List<CategoryPropValue>();

            HtmlGenericControl divRow,divColSm3,divColSm9;

            foreach (CategoryProp prop in props)
            {

                switch (prop.Type)
                {
                    case (int)CategoryPropType.Checkbox:
                        var chk = new CheckBox();
                        chk.Text = prop.Caption;

                        divRow = GetDivRow();
                        divColSm3 = GetDivColSm(3);
                        divRow.Controls.Add(divColSm3);
                        
                        divColSm9 = GetDivColSm(9);
                        divColSm9.Controls.Add(chk);
                        divRow.Controls.Add(divColSm9);
                      
                        divProps.Controls.Add(divRow);

                        var ph = new CategoryPropHelper(chk,prop.Id);
                        ((List<CategoryPropHelper>)Session["Props"]).Add(ph);
                        break;

                    case (int)CategoryPropType.Combo:
                        label = new Label();
                        label.Text = prop.Caption + " : ";
                        var drp = new DropDownList();
                        drp.Width = 120;
                        drp.ID = "drp" + DateTime.Now.Ticks;

                        propValues = new CategoryPropValueRepository().GetPropValues(prop.Id);

                        drp.DataSource = propValues;
                        drp.DataValueField = "Id";
                        drp.DataTextField = "Value";
                        drp.DataBind();

                        divRow = GetDivRow();
                        divColSm3 = GetDivColSm(3);

                        divColSm3.Attributes.Add("align", "left");
                        divRow.Controls.Add(divColSm3);
                        divColSm3.Controls.Add(label);

                        divColSm9 = GetDivColSm(9);
                        divColSm9.Controls.Add(drp);
                        divRow.Controls.Add(divColSm9);

                        reqValidator = new RequiredFieldValidator();
                        reqValidator.ID = "pv" + prop.Id;
                        reqValidator.ControlToValidate = drp.ID;
                        reqValidator.ErrorMessage = "الزامی";
                        reqValidator.ValidationGroup = "saveValidation";
                        reqValidator.CssClass = "propReqValidator";
                        divColSm9.Controls.Add(reqValidator);
                        divProps.Controls.Add(divRow);

                        var phCombo = new CategoryPropHelper(drp,prop.Id);
                        ((List<CategoryPropHelper>)Session["Props"]).Add(phCombo);
                        break;

                    case (int)CategoryPropType.Decimal:
                    case (int)CategoryPropType.Int:
                    case (int)CategoryPropType.String:

                        label = new Label();
                        label.Text = prop.Caption + " : ";

                        divRow = GetDivRow();
                        divColSm3 = GetDivColSm(3);

                        divColSm3.Attributes.Add("align", "left");
                        divRow.Controls.Add(divColSm3);
                        divColSm3.Controls.Add(label);
                        
                        textBox = new TextBox();
                        textBox.Width = 120;
                        textBox.ID = "textBox" + DateTime.Now.Ticks;

                        divColSm9 = GetDivColSm(9);
                        divColSm9.Controls.Add(textBox);
                        divRow.Controls.Add(divColSm9);

                        reqValidator = new RequiredFieldValidator();
                        reqValidator.ID = "pv" + prop.Id;
                        reqValidator.ControlToValidate = textBox.ID;
                        reqValidator.ErrorMessage = "الزامی";
                        reqValidator.ValidationGroup = "saveValidation";
                        reqValidator.CssClass = "propReqValidator";
                        divColSm9.Controls.Add(reqValidator);

                        divProps.Controls.Add(divRow);

                        var phText = new CategoryPropHelper(textBox,prop.Id);
                        ((List<CategoryPropHelper>)Session["Props"]).Add(phText);
                        break;

                    case (int)CategoryPropType.Radio:
                        label = new Label();
                        label.Text = prop.Caption + " : ";

                        divRow = GetDivRow();
                        divColSm3 = GetDivColSm(3);

                        divColSm3.Attributes.Add("align", "left");
                        divRow.Controls.Add(divColSm3);
                        divColSm3.Controls.Add(label);

                        var rb = new RadioButtonList();
                        rb.Width = 120;
                        rb.CssClass = "propRB";
                        
                        propValues = new CategoryPropValueRepository().GetPropValues(prop.Id);

                        foreach (CategoryPropValue p in propValues)
                        {
                            rb.Items.Add(new ListItem(p.Value));
                        }
                        //todo validation
                        divColSm9 = GetDivColSm(9);
                        divColSm9.Controls.Add(rb);
                        divRow.Controls.Add(divColSm9);

                        divProps.Controls.Add(divRow);

                        var phradio = new CategoryPropHelper(rb, prop.Id);
                        ((List<CategoryPropHelper>)Session["Props"]).Add(phradio);
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

        private void drp_SelectedIndexChanged(object sender, EventArgs e)
        {
            CreateDrp(Convert.ToInt32((sender as DropDownList).SelectedValue), true);
            LoadCatsFromSession();
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (Session["Props"] == null) return;

                var props = (List<CategoryPropHelper>) Session["Props"];

            }
            catch (Exception ex)
            {
                
                throw;
            }
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