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
    [RoutePrefix("api/Shift")]
    public class ShiftController : ApiController
    {
        OVSEntities5 db = new OVSEntities5();
        // GET: Shift
        [Route("GetShift")]
        [HttpGet]
        public List<ShiftVM> GetEmployeeType()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Shifts.Select(zz => new ShiftVM
            {
                ShiftTypeID = zz.Shift_Type_ID,
                ShiftID = zz.Shift_ID,
            }).ToList();
        }


        [Route("GetShiftByID/{id:int}")]
        [HttpPost]
        public object GetShiftByID(int id)
        {

            db.Configuration.ProxyCreationEnabled = false;

            Shift shift = db.Shifts.Find(id);
            if (shift == null)
            {
                return NotFound();
            }
            return shift;

        }
        //Add: SHIFT 
        [Route("CreateShift")]
        [HttpPost]
        public ResponseObject CreateShift([FromBody] ShiftVM shift)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();
            var newShift = new Shift
            {
                Shift_Type_ID = shift.ShiftTypeID,
            };

            try
            {
                db.Shifts.Add(newShift);
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

        // Update Shift

        [Route("UpdateShift")]
        [HttpPut]
        public ResponseObject UpdateShift([FromBody] ShiftVM shift)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();

            var toUpdate = db.Shifts.Where(zz => zz.Shift_ID
            == shift.ShiftID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "The shift that you are trying to edit was not found in the system.";
                return response;
            }

            try
            {
                toUpdate.Shift_Type_ID = shift.ShiftTypeID;
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
        //DeleteShift
        [Route("DeleteShift/{id:int}")]
        [HttpDelete]
        public object DeleteShift(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Shift shift = db.Shifts.Find(id);
            if (shift == null)
            {
                return NotFound();
            }
            db.Shifts.Remove(shift);
            db.SaveChanges();

            return "The shift record has been deleted";

        }
    }
}
