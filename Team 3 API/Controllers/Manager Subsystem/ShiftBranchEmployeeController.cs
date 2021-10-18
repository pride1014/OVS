using OVS_Team_3_API.Models;
using OVS_Team_3_API.ViewModels;
using OVS_Team_3_API.ViewModels.Manager_Subsystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OVS_Team_3_API.Controllers.Manager_Subsystem
{
    [RoutePrefix("api/ShiftBranchEmployee")]
    public class ShiftBranchEmployeeController : ApiController
    {
        OVSEntities5 db = new OVSEntities5();

        // GET: ShiftBranchEmployee
        [Route("GetShiftBranchEmployee")]
        [HttpGet]
        public List<ShiftBranchEmployeeVM> GetShiftBranch()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Shift_Branch_Employee.Select(zz => new ShiftBranchEmployeeVM
            {
                ShiftBranchID = zz.Shift_Branch_ID,
                EmployeeID = zz.Employee_ID

            }).ToList();
        }

        // Get ShiftBranchEmployee by ID

        [System.Web.Http.Route("GetShiftBranchEmployeeByID/{id:int}")]
        [System.Web.Mvc.HttpPost]
        [HttpPost]
        public object GetShiftBranchEmployeeByID(int id)

        {

            db.Configuration.ProxyCreationEnabled = false;

            Shift_Branch_Employee zz = db.Shift_Branch_Employee.Find(id);
            if (zz == null)
            {
                return NotFound();
            }
            return zz;

        }


        //Add: ShiftBranchEmployee
        [Route("CreateShiftBranchEmployee")]
        [HttpPost]
        public ResponseObject CreateShiftBranchEmployee([FromBody] ShiftBranchEmployeeVM zz)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();
            var newSBE = new Shift_Branch_Employee
            {
                Employee_ID = zz.EmployeeID,

            };

            try
            {
                db.Shift_Branch_Employee.Add(newSBE);
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

        // UpdateShiftBranchEmployee

        [Route("UpdateShiftBranchEmployee")]
        [HttpPut]
        public ResponseObject UpdateShiftBranchEmployee([FromBody] ShiftBranchEmployeeVM sb)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();

            var toUpdate = db.Shift_Branch_Employee.Where(zz => zz.Shift_Branch_Employee_ID
            == sb.ShiftBranchEmployeeID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "The shift at the branch that you are trying to update was not found in the system.";
                return response;
            }

            try
            {
                toUpdate.Employee_ID = sb.EmployeeID;

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

        //DeleteShiftBranchEmployee
        [System.Web.Http.Route("DeleteShiftBranchEmployee/{id:int}")]
        [System.Web.Mvc.HttpDelete]
        [HttpDelete]
        public object DeleteShiftBranchEmployee(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Shift_Branch_Employee slot = db.Shift_Branch_Employee.Find(id);
            if (slot == null)
            {
                return NotFound();
            }
            db.Shift_Branch_Employee.Remove(slot);
            db.SaveChanges();

            return "The shfit at the branch has been deleted";

        }

    }
}
