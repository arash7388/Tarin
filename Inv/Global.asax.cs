using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using Microsoft.AspNet.FriendlyUrls;

namespace Inv
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            //RouteTable.Routes.MapPageRoute("Login", "Login", "~/Login.aspx");
            //RouteTable.Routes.MapPageRoute("Home", "Home", "~/Home.aspx");
            RouteTable.Routes.MapPageRoute("Confirmation", "Confirmation", "~/Confirmation.aspx");
            RouteTable.Routes.MapPageRoute("CustomerList", "CustomerList", "~/CustomerList.aspx");
            //RouteTable.Routes.MapPageRoute("OrderList", "OrderList", "~/OrderList.aspx");
            //RouteTable.Routes.MapPageRoute("FactorList", "FactorList", "~/FactorList.aspx");
            //RouteTable.Routes.MapPageRoute("PaymentList", "PaymentList", "~/PaymentList.aspx");
            //RouteTable.Routes.MapPageRoute("FactorListActionResult", "FactorList/{ActionResult}", "~/FactorList.aspx");
            //RouteTable.Routes.MapPageRoute("PaymentListActionResult", "PaymentList/{ActionResult}", "~/PaymentList.aspx");
            RouteTable.Routes.MapPageRoute("Customer", "Customer/{Id}", "~/Customer.aspx");
            //RouteTable.Routes.MapPageRoute("Factor", "Factor/{Id}", "~/Factor.aspx");
            //RouteTable.Routes.MapPageRoute("FactorsReport", "FactorsReport", "~/FactorsReport.aspx");
            //RouteTable.Routes.MapPageRoute("FactorPrint", "FactorPrint/{Id}", "~/FactorPrint.aspx");
            //RouteTable.Routes.MapPageRoute("Order", "Order/{Id}", "~/Order.aspx");
            //RouteTable.Routes.MapPageRoute("OrdersReport", "OrdersReport", "~/OrdersReport.aspx");
            //RouteTable.Routes.MapPageRoute("OrderPrint", "OrderPrint/{Id}", "~/OrderPrint.aspx");
            //RouteTable.Routes.MapPageRoute("Payment", "Payment/{Id}", "~/Payment.aspx");
            //RouteTable.Routes.MapPageRoute("PaymentsReport", "PaymentsReport", "~/PaymentsReport.aspx");
            //RouteTable.Routes.MapPageRoute("ProductTypeList", "ProductTypeList", "~/ProductTypeList.aspx");
            //RouteTable.Routes.MapPageRoute("ProductType", "ProductType/{Id}", "~/ProductTypeList.aspx");

            RouteTable.Routes.Ignore("*.html|js|css|gif|jpg|jpeg|png|swf");
            RouteTable.Routes.EnableFriendlyUrls();
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}