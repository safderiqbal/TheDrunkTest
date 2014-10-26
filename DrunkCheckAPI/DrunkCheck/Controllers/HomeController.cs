using System;
using System.Text;
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

            bool notificationSent = false;

            if (notifySupervisor && user.SupervisorId >= 0 && reading.Value > 400)
            {
                User supervisor = DrunkCheckUser.Get(user.SupervisorId);

                //nope.avi
                String message = "Hi " + supervisor.Name + ", " + user.Name +
                                 " is trying to commit code while in the state of '" +
                                 response.drunkLevel + "'. What a tit";

                notificationSent = ClockWorkSms.SendMessage(supervisor.Mobile, message);
            }
            
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}