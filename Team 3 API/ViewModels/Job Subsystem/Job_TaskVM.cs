using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OVS_Team_3_API.ViewModels.Job_Subsystem
{
    public class Job_TaskVM
    {
        public int JobtaskID { get; set; }
        public Nullable<int> JobID { get; set; }
        public Nullable<int> TaskID { get; set; }
        public Nullable<int> JobTaskStatusID { get; set; }
        public Nullable<int> JobTaskTypeID { get; set; }
    }
}