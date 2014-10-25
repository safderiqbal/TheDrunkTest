using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using DrunkCheck.Models;

namespace DrunkCheck.Controllers
{
    public class HomeController : Controller
    {
        readonly Random Random = new Random();

        public JsonResult Read(int returnValue = -1, bool staticValue = false)
        {
            if (returnValue == -1)
                returnValue = staticValue ? 100 : Random.Next(100, 400);

            return Json(new DrunkCheckResponse(returnValue), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadForUser(string username, int returnValue = -1, bool staticValue = false)
        {
            if (returnValue == -1)
                returnValue = staticValue ? 100 : Random.Next(100, 400);

            return Json(new DrunkCheckResponse(username, returnValue), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadForUserEmail(string email, int returnValue = -1)
        {
            if (returnValue == -1)
            {
                returnValue = Random.Next(100, 400);
            }

            using (DrunkCheckerContext db = new DrunkCheckerContext())
            {
                User user = db.Users.FirstOrDefault(u => u.Email == email);

                if (user == null)
                {
                    return Json("{success : fail, Value : This User does not exist.}");
                }

                Reading reading = new Reading {UserId = user.Id, DateTime = DateTime.Now, Value = returnValue};

                db.Readings.Add(reading);

                db.SaveChanges();
            }

            return Json("{success : true}", JsonRequestBehavior.AllowGet);

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
        
        public JsonResult BensTest(string username, string email)
        {
            using (DrunkCheckerContext db = new DrunkCheckerContext())
            {
                User user = new User{Email = "example@example.com", Name = "example"};
                db.Users.Add(user);

                db.SaveChanges();
            }

            return Json("{success : true}");
        }
    }
}