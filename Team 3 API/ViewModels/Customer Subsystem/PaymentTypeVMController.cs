using OVS_Team_3_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OVS_Team_3_API.ViewModels.Customer_Subsystem
{
    public class PaymentTypeVM
    {
        public int PaymentTypeID { get; set; }
        public string PaymentTypeDescription { get; set; }
        public string PaymentTypeName { get; set; }
    }
}