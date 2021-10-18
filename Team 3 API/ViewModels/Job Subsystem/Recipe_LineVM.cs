using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OVS_Team_3_API.ViewModels.Job_Subsystem
{
    public class Recipe_LineVM
    {
        public int RecipleLineID { get; set; }
        public int Quantity { get; set; }
        public Nullable<int> RecipeID { get; set; }
        public Nullable<int> RawMaterialID { get; set; }
        public Nullable<int> RecipeLineUnitID { get; set; }

        public string RecipeDescription { get; set; }
        public int QuantityProduced { get; set; }
        public string RecipeName { get; set; }
    }
}