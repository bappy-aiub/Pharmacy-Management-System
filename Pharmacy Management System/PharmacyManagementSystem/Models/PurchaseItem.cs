using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacyManagementSystem.Models
{
    public class PurchaseItem
    {
        public int ID { get; set; }
        public int Batch_ID { get; set; }
        public int Quantity { get; set; }
        public int Cost_Price { get; set; }
        public int Sell_Price { get; set; }
        public System.DateTime Production_Date { get; set; }
        public System.DateTime Expire_Date { get; set; }
        public string Purchase_ID { get; set; }
        public int Medicine_ID { get; set; }
    }
}