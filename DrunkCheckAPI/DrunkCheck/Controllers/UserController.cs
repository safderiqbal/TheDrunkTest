using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using DrunkCheck.Models;
using DrunkCheckUser = DrunkCheck.Models.User;

namespace DrunkCheck.Controllers
{
    public class UserController : Controller
    {
        public JsonResult CreateUser(string name, string email, string mobile, int supervisorId)
        {
            User user;
            using (DrunkCheckerContext db = new DrunkCheckerContext())
            {
                user = new User
                {
                    Name = name,
                    Email = email,
                    Mobile = mobile,
                    SupervisorId = supervisorId
                };

                db.Users.Add(user);

                db.SaveChanges();
            }

            return Json(user, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUser(int userId = -1, string email = null)
        {
            return Json(DrunkCheckUser.Get(userId, email), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetResultsForUser(int userId)
        {
            List<Reading> readings;
            using (DrunkCheckerContext db = new DrunkCheckerContext())
            {
                readings = db.Readings.Where(r => r.UserId == userId).ToList();
            }

            return Json(readings, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateUser(int userId, string email = null, string mobile = null, int supervisorId = -1)
        {
            User user;
            using (DrunkCheckerContext db = new DrunkCheckerContext())
            {
                user = db.Users.FirstOrDefault(u => u.Id == userId);
                if (user != null)
                {
                    user.Mobile = mobile ?? user.Mobile;
                    user.Email = email ?? user.Email;
                    user.SupervisorId = (supervisorId != -1) ? supervisorId : user.SupervisorId;
                }
                db.SaveChanges();
            }
            return Json(user, JsonRequestBehavior.AllowGet);
        }
    }
}