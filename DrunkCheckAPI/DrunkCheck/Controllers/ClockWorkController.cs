using System;
using System.Web.Mvc;
using DrunkCheck.APIs;
using DrunkCheck.Models;
using DrunkCheckUser = DrunkCheck.Models.User;

namespace DrunkCheck.Controllers
{
    public class ClockWorkController : Controller
    {
        public JsonResult SendSms(string recipientNumber, string message)
        {
            bool success = ClockWorkSms.SendMessage(recipientNumber, message);
            return Json(new {success}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReceiveSms(string to, string from, string content, string keyword)
        {
            string realContent = content.Substring(11);

            string action = realContent.Trim().Substring(0, realContent.IndexOf(" ", System.StringComparison.Ordinal) + 1);
            string actionValue =
                realContent.Trim()
                    .Substring(realContent.IndexOf(" ", System.StringComparison.Ordinal) + 1, realContent.Length);

            bool result = false;

            if (action.Equals("read", StringComparison.InvariantCultureIgnoreCase))
            {
               result = SmsRead(from, actionValue);
            }

            if (action.Equals("donate", StringComparison.InvariantCultureIgnoreCase))
            {
                result = SmsDonate();
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public bool SmsRead(string from, string email)
        {
            User user = null;
            if (email.Trim() != "")
            {
                user = DrunkCheckUser.Get(-1, email);
            }

            if (user == null)
            {
                ClockWorkSms.SendMessage(from, "Sorry but the that user has not been found. Blame Jeff");
                return false;
            }

            Reading reading;

            DrunkCheckResponse response = DrunkCheckInterface.Read(user);
            using (DrunkCheckerContext db = new DrunkCheckerContext())
            {
                reading = new Reading
                {
                    UserId = user.Id,
                    DateTime = DateTime.Now,
                    Value = response.value
                };

                db.Readings.Add(reading);
                db.SaveChanges();
            }

            ClockWorkSms.SendMessage(from,
                "You read a value of " + reading.Value +
                ", which must mean you are Drunk Level '" + response.drunkLevel + "'");

            return true;
        }

        public bool SmsDonate()
        {
            return true;
        }
    }
}