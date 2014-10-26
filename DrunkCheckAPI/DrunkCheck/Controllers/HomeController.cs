using System;
using System.Web.Mvc;
using DrunkCheck.Models;
using DrunkCheckUser = DrunkCheck.Models.User;
using DrunkCheck.APIs;

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

            if (notifySupervisor && user.SupervisorId >= 0 && reading.Value > 400)
            {
                User supervisor = DrunkCheckUser.Get(user.SupervisorId);

                //nope.avi
                String message = "Hi " + supervisor.Name + ", " + user.Name +
                                 " is trying to commit code while in the state of '" +
                                 response.drunkLevel + "'. What a tit";

                ClockWorkSms.SendMessage(supervisor.Mobile, message);
            }

            if (textSelf && reading.Value > 400)
            {
                ClockWorkSms.SendMessage(user.Mobile, "STAPPPP");
            }

            if (notifyIce && reading.Value > 400)
            {
                ClockWorkSms.SendMessage(user.EmergancyContact,
                    string.Format("Erm...{0}, has tried to do stupid stuff while drunk. Please stop them."), user.Name);
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Donate(int id = -1, string email = null)
        {
            User user = DrunkCheckUser.Get(id, email);
            DrunkDonate.Donate(user);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}