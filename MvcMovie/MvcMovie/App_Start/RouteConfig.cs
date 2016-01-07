using System.Web.Mvc;
using System.Web.Routing;

namespace MvcMovie
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Admin",
                url: "admin/{action}/{id}",
                defaults: new
                {
                    controller = "AdminUser",
                    action = "Login",
                    id = UrlParameter.Optional
                });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "MovieClient",
                    action = "Index",
                    id = UrlParameter.Optional
                });
        }
    }
}