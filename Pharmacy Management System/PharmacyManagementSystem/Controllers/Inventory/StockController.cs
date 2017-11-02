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
    public class StockController : Controller
    {
        private PMSEntities db = new PMSEntities();

        //
        // GET: /Stock/

        public ActionResult Index(string search)
        {
            var batches = db.Batches.Include(b => b.Medicine_Information).Include(b => b.Purchase);

            return View(db.Batches.Where(x => x.Medicine_Information.Medicine_Name.Contains(search) || search == null).ToList());

            //return View(batches.ToList());
        }

        //
        // GET: /Stock/Details/5

        public ActionResult Details(int id = 0)
        {
            Batch batch = db.Batches.Find(id);
            if (batch == null)
            {
                return HttpNotFound();
            }
            return View(batch);
        }

        //
        // GET: /Stock/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Batch batch = db.Batches.Find(id);
            if (batch == null)
            {
                return HttpNotFound();
            }
            ViewBag.Medicine_ID = new SelectList(db.Medicine_Information, "ID", "Medicine_Name", batch.Medicine_ID);
            ViewBag.Purchase_ID = new SelectList(db.Purchases, "Purchase_ID", "Discount", batch.Purchase_ID);
            return View(batch);
        }

        //
        // POST: /Stock/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Batch batch)
        {
            if (ModelState.IsValid)
            {
                db.Batches.Attach(batch);
                db.Entry(batch).Property(r => r.Sell_Price).IsModified = true;
                //db.Entry(batch).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Medicine_ID = new SelectList(db.Medicine_Information, "ID", "Medicine_Name", batch.Medicine_ID);
            ViewBag.Purchase_ID = new SelectList(db.Purchases, "Purchase_ID", "Discount", batch.Purchase_ID);
            return View(batch);
        }

        //
        // GET: /Stock/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Batch batch = db.Batches.Find(id);
            if (batch == null)
            {
                return HttpNotFound();
            }
            return View(batch);
        }

        //
        // POST: /Stock/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Batch batch = db.Batches.Find(id);
            db.Batches.Remove(batch);
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