using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using AjaxControlToolkit;
using Common;
using Repository.DAL;
using Repository.Entity.Domain;
using Telerik.Web.UI.PersistenceFramework;

namespace Tarin
{
   
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]

    [System.Web.Script.Services.ScriptService()]
    public class WebService1 : System.Web.Services.WebService
    {
        [WebMethod]
        public CascadingDropDownNameValue[] GetSubCats(string knownCategoryValues)
        {
            var values = knownCategoryValues.Split(';');
            var thisValue = values[values.Length - 2];

            string id = CascadingDropDown.ParseKnownCategoryValuesString(thisValue)["Id"];
            var subCats = new CategoryRepository().GetAllByParentId(id.ToSafeInt());
            List<CascadingDropDownNameValue> countries = GetDataArray(subCats);
            return countries.ToArray();
        }

        [WebMethod]
        public bool CatHasSubCat(int catId)
        {
            return new CategoryRepository().GetById(catId).Children.Any();
        }

        [WebMethod]
        public CascadingDropDownNameValue[] GetMainCats()
        {
            var mainCats = new CategoryRepository().GetAllMainCats();
            List<CascadingDropDownNameValue> countries = GetDataArray(mainCats);
            return countries.ToArray();
        }
        
        private List<CascadingDropDownNameValue> GetDataArray(List<Category> cats)
        {
            List<CascadingDropDownNameValue> values = new List<CascadingDropDownNameValue>();

            foreach (Category c in cats)
            {
                values.Add(new CascadingDropDownNameValue
                {
                    name = c.Name.ToString(),
                    value = c.Id.ToString()
                });
            }

            return values;
        }
    }
}
