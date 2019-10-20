using MoodBustersLibrary;
using System;
using System.Web.Http;

namespace MoodBustersWebAPI
{
    public static class WebApiConfig
    {
        public static IRecognitionApi recognitionApi { get; } = new AmazonRekognitionApi();

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
        }
    }
}
