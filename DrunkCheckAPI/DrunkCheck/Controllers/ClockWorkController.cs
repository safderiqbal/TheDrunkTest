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
            string action = content.Substring(0, content.IndexOf(" ", StringComparison.Ordinal) + 1);
            string actionValue = content.Substring(content.IndexOf(" ", StringComparison.Ordinal) + 1);

            bool result = false;

            if (action.Trim().Equals("read", StringComparison.InvariantCultureIgnoreCase))
            {
               result = SmsRead(from, actionValue);
            }

            if (action.Trim().Equals("donate", StringComparison.InvariantCultureIgnoreCase))
            {
                result = SmsDonate(from, actionValue);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public bool SmsRead(string from, string email)
        {
            User user = GetUser(from, email);

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

        public bool SmsDonate(string from, string email)
        {
            DrunkStripe.PayRandomCharity(GetUser(from, email));
            return true;
        }

        private static User GetUser(string from, string email)
        {
            User user = null;
            if (email.Trim() != "")
            {
                user = DrunkCheckUser.Get(-1, email);
            }

            if (user == null)
            {
                ClockWorkSms.SendMessage(from, "Sorry but the that user has not been found. Blame Jeff");
                throw new Exception("Sorry but the that user has not been found. Blame Jeff");
            }

            return user;
        }
    }
}