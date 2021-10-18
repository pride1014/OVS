using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OVS_Team_3_API.ViewModels.Product
{
    public class ProductSpecialVM
    {
        public int ProductSpecialID { get; set; }
        public Nullable<int> ProductSizeID { get; set; }
        public Nullable<int> SpecialID { get; set; }
        public float PriceAmount { get; set; }
        public virtual ProductSizeVM ProductSize { get; set; }
        public virtual SpecialVM Special { get; set; }

    }
}