using OVS_Team_3_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OVS_Team_3_API.ViewModels.Product
{
    public class ProductTypeVM
    {
        public int ProductTypeID { get; set; }
        public string ProductTypeName { get; set; }
        public int? ProductCategoryID { get; set; }

        public string ProductCategoryName { get; set; }

        public virtual Product_Category Product_Category { get; set; }


    }
}