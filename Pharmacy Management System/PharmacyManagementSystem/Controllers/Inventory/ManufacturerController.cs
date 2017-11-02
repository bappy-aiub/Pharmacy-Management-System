using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PharmacyManagementSystem.Models;

namespace PharmacyManagementSystem.Controllers.Inventory
{
    public class ManufacturerController : Controller
    {
        private PMSEntities db = new PMSEntities();

        //
        // GET: /Manufacturer/

        public ActionResult Index(string searchManufacturer)
        {
            var manufacturer = from m in db.Manufacturers
                               select m;
            if (!String.IsNullOrEmpty(searchManufacturer))
            {
                manufacturer = manufacturer.Where(m => m.Manufacturer_Name.Contains(searchManufacturer));
            }

            return View(manufacturer);
        }

        

        //
        // GET: /Manufacturer/Details/5

        public ActionResult Details(int id = 0)
        {
            Manufacturer manufacturer = db.Manufacturers.Find(id);
            if (manufacturer == null)
            {
                return HttpNotFound();
            }
            return View(manufacturer);
        }

        //
        // GET: /Manufacturer/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Manufacturer/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Manufacturer manufacturer)
        {
            if (ModelState.IsValid)
            {
                db.Manufacturers.Add(manufacturer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(manufacturer);
        }

        //
        // GET: /Manufacturer/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Manufacturer manufacturer = db.Manufacturers.Find(id);
            if (manufacturer == null)
            {
                return HttpNotFound();
            }
            return View(manufacturer);
        }

        //
        // POST: /Manufacturer/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Manufacturer manufacturer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(manufacturer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(manufacturer);
        }

        //
        // GET: /Manufacturer/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Manufacturer manufacturer = db.Manufacturers.Find(id);
            if (manufacturer == null)
            {
                return HttpNotFound();
            }
            return View(manufacturer);
        }

        //
        // POST: /Manufacturer/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Manufacturer manufacturer = db.Manufacturers.Find(id);
            db.Manufacturers.Remove(manufacturer);
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