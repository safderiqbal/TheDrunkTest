using System.Web.Mvc;
using DrunkCheck.Models;
using DrunkCheckUser = DrunkCheck.Models.User;

namespace DrunkCheck.Controllers
{
    public class HomeController : Controller
    {
        public JsonResult Read()
        {
            return Json(DrunkCheckInterface.Read(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadForUser(int id = -1, string email = null, 
                                        bool notifySupervisor = false, bool textSelf = false, bool notifyIce = false)
        {
            User user = DrunkCheckUser.Get(id, email);
            DrunkCheckResponse response = DrunkHelpers.Read(user, notifySupervisor, textSelf, notifyIce);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Donate(int id = -1, string email = null)
        {
            User user = DrunkCheckUser.Get(id, email);
            DrunkHelpers.Donate(user);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}