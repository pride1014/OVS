using OVS_Team_3_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OVS_Team_3_API.ViewModels.Customer_Subsystem
{
    public class RequestQuoteVM
    {
        // GET: RequestOrderVM
        public int RequestQuoteID { get; set; }
        public System.DateTime Date { get; set; }
        public Nullable<int> QuoteStatusID { get; set; }

    }
}