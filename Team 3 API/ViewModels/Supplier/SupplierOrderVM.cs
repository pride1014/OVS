using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OVS_Team_3_API.ViewModels.Supplier
{
    public class SupplierOrderVM
    {
        public int SupplierOrderID { get; set; }
        public string SupplierOrderDescription { get; set; }
        public Nullable<int> SupplierID { get; set; }
    }
}