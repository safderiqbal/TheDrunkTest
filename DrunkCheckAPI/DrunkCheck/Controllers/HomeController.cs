using System;
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

        public JsonResult ReadForUser(int id = -1, string email = null)
        {
            User user = DrunkCheckUser.Get(id, email);
            
            if (user == null)
                throw new Exception("User not found.");

            DrunkCheckResponse response = DrunkCheckInterface.Read(user);
            using (DrunkCheckerContext db = new DrunkCheckerContext())
            {
                Reading reading = new Reading
                {
                    UserId = user.Id,
                    DateTime = DateTime.Now,
                    Value = response.value
                };

                db.Readings.Add(reading);
                db.SaveChanges();
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}