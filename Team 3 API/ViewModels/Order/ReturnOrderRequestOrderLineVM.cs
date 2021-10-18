using OVS_Team_3_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OVS_Team_3_API.ViewModels.Order
{
    public class ReturnOrderRequestOrderLineVM
    {
        public int ReturnOrderRequestOrderLineID { get; set; }
        public Nullable<int> ReturnOrderRequestID { get; set; }
        public Nullable<int> OrderLineID { get; set; }
        public string ReturnReason { get; set; }
        public int Quantity { get; set; }

        public virtual Order_Line OrderLine { get; set; }
        public virtual Return_Order_Request ReturnOrderRequest { get; set; }
    }
}