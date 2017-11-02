using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PharmacyManagementSystem.Models;
using PagedList;
using PagedList.Mvc;
using System.Net;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace PharmacyManagementSystem
{
    public class PurchaseHistoryController : Controller
    {
        private PMSEntities db = new PMSEntities();

        //
        // GET: /PurchaseHistory/

        public ActionResult Index(string searchBy, string search)
        {
            var purchases = db.Purchases.Include(p => p.Supplier);

            if (searchBy == "Purchase_ID")
            {
                return View(db.Purchases.Where(x => x.Purchase_ID.Contains(search) || search == null).ToList());
            }
            else
            {
                return View(db.Purchases.Where(x => x.Supplier.Supplier_Name.Contains(search) || search == null).ToList());
            }

            //return View(purchases.ToList());
        }

        //
        // GET: /PurchaseHistory/Details/5

        public ActionResult Details(string id)
        {
             var purchasedItem = (from n in db.Batches
                            where n.Purchase_ID == id
                                  select n).ToList();
             return View(purchasedItem);  
        }

        //
        // GET: /PurchaseHistory/Edit/5

        public ActionResult Edit(string id = null)
        {
            Purchase purchase = db.Purchases.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            ViewBag.Supplier_ID = new SelectList(db.Suppliers, "ID", "Supplier_Name", purchase.Supplier_ID);
            return View(purchase);
        }

        //
        // POST: /PurchaseHistory/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Purchases.Attach(purchase);
                    db.Entry(purchase).Property(r => r.IsPaid).IsModified = true;
                    db.Entry(purchase).Property(r => r.Description).IsModified = true;
                    //db.Entry(purchase).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}",
                                                    validationError.PropertyName,
                                                    validationError.ErrorMessage);
                        }
                    }
                }
            }
            ViewBag.Supplier_ID = new SelectList(db.Suppliers, "ID", "Supplier_Name", purchase.Supplier_ID);
            return View(purchase);
        }

        //
        // GET: /PurchaseHistory/Delete/5

        public ActionResult Delete(string id = null)
        {
            Purchase purchase = db.Purchases.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            return View(purchase);
        }

        //
        // POST: /PurchaseHistory/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Purchase purchase = db.Purchases.Find(id);
            db.Purchases.Remove(purchase);
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