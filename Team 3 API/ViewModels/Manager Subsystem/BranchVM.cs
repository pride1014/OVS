using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OVS_Team_3_API.ViewModels.Manager_Subsystem
{
    public class BranchVM
    {
        public int BranchID { get; set; }
        public string BranchName { get; set; }
        public int BranchLocationStorageCapacity { get; set; }
        public string BranchAddress { get; set; }

    }
}