using Common;
using Repository.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace MehranPack
{
    /// <summary>
    /// Summary description for DataHandler
    /// </summary>
    public class DataHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                if (context.Request["id"] == null)
                    return;

                if (!int.TryParse(context.Request["id"], out int id))
                    return;

                var serializer = new JavaScriptSerializer();

                var details = new WorksheetDetailRepository().GetAllDetails(id).ToList();

                if (details!=null && details.ToList().Any())
                {
                    var json = serializer.Serialize(details);
                    context.Response.ContentType = "application/json";
                    context.Response.Write(json);
                }
            }
            catch (Exception ex)
            {
                Debuging.Error(ex.Message + "\r\n" + ex.StackTrace);
            }
            
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}