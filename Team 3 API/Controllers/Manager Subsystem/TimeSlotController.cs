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
    [RoutePrefix("api/TimeSlot")]
    public class TimeSlotController : ApiController
    {
        
        OVSEntities5 db = new OVSEntities5();
        // GET: TimeSlot
        [Route("GetTimeSlot")]
        [HttpGet]
        public List<TimeslotVM> GetTimeSlot()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Time_Slot.Select(zz => new TimeslotVM
            {
                TimeSlotID = zz.Time_Slot_ID,
                StartingTime = zz.Starting_time,
                EndingTime = zz.Ending_time
            }).ToList();
        }

        [Route("GetTimeSlotByID/{id:int}")]
        [HttpPost]
        public object GetTimeSlotByID(int id)
        {

            db.Configuration.ProxyCreationEnabled = false;

            Time_Slot ts = db.Time_Slot.Find(id);
            if (ts == null)
            {
                return NotFound();
            }
            return ts;

        }
        //Add: TimeSlot
        [Route("AddTimeSlot")]
        [HttpPost]
        public ResponseObject AddTimeSlot([FromBody] TimeslotVM TS)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();
            var newTime = new Time_Slot
            {

                Starting_time = TS.StartingTime,
                Ending_time = TS.EndingTime

            };
            try
            {
                db.Time_Slot.Add(newTime);
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

        // UpdateTimeSlot

        [Route("UpdateTimeSlot")]
        [HttpPut]
        public ResponseObject UpdateTimeSlot([FromBody] TimeslotVM CR)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();

            var toUpdate = db.Time_Slot.Where(zz => zz.Time_Slot_ID
            == CR.TimeSlotID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "The time slot that you are trying to find was not found in the system.";
                return response;
            }

            try
            {
                toUpdate.Starting_time = CR.StartingTime;
                toUpdate.Ending_time = CR.EndingTime;
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
        //DeleteTimeSlot
        [Route("DeleteTimeSlot/{id:int}")]
        [HttpDelete]
        public object DeleteTimeSlot(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Time_Slot ts = db.Time_Slot.Find(id);
            if (ts == null)
            {
                return NotFound();
            }
            db.Time_Slot.Remove(ts);
            db.SaveChanges();

            return "The time slot record has been deleted";

        }

    }
}
