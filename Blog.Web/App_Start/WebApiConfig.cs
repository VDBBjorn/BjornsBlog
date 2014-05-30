using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;
using Blog.Web.Filters;
using Newtonsoft.Json.Serialization;

namespace Blog.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "Topics",
                routeTemplate: "api/v1/topics/{topicId}",
                defaults: new {controller = "topics", topicId = RouteParameter.Optional}
            );

            config.Routes.MapHttpRoute(
                name: "RepliesRoute",
                routeTemplate: "api/v1/topics/{topicid}/replies/{id}",
                defaults: new { controller = "replies", topicid = RouteParameter.Optional, id = RouteParameter.Optional }
            );
            
            //config.Filters.Add(new ForceHttpsAttribute());

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            var corsAttribute = new EnableCorsAttribute("*", "*", "GET,POST");
            config.EnableCors(corsAttribute);
        }
    }
}
