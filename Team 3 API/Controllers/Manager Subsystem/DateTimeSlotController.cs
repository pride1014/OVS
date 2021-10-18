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
    [RoutePrefix("api/DateTimeSlot")]
    public class DateTimeSlotController : ApiController
    {
        OVSEntities5 db = new OVSEntities5();

        // GET: DateTimeSlot
        [Route("GetDateTimeSlot")]
        [HttpGet]
        public List<DateTimeSlotVM> GetDateTimeSlot()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Date_Time_Slot.Select(zz => new DateTimeSlotVM
            {
                DateTimeSlotID = zz.Date_Time_Slot_ID,
                DateID = zz.Date_ID,
                TimeSlotID = zz.Time_Slot_ID,
                ShiftID = zz.Shift_ID

            }).ToList();
        }

        // Get DateTimeSlot by ID

        [System.Web.Http.Route("GetDateTimeSlotByID/{id:int}")]
        [System.Web.Mvc.HttpPost]
        [HttpPost]
        public object GetDateTimeSlotByID(int id)

        {

            db.Configuration.ProxyCreationEnabled = false;

            Date_Time_Slot date_Time_Slot = db.Date_Time_Slot.Find(id);
            if (date_Time_Slot == null)
            {
                return NotFound();
            }
            return date_Time_Slot;

        }


        //Add: DateTimeSlot
        [Route("CreateDateTimeSlot")]
        [HttpPost]
        public ResponseObject CreateDateTimeSlot([FromBody] DateTimeSlotVM date_Time_Slot)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();
            var newDTS = new Date_Time_Slot
            {
                Date_ID = date_Time_Slot.DateID,
                Shift_ID = date_Time_Slot.ShiftID,
                Time_Slot_ID = date_Time_Slot.TimeSlotID

            };

            try
            {
                db.Date_Time_Slot.Add(newDTS);
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

        // UpdateDateTimeSlot

        [Route("UpdateDateTimeSlot")]
        [HttpPut]
        public ResponseObject UpdateDateTimeSlot([FromBody] DateTimeSlotVM DTS)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();

            var toUpdate = db.Date_Time_Slot.Where(zz => zz.Date_Time_Slot_ID
            == DTS.DateTimeSlotID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "The shift slot that you are trying to update was not found in the system.";
                return response;
            }

            try
            {
                toUpdate.Date_ID = DTS.DateID;
                toUpdate.Time_Slot_ID = DTS.TimeSlotID;
                toUpdate.Shift_ID = DTS.ShiftID;

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

        //DeleteDateTimeSlot
        [System.Web.Http.Route("DeleteDateTimeSlot/{id:int}")]
        [System.Web.Mvc.HttpDelete]
        [HttpDelete]
        public object DeleteDateTimeSlot(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Date_Time_Slot slot = db.Date_Time_Slot.Find(id);
            if (slot == null)
            {
                return NotFound();
            }
            db.Date_Time_Slot.Remove(slot);
            db.SaveChanges();

            return "The shfit slot has been deleted";

        }

    }
}
