using OVS_Team_3_API.ViewModels.Order;
using OVS_Team_3_API.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OVS_Team_3_API.ViewModels.Wishlist
{
    public class WishlistProductVM
    {
        public int WishlistProductID { get; set; }
        public Nullable<int> ProductID { get; set; }
        public Nullable<int> WishlistID { get; set; }
        public int Quantity { get; set; }

    }
}