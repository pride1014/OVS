using OVS_Team_3_API.ViewModels.Product;
using OVS_Team_3_API.ViewModels.Raw_Materials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OVS_Team_3_API.ViewModels.Supplier
{
    public class SupplierOrderLineVM
    {
        public int SupplierOrderLineID { get; set; }
        public Nullable<int> SupplierOrderID { get; set; }
        public int Quantity { get; set; }
        public Nullable<int> ProductID { get; set; }
        public Nullable<int> RawMaterialID { get; set; }


    }
}