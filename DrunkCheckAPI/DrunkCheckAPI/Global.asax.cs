using System.Web;
using System.Web.Mvc;

namespace DrunkCheckAPI
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
        }
    }
}
