using System;
using System.Web.Mvc;
using System.Web.WebPages;
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

            if (action.Trim().Equals("override", StringComparison.InvariantCultureIgnoreCase))
            {
                result = SmsOverride(from, actionValue);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public bool SmsRead(string from, string email)
        {
            User user = GetUser(from, email);
            DrunkCheckResponse response = DrunkHelpers.Read(user);

            ClockWorkSms.SendMessage(from,
                "You read a value of " + response.value +
                ", which must mean you are Drunk Level '" + response.drunkLevel + "'");

            return true;
        }

        public bool SmsOverride(string from, string email)
        {
            DrunkHelpers.Override(GetUser(from, email));
            return true;
        }

        public bool SmsDonate(string from, string email)
        {
            DrunkHelpers.Donate(GetUser(from, email));
            return true;
        }

        private static User GetUser(string from, string email)
        {
            User user = null;
            if (email.Trim().IsEmpty())
            {
                user = DrunkCheckUser.Get(email: email);
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