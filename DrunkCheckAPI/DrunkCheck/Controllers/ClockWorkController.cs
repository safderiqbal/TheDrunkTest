using System;
using System.Web.Mvc;
using DrunkCheck.APIs;

namespace DrunkCheck.Controllers
{
    public class ClockWorkController : Controller
    {
        public JsonResult SendSms(int recipientNumber, string message)
        {
            bool success = ClockWorkSms.SendMessage(recipientNumber, message);
            return Json(new {success}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReceiveSms(string to, string from, string content, string keyword)
        {
            ClockWorkSms.SendMessage(Convert.ToInt32(from), content);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}