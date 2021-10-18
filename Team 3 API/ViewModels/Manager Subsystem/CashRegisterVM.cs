using OVS_Team_3_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OVS_Team_3_API.ViewModels.Manager_Subsystem
{
    public class CashRegisterVM
    {

        public int RegisterID { get; set; }
        public string CashRegisterName { get; set; }
        public Nullable<int> BranchID { get; set; }
    }
}