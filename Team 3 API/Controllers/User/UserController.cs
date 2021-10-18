using OVS_Team_3_API.Models;
using OVS_Team_3_API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Data.Entity;
using HttpDeleteAttribute = System.Web.Http.HttpDeleteAttribute;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using HttpPutAttribute = System.Web.Http.HttpPutAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;
using RoutePrefixAttribute = System.Web.Http.RoutePrefixAttribute;
using System.Web.Http.Cors;

namespace OVS_Team_3_API.Controllers
{
   // [EnableCors(origins:"http://localhost:44387", headers: "*", methods: "*")]
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        OVSEntities5 db = new OVSEntities5();
        // GET: User
        [Route("GetUser")]
        [HttpGet]
        public List<UserVM> GetUser()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Users.Include(x => x.User_Access_Permission).Select(zz => new UserVM
            {
                UserID = zz.User_ID,
                UserName = zz.User_Name,
                UserPassword = zz.User_Password,
                UserAccessPermissionID = zz.User_Access_Permission_ID,
                UserRoleName=zz.User_Access_Permission.User_Role_Name

            }).ToList();
        }

        // Get User by ID

        [System.Web.Http.Route("getUserByID/{id:int}")]
        [HttpGet]
        public object getUserByID(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;

        }

        //Add: User
        [Route("CreateUser")]
        [HttpPost]
        public ResponseObject CreateUser([FromBody] UserVM user)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();
           
            
            if (this.UserExists(user.UserName))
            {
                response.Success = false;
                return response;
            }




            var Newuser = new User
            {
                User_Name = user.UserName,
               User_Password = ComputeSha256Hash(user.UserPassword),
               User_Access_Permission_ID=user.UserAccessPermissionID
              //User_Password = user.User_Password

            };




            try
            {
                db.Users.Add(Newuser);
                db.SaveChanges();

                response.Success = true;
                response.ErrorMessage = null;
                return response;
               // return Ok(Newuser);
            }
            catch (Exception e)
            {
                response.Success = false;
                response.ErrorMessage = e.Message;
                return response;
            }
        }


        [HttpPost]
        [Route("Login")]
        public ResponseObject Login([FromBody] UserVM User)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();
          


            string hashedPassword = this.ComputeSha256Hash(User.UserPassword);
           var user = db.Users.Where(zz => zz.User_Name == User.UserName && zz.User_Password == hashedPassword).FirstOrDefault();
            var id = db.Users.Where(zz =>  zz.User_Access_Permission_ID == User.UserAccessPermissionID );


            if (user == null)
            {
                response.Success = false;
                response.ErrorMessage = "Not found";
                return response;
            }
            response.Success = true;
            response.UserAccessPermissionID = user.User_Access_Permission_ID;
            return (response);

            //return Ok(vm);
        }


        // Update User

        [Route("UpdateUser")]
        [HttpPut]
        public ResponseObject UpdateUser([FromBody] UserVM user)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();
            string hashedPassword = this.ComputeSha256Hash(user.UserPassword);
            var toUpdate = db.Users.Where(zz => zz.User_ID
            == user.UserID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "Not found";
                return response;
            }

            try
            {
                toUpdate.User_Name = user.UserName;
                toUpdate.User_Password = user.UserPassword;
                toUpdate.User_Access_Permission_ID = user.UserAccessPermissionID;

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
        [System.Web.Http.Route("DeleteUser/{id:int}")]
        [System.Web.Mvc.HttpDelete]
        [HttpDelete]
        public object DeleteUser(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            User users = db.Users.Find(id);
            if (users == null)
            {
                return NotFound();
            }
            db.Users.Remove(users);
            db.SaveChanges();

            return "deleted";

        }


        // Helper functions
        private bool UserExists(string username)
        {
           
            var user = db.Users.Where(zz => zz.User_Name == username).FirstOrDefault();

            return user != null;
        }


        private string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

    }
}