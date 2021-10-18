using OVS_Team_3_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OVS_Team_3_API.ViewModels.Customer_Subsystem
{
    public class ReturnSaleRequestSaleLineVM
    {
        // GET: ReturnSaleRequestSaleLineVM
        public int ReturnSaleRequestSaleLineID { get; set; }
        public string ReturnSaleReason { get; set; }
        public Nullable<int> ReturnSaleRequestID { get; set; }
        public Nullable<int> SaleLineID { get; set; }
        public Nullable<int> Quantity { get; set; }
    }
}