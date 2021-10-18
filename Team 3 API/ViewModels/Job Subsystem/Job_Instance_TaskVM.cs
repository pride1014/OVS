using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OVS_Team_3_API.ViewModels.Job_Subsystem
{
    public class Job_Instance_TaskVM
    {
        public int JobInstanceTaskID { get; set; }
        public Nullable<int> JobTaskID { get; set; }
        public Nullable<int> JobInstanceID { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
    }
}