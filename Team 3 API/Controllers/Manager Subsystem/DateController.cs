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
    [RoutePrefix("api/Date")]
    public class DateController : ApiController
    {
        OVSEntities5 db = new OVSEntities5();
        // GET: Date
        [Route("GetDate")]
        [HttpGet]
        public List<DateVM> GetDate()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Dates.Select(zz => new DateVM
            {
                DateID = zz.Date_ID,
                DateDescription = zz.Date_Description,
            }).ToList();
        }

        [Route("GetDateByID/{id:int}")]
        [HttpPost]
        public object GetDateByID(int id)
        {

            db.Configuration.ProxyCreationEnabled = false;

            Date CR = db.Dates.Find(id);
            if (CR == null)
            {
                return NotFound();
            }
            return CR;

        }
        //Add: Date
        [Route("AddDate")]
        [HttpPost]
        public ResponseObject AddDate([FromBody] DateVM CR)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();
            var dt = new Date
            {
                Date_Description = CR.DateDescription,

            };
            try
            {
                db.Dates.Add(dt);
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

        // UpdateDate

        [Route("UpdateDate")]
        [HttpPut]
        public ResponseObject UpdateDate([FromBody] DateVM CR)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();

            var toUpdate = db.Dates.Where(zz => zz.Date_ID
            == CR.DateID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "The date that you are trying to find was not found in the system.";
                return response;
            }

            try
            {
                toUpdate.Date_Description = CR.DateDescription;
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
        //DeleteDate
        [Route("DeleteDate/{id:int}")]
        [HttpDelete]
        public object DeleteDate(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Date date = db.Dates.Find(id);
            if (date == null)
            {
                return NotFound();
            }
            db.Dates.Remove(date);
            db.SaveChanges();

            return "The date record has been deleted";

        }

    }
}
