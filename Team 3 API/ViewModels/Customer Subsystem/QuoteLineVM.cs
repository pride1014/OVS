using OVS_Team_3_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OVS_Team_3_API.ViewModels.Customer_Subsystem
{
    public class QuoteLineVM
    {
        public int QuoteLineID { get; set; }
        public int Quantity { get; set; }
        public Nullable<int> ProductID { get; set; }
        public Nullable<int> RequestQuoteID { get; set; }

        public System.DateTime Date { get; set; }
        public int? QuoteStatusID { get; set; }

        public string QuoteStatusDescription { get; set; }

        public double PriceAmount { get; set; }

        public string ProductName { get; set; }
        public string ProductDescription { get; set; }

    }
}