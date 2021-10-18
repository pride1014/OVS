using OVS_Team_3_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OVS_Team_3_API.ViewModels.Customer_Subsystem
{
    public class OrderPaymentVM
    {
        // GET: OrderPaymentVM
        public int OrderPaymentID { get; set; }
        public int OrderPaymentAmount { get; set; }
        public System.DateTime OrderPaymentDate { get; set; }
        public Nullable<int> PaymentTypeID { get; set; }
        public Nullable<int> OrderPaymentStatusID { get; set; }
        public Nullable<int> OrderID { get; set; }
    }
}