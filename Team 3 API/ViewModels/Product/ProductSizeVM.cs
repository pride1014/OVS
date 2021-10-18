using OVS_Team_3_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OVS_Team_3_API.ViewModels.Product
{
    public class ProductSizeVM
    {
        public int ProductSizeID { get; set; }
        public Nullable<int> PriceID { get; set; }
        public Nullable<int> ProductID { get; set; }
        public Nullable<int> SizeID { get; set; }

        public int? Quantityonhand { get; set; }

        public byte[] ProductImage { get; set; }

        public double PriceAmount { get; set; }

        public string ProductName { get; set; }
        public string ProductDescription { get; set; }

        public string SizeDescription { get; set; }


    }
}