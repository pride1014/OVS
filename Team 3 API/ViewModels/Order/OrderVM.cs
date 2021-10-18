using OVS_Team_3_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OVS_Team_3_API.ViewModels.Order
{
    public class OrderVM
    {
        public int OrderID { get; set; }
        public System.DateTime OrderDate { get; set; }
        public System.DateTime OrderFinalizastionDate { get; set; }
        public bool Delivery { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public Nullable<int> OrderStatusID { get; set; }
        public Nullable<int> EmployeeID { get; set; }

    }
}