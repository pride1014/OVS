using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OVS_Team_3_API.ViewModels
{
    public class CustomerVM
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public string CustomerCellphoneNumber { get; set; }
        public System.DateTime CustomerDateOfBirth { get; set; }
        public string CustomerEmailAddress { get; set; }
        public string CustomerPhysicalAddress { get; set; }
        public Nullable<int> CustomerTypeID { get; set; }
        public Nullable<int> UserID { get; set; }
        public string CustomerTypeDescription { get; set; }
    }
}