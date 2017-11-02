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
    public class DrugGenericNameController : Controller
    {
        private PMSEntities db = new PMSEntities();

        //
        // GET: /DrugGenericName/

        public ActionResult Index(string searchGenericName)
        {
            var genericName = from m in db.Drug_Generic_name
                               select m;
            if (!String.IsNullOrEmpty(searchGenericName))
            {
                genericName = genericName.Where(m => m.Genric_Name.Contains(searchGenericName));
            }

            return View(genericName);
        }

        //
        // GET: /DrugGenericName/Details/5

        public ActionResult Details(int id = 0)
        {
            Drug_Generic_name drug_generic_name = db.Drug_Generic_name.Find(id);
            if (drug_generic_name == null)
            {
                return HttpNotFound();
            }
            return View(drug_generic_name);
        }

        //
        // GET: /DrugGenericName/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /DrugGenericName/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Drug_Generic_name drug_generic_name)
        {
            if (ModelState.IsValid)
            {
                db.Drug_Generic_name.Add(drug_generic_name);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(drug_generic_name);
        }

        //
        // GET: /DrugGenericName/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Drug_Generic_name drug_generic_name = db.Drug_Generic_name.Find(id);
            if (drug_generic_name == null)
            {
                return HttpNotFound();
            }
            return View(drug_generic_name);
        }

        //
        // POST: /DrugGenericName/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Drug_Generic_name drug_generic_name)
        {
            if (ModelState.IsValid)
            {
                db.Entry(drug_generic_name).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(drug_generic_name);
        }

        //
        // GET: /DrugGenericName/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Drug_Generic_name drug_generic_name = db.Drug_Generic_name.Find(id);
            if (drug_generic_name == null)
            {
                return HttpNotFound();
            }
            return View(drug_generic_name);
        }

        //
        // POST: /DrugGenericName/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Drug_Generic_name drug_generic_name = db.Drug_Generic_name.Find(id);
            db.Drug_Generic_name.Remove(drug_generic_name);
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