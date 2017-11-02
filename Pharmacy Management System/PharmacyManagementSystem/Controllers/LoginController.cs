using PharmacyManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PharmacyManagementSystem.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        private PMSEntities db = new PMSEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Login(User_Access users)
        {
            if (ModelState.IsValid)
            {
                var details = from user in db.User_Access
                              where user.Username == users.Username && user.Password == users.Password
                              select user;

                if (details.FirstOrDefault() != null)
                {
                    if (details.FirstOrDefault().Usertype == "Admin")
                    {
                        Session["Username"] = details.FirstOrDefault().Username;
                        Session["Usertype"] = details.FirstOrDefault().Usertype;
                        return RedirectToAction("AdminDashboard", "Dashboard");
                    }
                    else
                    {
                        Session["Username"] = details.FirstOrDefault().Username;
                        Session["Usertype"] = details.FirstOrDefault().Usertype;
                        return RedirectToAction("StaffDashboard", "Dashboard");
                    }
                }
                else
                {
                    ModelState.AddModelError("Error", "Wrong Username or Password");
                }

            }

            else
            {
                ModelState.AddModelError("", "Invalid");
            }

            return View(users);
        }

    }
}
