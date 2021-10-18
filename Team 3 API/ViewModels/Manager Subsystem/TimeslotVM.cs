using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OVS_Team_3_API.ViewModels.Manager_Subsystem
{
    public class TimeslotVM
    {
        public int TimeSlotID { get; set; }
        public System.TimeSpan StartingTime { get; set; }
        public System.TimeSpan EndingTime { get; set; }
    }
}