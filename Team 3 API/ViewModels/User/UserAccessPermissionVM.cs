using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OVS_Team_3_API.ViewModels
{
    public class UserAccessPermissionVM
    {
        public int UserAccessPermissionID { get; set; }
        public string UserRoleName { get; set; }
        public string UserRoleDescription { get; set; }
    }
}