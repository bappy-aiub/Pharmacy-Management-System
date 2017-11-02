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
    public class MedicineController : Controller
    {
        private PMSEntities db = new PMSEntities();

        //
        // GET: /Medicine/

        public ActionResult Index(string searchBy, string search)
        {
            if (searchBy == "Medicine_Name")
            {
                return View(db.Medicine_Information.Where(x => x.Medicine_Name.Contains(search) || search == null).ToList());
            }
            else if (searchBy == "Category")
            {
                return View(db.Medicine_Information.Where(x => x.Category.Contains(search) || search == null).ToList());
            }
            else if (searchBy == "Genric_Name")
            {
                return View(db.Medicine_Information.Where(x => x.Drug_Generic_name.Genric_Name.Contains(search) || search == null).ToList());
            }
            else
            {
                return View(db.Medicine_Information.Where(x => x.Manufacturer.Manufacturer_Name.Contains(search) || search == null).ToList());
            }

            //var medicine_information = db.Medicine_Information.Include(m => m.Drug_Generic_name).Include(m => m.Manufacturer);
            //return View(medicine_information.ToList());
        }

  

        //
        // GET: /Medicine/Details/5

        public ActionResult Details(int id = 0)
        {
            Medicine_Information medicine_information = db.Medicine_Information.Find(id);
            if (medicine_information == null)
            {
                return HttpNotFound();
            }
            return View(medicine_information);
        }

        //
        // GET: /Medicine/Create

        public ActionResult Create()
        {
            ViewBag.Generic_ID = new SelectList(db.Drug_Generic_name, "ID", "Genric_Name");
            ViewBag.Manufacturer_ID = new SelectList(db.Manufacturers, "ID", "Manufacturer_Name");
            return View();
        }

        //
        // POST: /Medicine/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Medicine_Information medicine_information)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Medicine_Information.Add(medicine_information);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ModelState.AddModelError("Error", "Generic Name Already Assigned");
                }
            }

            ViewBag.Generic_ID = new SelectList(db.Drug_Generic_name, "ID", "Genric_Name", medicine_information.Generic_ID);
            ViewBag.Manufacturer_ID = new SelectList(db.Manufacturers, "ID", "Manufacturer_Name", medicine_information.Manufacturer_ID);
            return View(medicine_information);
        }

        //
        // GET: /Medicine/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Medicine_Information medicine_information = db.Medicine_Information.Find(id);
            if (medicine_information == null)
            {
                return HttpNotFound();
            }
            //ViewBag.Generic_ID = new SelectList(db.Drug_Generic_name, "ID", "Genric_Name", medicine_information.Generic_ID);
            ViewBag.Manufacturer_ID = new SelectList(db.Manufacturers, "ID", "Manufacturer_Name", medicine_information.Manufacturer_ID);
            return View(medicine_information);
        }

        //
        // POST: /Medicine/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Medicine_Information medicine_information)
        {
            if (ModelState.IsValid)
            {
                db.Medicine_Information.Attach(medicine_information);
                db.Entry(medicine_information).Property(r => r.Medicine_Name).IsModified = true;
                db.Entry(medicine_information).Property(r => r.Category).IsModified = true;
                db.Entry(medicine_information).Property(r => r.Manufacturer_ID).IsModified = true;
               // db.Entry(medicine_information).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(medicine_information);
            }
             //ViewBag.Generic_ID = new SelectList(db.Drug_Generic_name, "ID", "Genric_Name", medicine_information.Generic_ID);
           // ViewBag.Manufacturer_ID = new SelectList(db.Manufacturers, "ID", "Manufacturer_Name", medicine_information.Manufacturer_ID);
           
        }

        //
        // GET: /Medicine/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Medicine_Information medicine_information = db.Medicine_Information.Find(id);
            if (medicine_information == null)
            {
                return HttpNotFound();
            }
            return View(medicine_information);
        }

        //
        // POST: /Medicine/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Medicine_Information medicine_information = db.Medicine_Information.Find(id);
            db.Medicine_Information.Remove(medicine_information);
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