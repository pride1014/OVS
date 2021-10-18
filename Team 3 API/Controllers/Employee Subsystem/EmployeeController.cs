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
    [RoutePrefix("api/Employee")]
    public class EmployeeController : ApiController
    {
        OVSEntities5 db = new OVSEntities5();
        // GET: Employee
        [Route("GetEmployee")]
        [HttpGet]
        public List<EmployeeVM> GetEmployee()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Employees.Select(zz => new EmployeeVM
            {
                EmployeeID = zz.Employee_ID,
                EmployeeName = zz.Employee_Name,
                EmployeeSurname = zz.Employee_Surname,
                EmployeePhoneNumber = zz.Employee_Phone_Number,
                EmployeeEmailAddress = zz.Employee_Email_Address,
                EmployeeTypeID = zz.Employee_Type_ID,
                UserID = zz.User_ID,
            }).ToList();
        }

        [Route("GetEmployeeByID/{id:int}")]
        [HttpGet]
        public object GetEmployeeByID(int id)
        {

            db.Configuration.ProxyCreationEnabled = false;

            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            return employee;

        }
        //Add: Employee
        [Route("CreateEmployee")]
        [HttpPost]
        public ResponseObject CreateEmployee([FromBody] EmployeeVM emp)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();
            var newEmp = new Employee
            {
                Employee_Name = emp.EmployeeName,
                Employee_Surname = emp.EmployeeSurname,
                Employee_Phone_Number = emp.EmployeePhoneNumber,
                Employee_Email_Address = emp.EmployeeEmailAddress,
                Employee_Type_ID = emp.EmployeeTypeID,
                User_ID = emp.UserID,

            };
            try
            {
                db.Employees.Add(newEmp);
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

        // Update Employee

        [Route("UpdateEmployee")]
        [HttpPut]
        public ResponseObject UpdateEmployee([FromBody] EmployeeVM employee)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();

            var toUpdate = db.Employees.Where(zz => zz.Employee_ID
            == employee.EmployeeID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "The employee that you are trying to edit was not found in the system.";
                return response;
            }

            try
            {
                toUpdate.Employee_Name = employee.EmployeeName;
                toUpdate.Employee_Surname = employee.EmployeeSurname;
                toUpdate.Employee_Phone_Number = employee.EmployeePhoneNumber;
                toUpdate.Employee_Email_Address = employee.EmployeeEmailAddress;
                toUpdate.Employee_Type_ID = employee.EmployeeTypeID;
                toUpdate.User_ID = employee.UserID;
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
        //Delete Employee
        [Route("DeleteEmployee/{id:int}")]
        [HttpDelete]
        public object DeleteEmployee(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Employee emp = db.Employees.Find(id);
            if (emp == null)
            {
                return NotFound();
            }
            db.Employees.Remove(emp);
            db.SaveChanges();

            return "The employee record has been deleted";

        }

    }
}
