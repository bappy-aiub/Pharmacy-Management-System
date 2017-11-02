using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacyManagementSystem.Models
{
    public class PurchaseEntryVM
    {
        public int ID { get; set; }
        public string Purchase_ID { get; set; }
        public int Amount { get; set; }
        public string Discount { get; set; }
        public double Discount_Amount { get; set; }
        public double Grand_Total { get; set; }
        public string IsPaid { get; set; }
        public System.DateTime Entry_Date { get; set; }
        public string Description { get; set; }
        public int Supplier_ID { get; set; }

        //public string ID { get; set; }
        //public DateTime Date { get; set; }
        //public int SupplierID { get; set; }
        //public int PurchaseID { get; set; }
        //public int Amount { get; set; }
        //public int Discount { get; set; }

        //public int Discount_Amount { get; set; }
        //public double GrandTotal { get; set; }
        //public string IsPaid { get; set; }
        //public string Description { get; set; }
        //public DateTime LastUpdated { get; set; }

        public List<PurchaseItem> PurchaseItems { get; set; }
    }
}