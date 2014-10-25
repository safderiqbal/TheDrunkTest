using System;
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