using OVS_Team_3_API.Models;
using OVS_Team_3_API.ViewModels;
using OVS_Team_3_API.ViewModels.Job_Subsystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using HttpDeleteAttribute = System.Web.Http.HttpDeleteAttribute;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using HttpPutAttribute = System.Web.Http.HttpPutAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;
using RoutePrefixAttribute = System.Web.Http.RoutePrefixAttribute;

namespace OVS_Team_3_API.Controllers.Job_Subsystem
{
    [RoutePrefix("api/JobStatus")]
    public class Job_StatusController : ApiController
    {
        OVSEntities5 db = new OVSEntities5();

        // GET: Job_Status
        [Route("GetJobStatus")]
        [HttpGet]
        public List<Job_StatusVM> GetJob_Status()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Job_Status.Select(zz => new Job_StatusVM
            {
                JobStatusID = zz.Job_Status_ID,
                JobStatusDescription = zz.Job_Status_Description

            }).ToList();
        }


        // Get Job_Status by ID
        [System.Web.Http.Route("getJob_StatusByID/{id:int}")]
        [System.Web.Mvc.HttpPost]
        [HttpPost]
        public object getJob_Status(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Job_Status jobstat = db.Job_Status.Find(id);
            if (jobstat == null)
            {
                return NotFound();
            }
            return jobstat;

        }


        //Add: Job_Status
        [Route("CreateJob_Status")]
        [HttpPost]
        public ResponseObject CreateJob_Status([FromBody] Job_StatusVM jobstat)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();
            var Newjobstat = new Job_Status
            {
                Job_Status_ID = jobstat.JobStatusID,
                Job_Status_Description = jobstat.JobStatusDescription
            };

            try
            {
                db.Job_Status.Add(Newjobstat);
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


        // Update Job_Status

        [Route("UpdateJob_Status")]
        [HttpPut]
        public ResponseObject UpdateJob_Status([FromBody] Job_StatusVM jobstat)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();

            var toUpdate = db.Job_Status.Where(zz => zz.Job_Status_ID
            == jobstat.JobStatusID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "Not found";
                return response;
            }

            try
            {
                toUpdate.Job_Status_Description = jobstat.JobStatusDescription;

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



        //Delete Job_Status
        [System.Web.Http.Route("DeleteJob_Status/{id:int}")]
        [System.Web.Mvc.HttpDelete]
        [HttpDelete]
        public object DeleteJob_Status(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Job_Status jobstat = db.Job_Status.Find(id);
            if (jobstat == null)
            {
                return NotFound();
            }
            db.Job_Status.Remove(jobstat);
            db.SaveChanges();

            return "Job status deleted";

        }







    }






}