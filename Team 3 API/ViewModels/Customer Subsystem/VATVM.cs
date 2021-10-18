using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OVS_Team_3_API.ViewModels.Customer_Subsystem
{
    public class VATVM
    {
        public int VATID { get; set; }
        public int VATPercentage { get; set; }
        public System.DateTime VATDate { get; set; }
    }
}