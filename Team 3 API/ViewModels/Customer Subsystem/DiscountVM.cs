using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OVS_Team_3_API.ViewModels.Customer_Subsystem
{
    public class DiscountVM
    {
        public int DiscountID { get; set; }
        public string DiscountName { get; set; }
        public string DiscountDescription { get; set; }

        public Decimal DiscountPercentage { get; set; }
    }
}