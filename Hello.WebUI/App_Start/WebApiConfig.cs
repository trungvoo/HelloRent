using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Hello.WebUI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();


            // Format Json
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Device API route
            config.Routes.MapHttpRoute(
                name: "DeviceApi",
                routeTemplate: "api/{controller}/{token}/{type}/{version}/{location}/{latitude}/{longitude}"
                );

            // Device API Account
            config.Routes.MapHttpRoute(
                name: "AccountApi",
                routeTemplate: "api/{controller}/{userName}/{password}/{type}"
                );
        }
    }
}
