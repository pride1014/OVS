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
    [RoutePrefix("api/ShiftType")]
    public class ShiftTypeController : ApiController
    {
        OVSEntities5 db = new OVSEntities5();
        // GET: ShiftType
        [Route("GetShiftType")]
        [HttpGet]
        public List<ShiftTypeVM> GetShiftType()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Shift_Type.Select(zz => new ShiftTypeVM
            {
                ShiftTypeID = zz.Shift_Type_ID,
                ShiftTypeDescription = zz.Shift_Type_Description,
            }).ToList();
        }


        [Route("GetShiftTypeByID/{id:int}")]
        [HttpPost]
        public object GetShiftTypeByID(int id)
        {

            db.Configuration.ProxyCreationEnabled = false;

            Shift_Type shift_Type = db.Shift_Type.Find(id);
            if (shift_Type == null)
            {
                return NotFound();
            }
            return shift_Type;

        }
        //Add: SHIFT Type
        [Route("CreateShiftType")]
        [HttpPost]
        public ResponseObject CreateShiftType([FromBody] ShiftTypeVM shiftType)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();
            var newShiftType = new Shift_Type
            {
                Shift_Type_Description = shiftType.ShiftTypeDescription,

            };

            try
            {
                db.Shift_Type.Add(newShiftType);
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

        // Update ShiftType

        [Route("UpdateShiftType")]
        [HttpPut]
        public ResponseObject UpdateShiftType([FromBody] ShiftTypeVM shiftType)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();

            var toUpdate = db.Shift_Type.Where(zz => zz.Shift_Type_ID
            == shiftType.ShiftTypeID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "The shift  type that you are trying to edit was not found in the system.";
                return response;
            }

            try
            {
                toUpdate.Shift_Type_Description = shiftType.ShiftTypeDescription;
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
        [Route("DeleteShiftType/{id:int}")]
        [HttpDelete]
        public object DeleteShiftType(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Shift_Type shiftType = db.Shift_Type.Find(id);
            if (shiftType == null)
            {
                return NotFound();
            }
            db.Shift_Type.Remove(shiftType);
            db.SaveChanges();

            return "The shift type record has been deleted";

        }
    }
}
