using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OVS_Team_3_API.ViewModels
{
    public class UserVM
    {
        public int UserID { get; set; }
        public string UserPassword { get; set; }
        public string UserName { get; set; }
        public Nullable<int> UserAccessPermissionID { get; set; }

        public string UserRoleName { get; set; }
    }
}