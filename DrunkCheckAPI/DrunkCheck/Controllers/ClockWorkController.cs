using System.Web.Mvc;
using DrunkCheck.APIs;

namespace DrunkCheck.Controllers
{
    public class ClockWorkController : Controller
    {
        public JsonResult ClockworkTest()
        {
            bool success = ClockWorkSms.SendTestMessage();
            return Json("{success: " + success.ToString().ToLower() + "}", JsonRequestBehavior.AllowGet);
        }
    }
}