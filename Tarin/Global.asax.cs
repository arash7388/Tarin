using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.WebHost;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using Microsoft.AspNet.FriendlyUrls;

namespace Tarin
{
    public class Global : System.Web.HttpApplication
    {
        #region Classes To Provide Session Support For WebApi
        public class MyHttpControllerHandler : HttpControllerHandler, IRequiresSessionState
        {
            public MyHttpControllerHandler(RouteData routeData): base(routeData)
            { }
        }

        public class MyHttpControllerRouteHandler : HttpControllerRouteHandler
        {
            protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
            {
                return new MyHttpControllerHandler(requestContext.RouteData);
            }
        }

        #endregion 

        protected void Application_Start(object sender, EventArgs e)
        {
            //RouteTable.Routes.MapPageRoute("Error", "App/Error/{Data}", "~/Error.aspx");
            //RouteTable.Routes.MapPageRoute("UserEdit", "User/UserEdit/{UserId}", "~/NewAdv.aspx");

            RouteTable.Routes.MapPageRoute("NewAdv", "NewAdv", "~/UserEdit.aspx");
            
            RouteTable.Routes.Ignore("*.html|js|css|gif|jpg|jpeg|png|swf");
            RouteTable.Routes.EnableFriendlyUrls();

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
            RouteTable.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = System.Web.Http.RouteParameter.Optional }
                ).RouteHandler = new MyHttpControllerRouteHandler();

            RouteTable.Routes.MapHttpRoute(
                name: "GetPagedData",
                routeTemplate: "api/{controller}/{action}/{catId}/{pageNumber}/{pageSize}"
            );

            RouteTable.Routes.MapHttpRoute(
                name: "GetMenuList",
                routeTemplate: "apimenu/{controller}/{action}/{parentId}"
            );
            //RouteTable.Routes.MapHttpRoute(
            //name: "ActionApi",
            //routeTemplate: "api2/{controller}/{action}/{id}");

            //GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}