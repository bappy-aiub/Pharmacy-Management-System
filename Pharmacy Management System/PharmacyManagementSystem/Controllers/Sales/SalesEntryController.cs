using PharmacyManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PharmacyManagementSystem.Controllers.Sales
{
    public class SalesEntryController : Controller
    {
        PMSEntities db = new PMSEntities();
        //
        // GET: /SalesEntry/

        public ActionResult Index(string search)
        {
            return View(db.Batches.Where(x => x.Medicine_Information.Medicine_Name.Contains(search) || search == null).ToList());
            //return View(db.Batches.ToList());
        }

        public JsonResult SaveSales(Bill_InfromationVM b)
        {
            bool status = false;

            if (b != null)
            {
                //new purchase object using the data from the viewmodel : PurchaseEntryVM
                var billInfo = new PharmacyManagementSystem.Models.Bill_Information
                {
                    Invoice_No = b.Invoice_No,
                    Total_Amount = b.Total_Amount,
                    Discount = b.Discount,
                    Discount_Amount = b.Discount_Amount,
                    Total_Payable = b.Total_Payable,
                    Paid = b.Paid,
                    Returned = b.Returned,
                    Date = b.Date,
                };

                var sales = b.SalesItems.Select(i => new PharmacyManagementSystem.Models.Sale
                {

                    Quantity = i.Quantity,
                    Cost = i.Cost,
                    Amount = i.Amount,
                    Medicine_ID = i.Medicine_ID,
                    Bill_Invoice = i.Bill_Invoice,
                }).ToList();

                //db.Purchases.Attach(purchase);
                db.Bill_Information.Add(billInfo);

                foreach (var c in sales)
                {
                    
                    db.Sales.Add(c);
                }

  
                    db.SaveChanges();

                status = true;
            }

            // return the status in form of Json
            return new JsonResult { Data = new { status = status } };
        } 

    }
}
