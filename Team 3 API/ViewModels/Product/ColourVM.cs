using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OVS_Team_3_API.ViewModels.Product
{
    public class ColourVM
    {
        public int ColourID { get; set; }
        public string ColourDescription { get; set; }
        public Nullable<int> ProductSizeID { get; set; }

    }
}