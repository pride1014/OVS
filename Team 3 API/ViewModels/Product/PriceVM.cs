using OVS_Team_3_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OVS_Team_3_API.ViewModels.Product
{
    public class PriceVM
    {
        public int PriceID { get; set; }
        public float PriceAmount { get; set; }
        public System.DateTime PriceDate { get; set; }

        public int? ProductSizeID { get; set; }

        public virtual Product_Size ProductSize { get; set; }
    }
}