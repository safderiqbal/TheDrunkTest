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
        // GET: User
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

            return Json("{success : true, userId : " + user.Id + "}", JsonRequestBehavior.AllowGet);
        }
    }
}