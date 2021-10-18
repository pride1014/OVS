using OVS_Team_3_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OVS_Team_3_API.ViewModels.Customer_Subsystem
{
    public class ReturnSaleRequestVM
    {
        // GET: ReturnSaleRequestVM
        public int ReturnSaleRequestID { get; set; }
        public System.DateTime ReturnRequestDate { get; set; }
        public Nullable<int> CustomerID { get; set; }
    }
}