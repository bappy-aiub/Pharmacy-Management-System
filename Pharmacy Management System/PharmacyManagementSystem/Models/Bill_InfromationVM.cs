using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacyManagementSystem.Models
{
    public class Bill_InfromationVM
    {
        public string Invoice_No { get; set; }
        public double Total_Amount { get; set; }
        public string Discount { get; set; }
        public double Discount_Amount { get; set; }
        public double Total_Payable { get; set; }
        public double Paid { get; set; }
        public double Returned { get; set; }
        public System.DateTime Date { get; set; }

        public List<SalesVM> SalesItems { get; set; }
    }
}