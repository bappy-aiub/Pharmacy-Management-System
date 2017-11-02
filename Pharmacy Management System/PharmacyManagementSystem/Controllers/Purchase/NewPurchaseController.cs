using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PharmacyManagementSystem.Models;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace PharmacyManagementSystem.Controllers.Purchase
{
    public class NewPurchaseController : Controller
    {
        //
        // GET: /NewPurchase/
        PMSEntities db = new PMSEntities();

        public ActionResult Index()
        {
            var batch_information = db.Batches.Include(u => u.Batch_ID);

            ViewBag.Supplier_ID = new SelectList(db.Suppliers, "ID", "Supplier_Name");
            ViewBag.Medicine_ID = new SelectList(db.Medicine_Information, "ID", "Medicine_Name");

            return View();
        }

        [HttpPost]
        public JsonResult CheckBatchID(int? batchID)
        {
            bool status = false;

            if (batchID != null)
            {
                var batchId = from n in db.Batches
                              where n.Batch_ID == batchID
                              select n;

                if (batchId.Any())
                {
                    status = true;
                }
                else
                {
                    status = false;
                }
            }
               
            return new JsonResult { Data = new { status = status } };
        }  


        public JsonResult getSupplier()
        {
            //holds list of suppliers
            List<Supplier> _supplierList = new List<Supplier>();

            //queries all the suppliers for its ID and Name property.
            var supplierList = (from s in db.Suppliers
                                select new { s.ID, s.Supplier_Name }).ToList();

            //save list of suppliers to the _supplierList
            foreach (var supplier in supplierList)
            {
                _supplierList.Add(new Supplier
                {
                    ID = supplier.ID,
                    Supplier_Name = supplier.Supplier_Name
                });
            }

            //returns the Json result of _supplierList
            return Json(_supplierList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getMedicine()
        {
            //holds list of suppliers
            List<Medicine_Information> _medicineList = new List<Medicine_Information>();

            //queries all the suppliers for its ID and Name property.
            var medicineList = (from m in db.Medicine_Information
                                select new { m.ID, m.Medicine_Name }).ToList();

            //save list of suppliers to the _supplierList
            foreach (var medicine in medicineList)
            {
                _medicineList.Add(new Medicine_Information
                {
                    ID = medicine.ID,
                    Medicine_Name = medicine.Medicine_Name
                });
            }

            //returns the Json result of _supplierList
            return Json(_medicineList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SavePurchase(PurchaseEntryVM p)
        {
            bool status = false;

            if (p != null)
            {
                //new purchase object using the data from the viewmodel : PurchaseEntryVM
                var purchase = new PharmacyManagementSystem.Models.Purchase
                {
                    Purchase_ID = p.Purchase_ID,
                    Entry_Date = p.Entry_Date,
                    Supplier_ID = p.Supplier_ID,
                    Amount = p.Amount,
                    Discount = p.Discount.ToString(),
                    Grand_Total = p.Grand_Total,
                    IsPaid = p.IsPaid,
                    Description = p.Description,
                    Discount_Amount = p.Discount_Amount
                };

                var batchs = p.PurchaseItems.Select(i => new PharmacyManagementSystem.Models.Batch
                {
                    Batch_ID = i.Batch_ID,
                    Quantity = i.Quantity,
                    Cost_Price = i.Cost_Price,
                    Sell_Price = i.Sell_Price,
                    Production_Date = i.Production_Date,
                    Expire_Date = i.Expire_Date,
                    Purchase_ID = p.Purchase_ID,
                    Medicine_ID = i.Medicine_ID
                }).ToList();

                //db.Purchases.Attach(purchase);
                db.Purchases.Add(purchase);

                foreach (var b in batchs)
                {
                    //db.Batches.Attach(b);
                    db.Batches.Add(b);
                }


                try
                {
                    db.SaveChanges();
                }
                catch
                {
                    Response.Write("<script>alert('Batch Number needs to be unqiue')</script>");
                }
              


                status = true;
            }
            // return the status in form of Json
            return new JsonResult { Data = new { status = status } };
        } 
    }
}
