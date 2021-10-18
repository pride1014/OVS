using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OVS_Team_3_API.ViewModels.Job_Subsystem
{
    public class RecipeVM
    {
        public int RecipeID { get; set; }
        public string RecipeDescription { get; set; }
        public int QuantityProduced { get; set; }
        public string RecipeName { get; set; }

    }
}