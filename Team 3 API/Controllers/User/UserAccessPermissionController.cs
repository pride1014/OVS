using Microsoft.Exchange.WebServices.Data;
using OVS_Team_3_API.Models;
using OVS_Team_3_API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OVS_Team_3_API.Controllers
{
    [RoutePrefix("api/UserAccess")]
    public class UserAccessPermissionController : ApiController
    {
        OVSEntities5 db = new OVSEntities5();
        // GET: UserAccess
        [Route("GetUserAccess")]
        [HttpGet]
        public List<UserAccessPermissionVM> GetUserAccess()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.User_Access_Permission.Select(zz => new UserAccessPermissionVM
            {
                UserAccessPermissionID = zz.User_Access_Permission_ID,
                UserRoleName = zz.User_Role_Name,
                UserRoleDescription = zz.User_Role_Description,

            }).ToList();
        }

        // Get UserAccesys by ID

        [System.Web.Http.Route("getUserAccessByID/{id:int}")]
        [System.Web.Mvc.HttpPost]
        [HttpPost]
        public object getUserAccess(int id)

        {

            db.Configuration.ProxyCreationEnabled = false;

            User_Access_Permission user = db.User_Access_Permission.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;

        }

        //Add: UserAccess
        [Route("CreateUserAccess")]
        [HttpPost]
        public ResponseObject CreateUserAccess([FromBody] UserAccessPermissionVM userAccess)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();
            var NewuserAccess = new User_Access_Permission
            {
                User_Role_Name = userAccess.UserRoleName,
                User_Role_Description = userAccess.UserRoleDescription

            };

            try
            {
                db.User_Access_Permission.Add(NewuserAccess);
                db.SaveChanges();

                response.Success = true;
                response.ErrorMessage = null;
                return response;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.ErrorMessage = e.Message;
                return response;
            }
        }

        // Update UserAccess

        [Route("UpdateUserAcess")]
        [HttpPut]
        public ResponseObject UpdateUserAccess([FromBody] UserAccessPermissionVM useraccess)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();

            var toUpdate = db.User_Access_Permission.Where(zz => zz.User_Access_Permission_ID 
            == useraccess.UserAccessPermissionID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "The user access that you are trying to update was not found in the system.";
                return response;
            }

            try
            {
                toUpdate.User_Role_Name = useraccess.UserRoleName;
                toUpdate.User_Role_Description = useraccess.UserRoleDescription;

                db.SaveChanges();

                response.Success = true;
                response.ErrorMessage = null;
                return response;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.ErrorMessage = e.Message;
                return response;
            }
        }

        //Delete User
        [System.Web.Http.Route("DeleteUserAccess/{id:int}")]
        [System.Web.Mvc.HttpDelete]
        [HttpDelete]
        public object DeleteUserAccess(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            User_Access_Permission bur = db.User_Access_Permission.Find(id);
            if (bur == null)
            {
                return NotFound();
            }
            db.User_Access_Permission.Remove(bur);
            db.SaveChanges();

            return "The user access permission has been deleted";

        }
    }
}



