using System.Web.Mvc;
using DrunkCheck.Models;

namespace DrunkCheck.Controllers
{
    public class HomeController : Controller
    {
        public JsonResult Read(int returnValue = -1, bool staticValue = false)
        {
            return Json(DrunkCheckInterface.Read(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadForUser(string username, int returnValue = -1, bool staticValue = false)
        {
            return Json(DrunkCheckInterface.Read(username), JsonRequestBehavior.AllowGet);
        }
    }
}