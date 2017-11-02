using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PharmacyManagementSystem.Models;

namespace PharmacyManagementSystem.Controllers
{
    public class SalesHistoryController : Controller
    {
        private PMSEntities db = new PMSEntities();

        //
        // GET: /SalesHistory/

        public ActionResult Index()
        {
            var count = (from o in db.Sales
                         select o).Count();
            //var sales = db.Sales.Include(s => s.Bill_Information).Include(s => s.Medicine_Information);
            return View(count);
        }

        //
        // GET: /SalesHistory/Details/5

        public ActionResult Details(int id = 0)
        {
            Sale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        //
        // GET: /SalesHistory/Create

        public ActionResult Create()
        {
            ViewBag.Bill_Invoice = new SelectList(db.Bill_Information, "Invoice_No", "Discount");
            ViewBag.Medicine_ID = new SelectList(db.Medicine_Information, "ID", "Medicine_Name");
            return View();
        }

        //
        // POST: /SalesHistory/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Sale sale)
        {
            if (ModelState.IsValid)
            {
                db.Sales.Add(sale);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Bill_Invoice = new SelectList(db.Bill_Information, "Invoice_No", "Discount", sale.Bill_Invoice);
            ViewBag.Medicine_ID = new SelectList(db.Medicine_Information, "ID", "Medicine_Name", sale.Medicine_ID);
            return View(sale);
        }

        //
        // GET: /SalesHistory/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Sale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            ViewBag.Bill_Invoice = new SelectList(db.Bill_Information, "Invoice_No", "Discount", sale.Bill_Invoice);
            ViewBag.Medicine_ID = new SelectList(db.Medicine_Information, "ID", "Medicine_Name", sale.Medicine_ID);
            return View(sale);
        }

        //
        // POST: /SalesHistory/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Sale sale)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sale).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Bill_Invoice = new SelectList(db.Bill_Information, "Invoice_No", "Discount", sale.Bill_Invoice);
            ViewBag.Medicine_ID = new SelectList(db.Medicine_Information, "ID", "Medicine_Name", sale.Medicine_ID);
            return View(sale);
        }

        //
        // GET: /SalesHistory/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Sale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        //
        // POST: /SalesHistory/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sale sale = db.Sales.Find(id);
            db.Sales.Remove(sale);
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