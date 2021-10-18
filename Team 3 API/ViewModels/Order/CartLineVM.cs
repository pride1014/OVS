using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OVS_Team_3_API.ViewModels.Order
{
    public class CartLineVM
    {
        public int CartLineID { get; set; }
        public Nullable<int> CartID { get; set; }
        public Nullable<int> ProductID { get; set; }
        public Nullable<int> Quantity { get; set; }

        public Nullable<int> UserID { get; set; }
        public Nullable<int> CustomerID { get; set; }

    }
}