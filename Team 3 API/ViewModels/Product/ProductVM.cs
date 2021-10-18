using OVS_Team_3_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OVS_Team_3_API.ViewModels.Product
{
    public class ProductVM
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public byte[] ProductImage { get; set; }
        public int? Quantityonhand { get; set; }
        public int? ProductTypeID { get; set; }

        public string ProductTypeName { get; set; }

        public virtual ICollection<Product_Size> ProductSize { get; set; }

    }
}