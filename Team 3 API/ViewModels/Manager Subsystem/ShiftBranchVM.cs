using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OVS_Team_3_API.ViewModels.Employee_Subsystem
{
    public class ShiftBranchVM
    {
        public int ShiftBranchID { get; set; }
        public Nullable<int> ShiftID { get; set; }
        public Nullable<int> BranchID { get; set; }
    }
}