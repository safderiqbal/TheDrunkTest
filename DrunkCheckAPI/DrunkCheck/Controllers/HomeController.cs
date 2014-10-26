using System;
using System.Security.Cryptography;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using DrunkCheck.Models;
using DrunkCheckUser = DrunkCheck.Models.User;
using DrunkCheck.APIs;

namespace DrunkCheck.Controllers
{
    public class HomeController : Controller
    {
        readonly string[] _insults = {
            "numpty",
            "tit",
            "idiot",
            "wazzok",
            "noob",
            "utter genius",
            "pillock",
            "goon",
            "stooge",
            "dummy",
            "fool",
            "jive turkey"
        };

        private readonly string[] _stopSayings =
        {
            "STAPPPP",
            "NO! NOOOOOOOOOO! NO GOD NO!",
            "Just step away from the computer device...",
            "STOP! Hammer time!",
            "STOP! Collaborate and listen. Ice is back...",
            "MOVE AWAY FROM THE KEYBOARD"
        };

        public JsonResult Read()
        {
            return Json(DrunkCheckInterface.Read(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadForUser(int id = -1, string email = null, bool notifySupervisor = false, bool textSelf = false)
        {
            User user = DrunkCheckUser.Get(id, email);
            
            if (user == null)
                throw new Exception("User not found.");

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

            // Gen a random number generator for the placeholder text
            Random random = new Random(Guid.NewGuid().GetHashCode());

            if (notifySupervisor && user.SupervisorId >= 0 && reading.Value > 400)
            {
                User supervisor = DrunkCheckUser.Get(user.SupervisorId);

                //nope.avi
                String message = String.Format("Hi " + supervisor.Name + ", " + user.Name +
                                 " is trying to commit code while in the state of '" +
                                 response.drunkLevel + "'. What a {0}", _insults.GetValue(random.Next() * _insults.Length)); ;
                
                ClockWorkSms.SendMessage(supervisor.Mobile, message);
            }

            if (textSelf && reading.Value > 400)
            {
                ClockWorkSms.SendMessage(user.Mobile,
                    String.Format("{0}", _stopSayings.GetValue(random.Next() * _stopSayings.Length))
                );
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Donate(int id = -1, string email = null)
        {
            User user = DrunkCheckUser.Get(id, email);

            if (user == null)
                throw new Exception("User not found.");

            DrunkStripe.PayRandomCharity(user);

            using (DrunkCheckerContext db = new DrunkCheckerContext())
            {
                user.OverrideEnabled = true;
                db.SaveChanges();
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}