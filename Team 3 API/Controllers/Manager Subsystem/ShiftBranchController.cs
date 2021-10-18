using OVS_Team_3_API.Models;
using OVS_Team_3_API.ViewModels;
using OVS_Team_3_API.ViewModels.Employee_Subsystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OVS_Team_3_API.Controllers.Manager_Subsystem
{
    [RoutePrefix("api/ShiftBranch")]
    public class ShiftBranchController : ApiController
    {
        OVSEntities5 db = new OVSEntities5();

        // GET: ShiftBranch
        [Route("GetShiftBranch")]
        [HttpGet]
        public List<ShiftBranchVM> GetShiftBranch()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Shift_Branch.Select(zz => new ShiftBranchVM
            {
                ShiftBranchID = zz.Shift_Branch_ID,
                BranchID = zz.Branch_ID,
                ShiftID = zz.Shift_ID

            }).ToList();
        }

        // Get DateTimeSlot by ID

        [System.Web.Http.Route("GetShiftBranchByID/{id:int}")]
        [System.Web.Mvc.HttpPost]
        [HttpPost]
        public object GetShiftBranchByID(int id)

        {

            db.Configuration.ProxyCreationEnabled = false;

            Shift_Branch zz = db.Shift_Branch.Find(id);
            if (zz == null)
            {
                return NotFound();
            }
            return zz;

        }


        //Add: ShiftBranch
        [Route("CreateShiftBranch")]
        [HttpPost]
        public ResponseObject CreateShiftBranch([FromBody] ShiftBranchVM zz)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();
            var newSB = new Shift_Branch
            {
                Branch_ID = zz.BranchID,
                Shift_ID = zz.ShiftID,

            };

            try
            {
                db.Shift_Branch.Add(newSB);
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

        // UpdateShiftBranch

        [Route("UpdateShiftBranch")]
        [HttpPut]
        public ResponseObject UpdateShiftBranch([FromBody] ShiftBranchVM sb)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();

            var toUpdate = db.Shift_Branch.Where(zz => zz.Shift_Branch_ID
            == sb.ShiftBranchID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "The shift at the branch that you are trying to update was not found in the system.";
                return response;
            }

            try
            {
                toUpdate.Branch_ID = sb.BranchID;
                toUpdate.Shift_ID = sb.ShiftID;

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

        //DeleteShiftBranch
        [System.Web.Http.Route("DeleteShiftBranch/{id:int}")]
        [System.Web.Mvc.HttpDelete]
        [HttpDelete]
        public object DeleteShiftBranch(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Shift_Branch slot = db.Shift_Branch.Find(id);
            if (slot == null)
            {
                return NotFound();
            }
            db.Shift_Branch.Remove(slot);
            db.SaveChanges();

            return "The shfit at the branch has been deleted";

        }

    }
}
