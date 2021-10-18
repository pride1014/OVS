using OVS_Team_3_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OVS_Team_3_API.ViewModels.Order
{
    public class ReturnOrderRequestVM
    {
        public int ReturnOrderRequest_ID { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public Nullable<int> OrderID { get; set; }
        public System.DateTime RequestOrderDate { get; set; }

    
        public virtual Customer Customer { get; set; }
        public virtual Models.Order Order { get; set; }
    }
}