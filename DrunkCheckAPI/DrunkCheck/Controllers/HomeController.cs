using System;
using System.Linq;
using System.Web.Mvc;
using DrunkCheck.Models;

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
            using (DrunkCheckerContext db = new DrunkCheckerContext())
            {
                User user = id == -1 
                    ? db.Users.FirstOrDefault(u => u.Email == email) 
                    : db.Users.FirstOrDefault(u => u.Id == id);

                if (user == null)
                    return Json("{success : fail, Value : This User does not exist.}");

                DrunkCheckResponse response = DrunkCheckInterface.Read(user);

                Reading reading = new Reading
                {
                    UserId = user.Id,
                    DateTime = DateTime.Now,
                    Value = response.value
                };

                db.Readings.Add(reading);
                db.SaveChanges();

                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }
    }
}