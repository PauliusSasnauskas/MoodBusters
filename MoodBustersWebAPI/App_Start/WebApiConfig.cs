using MoodBustersLibrary;
using System;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Xml.Serialization;

namespace MoodBustersWebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //DbTest.Main(null);
        }
    }
}
