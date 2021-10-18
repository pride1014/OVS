using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OVS_Team_3_API.ViewModels.Order
{
    public class CartVM
    {
        public int CartID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> CustomerID { get; set; }


        public int? CartLineID { get; set; }
     
        public int? ProductID { get; set; }
        public int? Quantity { get; set; }
    }
}