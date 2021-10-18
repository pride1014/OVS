using OVS_Team_3_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OVS_Team_3_API.ViewModels.Raw_Materials
{
    public class RawMaterialVM
    {
        public int RawMaterialID { get; set; }
        public string RawMaterialName { get; set; }
        public int QuantityOnhand { get; set; }
        public string Rawmaterialdescription { get; set; }
        public Nullable<int> UnitID { get; set; }

        public string UnitMeasurement { get; set; }

      

        public virtual Unit Unit { get; set; }
    }
}