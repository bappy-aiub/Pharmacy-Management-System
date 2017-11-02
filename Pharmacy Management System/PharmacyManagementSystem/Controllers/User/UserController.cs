using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PharmacyManagementSystem.Models;

namespace PharmacyManagementSystem.Controllers.User
{
   
    public class UserController : Controller
    {
       
        private PMSEntities db = new PMSEntities();

        //
        // GET: /User/

        public ActionResult Index()
        {
            var user_information = db.User_Information.Include(u => u.User_Access);
            return View(user_information.ToList());
        }

        //
        // GET: /User/Details/5

        public ActionResult Details(int id = 0)
        {
            User_Information user_information = db.User_Information.Find(id);
            if (user_information == null)
            {
                return HttpNotFound();
            }
            return View(user_information);
        }

        //
        // GET: /User/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /User/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserViewModel model)
        {
            try
            {
                User_Access userAccess = new User_Access()
                {
                    Username = model.Username,
                    Password = model.Password,
                    Usertype = model.Usertype
                };

                User_Information userInfo = new User_Information();
                userInfo.Name = model.Name;
                userInfo.Email = model.Email;
                userInfo.Gender = model.Gender;
                userInfo.Date_of_Birth = model.Date_of_Birth;
                userInfo.Age = model.Age;
                userInfo.Address = model.Address;
                userInfo.Contact = model.Contact;
                userInfo.Blood_Group = model.Blood_Group;
                userInfo.Marital_Status = model.Marital_Status;
                userInfo.Join_Date = model.Join_Date;
                userInfo.Salary = model.Salary;
                userInfo.Username = model.Username;
                userInfo.Name = model.Name;
                userInfo.User_Access = userAccess;

                db.User_Information.Add(userInfo);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return RedirectToAction("Index", model);
            //return View(model);
        }

        //public ActionResult Create(User_Information user_information)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.User_Information.Add(user_information);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.Username = new SelectList(db.User_Access, "Username", "Password", user_information.Username);
        //    return View(user_information);
        //}

        //
        // GET: /User/Edit/5

        public ActionResult Edit(int id, string username)
        {
            User_Information user_information = db.User_Information.Find(id);
            User_Access user_access = db.User_Access.Find(username);
            if (user_information == null)
            {
                return HttpNotFound();
            }
            //ViewBag.Username = new SelectList(db.User_Access, "Username", "Password", user_information.Username);
            return View(user_information);
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User_Information user_information, User_Access user_access)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(user_access).State = EntityState.Modified;
                //db.Entry(user_information).State = EntityState.Modified;

                var userInfoQuery = from userInfo in db.User_Information
                                    where userInfo.Username == user_information.Username
                                    select userInfo;

                User_Information objUserInfo = userInfoQuery.FirstOrDefault();

                objUserInfo.Name = user_information.Name;
                objUserInfo.Email = user_information.Email;
                objUserInfo.Gender = user_information.Gender;
                objUserInfo.Age = user_information.Age;
                objUserInfo.Address = user_information.Address;
                objUserInfo.Contact = user_information.Contact;
                objUserInfo.Blood_Group = user_information.Blood_Group;
                objUserInfo.Marital_Status = user_information.Marital_Status;
                objUserInfo.Salary = user_information.Salary;


                var userAccessQuery = from userAccess in db.User_Access
                                      where userAccess.Username == user_access.Username
                                      select userAccess;

                User_Access objUserAccess = userAccessQuery.FirstOrDefault();

                objUserAccess.Password = user_access.Password;
                objUserAccess.Usertype = user_access.Usertype;
                

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Username = new SelectList(db.User_Access, "Username", "Password", user_information.Username);
            return View(user_information);
        }

        //
        // GET: /User/Delete/5

        public ActionResult Delete(int id = 0)
        {
            User_Information user_information = db.User_Information.Find(id);
            if (user_information == null)
            {
                return HttpNotFound();
            }
            return View(user_information);
        }

        //
        // POST: /User/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id,string username)
        {
            User_Information user_information = db.User_Information.Find(id);
            User_Access user_access = db.User_Access.Find(username);
            db.User_Information.Remove(user_information);
            db.User_Access.Remove(user_access);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}