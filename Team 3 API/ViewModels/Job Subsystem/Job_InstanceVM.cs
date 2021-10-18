using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OVS_Team_3_API.ViewModels.Job_Subsystem
{
    public class Job_InstanceVM
    {
        public int JobInstanceID { get; set; }
        public Nullable<int> JobtaskID { get; set; }
        public Nullable<int> ShiftBranchEmployeeID { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }

    }
}