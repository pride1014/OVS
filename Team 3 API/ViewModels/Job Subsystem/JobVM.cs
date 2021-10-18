using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OVS_Team_3_API.ViewModels.Job_Subsystem
{
    public class JobVM
    {
        public int JobID { get; set; }
        public string JobDescription { get; set; }
        public Nullable<int> JobStatusID { get; set; }
        public Nullable<int> ProductID { get; set; }
    }
}