using OVS_Team_3_API.Models;
using OVS_Team_3_API.ViewModels;
using OVS_Team_3_API.ViewModels.Employee_Subsystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OVS_Team_3_API.Controllers.Employee_Subsystem
{
        [RoutePrefix("api/EmployeeType")]
        public class EmployeeTypeController : ApiController
        {
            OVSEntities5 db = new OVSEntities5();
            // GET: EmployeeType
            [Route("GetEmployeeType")]
            [HttpGet]
            public List<EmployeeTypeVM> GetEmployeeType()
            {
                db.Configuration.ProxyCreationEnabled = false;
                return db.Employee_Type.Select(zz => new EmployeeTypeVM
                {
                    EmployeeTypeID = zz.Employee_Type_ID,
                    EmployeeTypeDescription = zz.Employee_Type_Description,


                }).ToList();
            }


            [Route("GetEmployeeTypeByID/{id:int}")]
            [HttpGet]
            public object GetEmployeeTypeByID(int id)
            {

                db.Configuration.ProxyCreationEnabled = false;

                Employee_Type employee_Type = db.Employee_Type.Find(id);
                if (employee_Type == null)
                {
                    return NotFound();
                }
                return employee_Type;

            }
            //Add: Employee Type
            [Route("CreateEmployeeType")]
            [HttpPost]
            public ResponseObject CreateEmployeeType([FromBody] EmployeeTypeVM empType)
            {
                db.Configuration.ProxyCreationEnabled = false;
                var response = new ResponseObject();
                var newEmpType = new Employee_Type
                {
                    Employee_Type_Description = empType.EmployeeTypeDescription,

                };

                try
                {
                    db.Employee_Type.Add(newEmpType);
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

            // Update EmployeeType

            [Route("EditEmployeeType")]
            [HttpPut]
            public ResponseObject EditEmployeeType([FromBody] EmployeeTypeVM employeeType)
            {
                db.Configuration.ProxyCreationEnabled = false;
                var response = new ResponseObject();

                var toUpdate = db.Employee_Type.Where(zz => zz.Employee_Type_ID
                == employeeType.EmployeeTypeID).FirstOrDefault();

                if (toUpdate == null)
                {
                    response.Success = false;
                    response.ErrorMessage = "The employee type that you are trying to edit was not found in the system.";
                    return response;
                }

                try
                {

                    toUpdate.Employee_Type_Description = employeeType.EmployeeTypeDescription;
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
            //Delete EmployeeType
            [Route("DeleteEmployeeType/{id:int}")]
            [HttpDelete]
            public object DeleteEmployeeType(int id)
            {
                db.Configuration.ProxyCreationEnabled = false;

                Employee_Type empType = db.Employee_Type.Find(id);
                if (empType == null)
                {
                    return NotFound();
                }
                db.Employee_Type.Remove(empType);
                db.SaveChanges();

                return "The employee type record has been deleted";

            }
        }
    
}
