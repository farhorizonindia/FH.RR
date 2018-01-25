using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using Newtonsoft.Json;

/// <summary>
/// Summary description for Global
/// </summary>
public class Global : System.Web.HttpApplication
{
    protected void Application_Start(object sender, EventArgs e)
    {

        try
        {
            RouteTable.Routes.MapHttpRoute(
            //GlobalConfiguration.Configuration.Routes.MapHttpRoute(
                 name: "MyAPI",
                routeTemplate: "api/{controller}/{pakackagename}/{departure}/{AgentId}",
                defaults: new
                {
                    pakackagename = System.Web.Http.RouteParameter.Optional,
                    departure = System.Web.Http.RouteParameter.Optional,
                    AgentId = RouteParameter.Optional
                }
            );

            GlobalConfiguration.Configuration.Routes.MapHttpRoute(
           name: "DefaultGetLockRoomApi",
           routeTemplate: "api/{controller}/{roomno}/{accomid}/{roomcatid}/{AgentId}",
           defaults: new
           {
               roomno = RouteParameter.Optional,
               accomid = RouteParameter.Optional,
               roomcatid = RouteParameter.Optional,
               AgentId= RouteParameter.Optional
           });

             
            GlobalConfiguration.Configuration.Routes.MapHttpRoute(
            //RouteTable.Routes.MapHttpRoute(
         name: "DefaultApi",
         routeTemplate: "api/{controller}/{pakackagename}/{chekindate}/{checkoutdate}/{total}/{refrence}/{roomno}/{accomid}/{roomcatid}/{paymentmethod}/{agentid}",
         defaults: new
         {
             pakackagename = RouteParameter.Optional,
             chekindate = RouteParameter.Optional,
             checkoutdate = RouteParameter.Optional,
             total = RouteParameter.Optional,
             refrence = RouteParameter.Optional,
             roomno = RouteParameter.Optional,
             accomid = RouteParameter.Optional,
             roomcatid = RouteParameter.Optional,
             paymentmethod = RouteParameter.Optional,
             agentid = RouteParameter.Optional
         }
     );
            GlobalConfiguration.Configuration.Routes.MapHttpRoute(
            //RouteTable.Routes.MapHttpRoute(
        name: "GetdepartureApi",
        routeTemplate: "api/{controller}/{AgentId}",
        defaults: new
        {
            AgentId = RouteParameter.Optional,
            //pakackagename = RouteParameter.Optional,
            //chekindate = RouteParameter.Optional,
            //checkoutdate = RouteParameter.Optional,
            //total = RouteParameter.Optional,
            //refrence = RouteParameter.Optional,
            //roomno = RouteParameter.Optional,
            //accomid = RouteParameter.Optional,
            //roomcatid = RouteParameter.Optional,
            //paymentmethod = RouteParameter.Optional

        }
    );


            //RouteTable.Routes.MapHttpRoute(
            //       name: "DefaultGetdepartureApi",
            //       routeTemplate: "api/{controller}/{roomno}/{accomid}/{roomcatid}",
            //       defaults: new
            //       {

            //           roomno = RouteParameter.Optional,
            //           accomid = RouteParameter.Optional,
            //           roomcatid = RouteParameter.Optional

            //       }
            //   );

            //RouteTable.Routes.MapHttpRoute("API Default", "api/{controller}/{roomno}/{accomid}/{roomcatid}",
            //new
            //{
            //    roomno = RouteParameter.Optional,
            //    accomid = RouteParameter.Optional,
            //    roomcatid = RouteParameter.Optional
            //});
            //    RouteTable.Routes.MapHttpRoute("DefaultApiPost", "Api/{controller}", new { action = "Post" }, new {  });

        }

        catch (Exception ex) { }
    }
}