using OVS_Team_3_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OVS_Team_3_API.ViewModels.Customer_Subsystem
{
    public class SalePaymentVM
    {
        public int SalePaymentID { get; set; }
        public double SalePaymentAmount { get; set; }
        public System.DateTime SalePaymentDate { get; set; }
        public Nullable<int> SalePaymentStatusID { get; set; }
        public Nullable<int> SaleID { get; set; }
        public Nullable<int> PaymentTypeID { get; set; }
        public Nullable<int> RegisterID { get; set; }
    }
}