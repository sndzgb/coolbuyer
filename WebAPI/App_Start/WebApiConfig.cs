using CoolBuyer.Server.WebApi.Filters;
using CoolBuyer.Server.WebApi.Handlers;
using CoolBuyer.Server.WebApi.Handlers.MessageHandlers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace CoolBuyer.Server.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            //config.SuppressDefaultHostAuthentication();
            config.SuppressHostPrincipal();

            // Web API routes
            config.MapHttpAttributeRoutes();
            //config.Filters.Add(new AuthenticateAttribute());
            //config.Filters.Add(new AuthenticationFilter());

            //config.Filters.Add(new ModelValidationFilter());

            //config.MessageHandlers.Add(new CheckRouteExistsMessageHandler());

            // serializer settings
            config.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Formatting.Indented;
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;
            //config.Formatters.JsonFormatter.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            //config.Formatters.JsonFormatter.SerializerSettings.DateParseHandling = DateParseHandling.DateTime;
            //config.Formatters.JsonFormatter.SerializerSettings.DateFormatString = "dd/MM/yyyy";
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            config.MessageHandlers.Add(new RequireHttpsHandler());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //config.Services.Replace(typeof(IExceptionLogger), new UnhandledExceptionLogger());
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
            
            config.Filters.Add(new AuthenticationFilter());
            config.Filters.Add(new GlobalExceptionFilterAttribute());
        }
    }
}
