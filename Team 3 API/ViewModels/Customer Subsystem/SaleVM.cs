using OVS_Team_3_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OVS_Team_3_API.ViewModels.Customer_Subsystem
{
    public class SaleVM
    {
        public int SaleID { get; set; }
        public System.DateTime SaleDate { get; set; }
        public Nullable<int> RequestQuoteID { get; set; }
    }
}