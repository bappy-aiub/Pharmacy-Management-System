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
    public class BillHistoryController : Controller
    {
        private PMSEntities db = new PMSEntities();

        //
        // GET: /BillHistory/

        public ActionResult Index(string search)
        {
            return View(db.Bill_Information.Where(x => x.Invoice_No.Contains(search) || search == null).ToList());
        }

        //
        // GET: /BillHistory/Details/5

        public ActionResult Details(string id)
        {
            var purchasedItem = (from n in db.Sales
                                 where n.Bill_Invoice == id
                                 select n).ToList();
            return View(purchasedItem);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}