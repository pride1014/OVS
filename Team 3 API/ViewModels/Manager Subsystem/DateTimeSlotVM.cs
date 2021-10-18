using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OVS_Team_3_API.ViewModels.Manager_Subsystem
{
    public class DateTimeSlotVM
    {
        public int DateTimeSlotID { get; set; }
        public Nullable<int> ShiftID { get; set; }
        public Nullable<int> TimeSlotID { get; set; }
        public Nullable<int> DateID { get; set; }
    }
}