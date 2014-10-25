using System.Web.Mvc;
using System.Web.Routing;

namespace DrunkCheck
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Action",
                "{action}",
                new { controller = "Home", action = "Read" }
            );

            routes.MapRoute(
                "ControllerAction", 
                "{controller}/{action}", 
                new { controller = "Home", action = "Read" }
            );
        }
    }
}
