using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DrunkCheck.Models;

namespace DrunkCheck.Controllers
{
    public class UserController : Controller
    {
        public JsonResult CreateUser(string name, string email)
        {
            User user;
            using (DrunkCheckerContext db = new DrunkCheckerContext())
            {
                user = new User
                {
                    Name = name,
                    Email = email
                };

                db.Users.Add(user);

                db.SaveChanges();
            }

            return Json(user, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUser(int userId = -1, string email = null)
        {

            if (userId == -1 && email == null)
            {
                return Json("{success : false}", JsonRequestBehavior.AllowGet);
            }

            User user;
            using (DrunkCheckerContext db = new DrunkCheckerContext())
            {
                user = userId != -1 ? db.Users.FirstOrDefault(u => u.Id == userId) : db.Users.FirstOrDefault(u => u.Email == email);
                
            }

            return Json(user, JsonRequestBehavior.AllowGet);
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
    }
}