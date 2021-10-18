using OVS_Team_3_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OVS_Team_3_API.ViewModels.Customer_Subsystem
{
    public class SaleLineVM
    {
        public int SaleLineID { get; set; }
        public Nullable<int> SaleID { get; set; }
        public Nullable<int> ProductID { get; set; }
        public int Quantity { get; set; }
    }
}