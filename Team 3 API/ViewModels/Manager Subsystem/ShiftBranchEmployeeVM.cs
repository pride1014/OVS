using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OVS_Team_3_API.ViewModels.Manager_Subsystem
{
    public class ShiftBranchEmployeeVM
    {
        public int ShiftBranchEmployeeID { get; set; }
        public Nullable<int> ShiftBranchID { get; set; }
        public Nullable<int> EmployeeID { get; set; }
    }
}