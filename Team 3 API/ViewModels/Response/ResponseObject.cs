using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OVS_Team_3_API.ViewModels
{
    public class ResponseObject
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }

        public int? UserAccessPermissionID { get; set; }
    }
}