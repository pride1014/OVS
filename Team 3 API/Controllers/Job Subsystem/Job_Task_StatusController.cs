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
    [RoutePrefix("api/Job_Task_Status")]
    public class Job_Task_StatusController : ApiController
    {
        OVSEntities5 db = new OVSEntities5();

        // GET: Job_Task_Status
        [Route("GetJob_Task_Status")]
        [HttpGet]
        public List<Job_Task_StatusVM> GetJob_Task_Status()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Job_Task_Status.Select(zz => new Job_Task_StatusVM
            {
                JobTaskStatusID = zz.Job_Task_Status_ID,
                JobTaskDescriptionStatus = zz.Job_Task_Description_Status

            }).ToList();
        }



        // Get Job_Task_Status by ID

        [System.Web.Http.Route("getJob_Task_StatusByID/{id:int}")]
        [System.Web.Mvc.HttpPost]
        [HttpPost]
        public object getJob_Task_Status(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Job_Task_Status jobtaskstatus = db.Job_Task_Status.Find(id);
            if (jobtaskstatus == null)
            {
                return NotFound();
            }
            return jobtaskstatus;

        }



        //Add: Job_Task_Status
        [Route("CreateJob_Task_Status")]
        [HttpPost]
        public ResponseObject CreateJob_Task_Status([FromBody] Job_Task_StatusVM jobtaskstatus)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();
            var Newjobtaskstatus = new Job_Task_Status
            {
                Job_Task_Status_ID = jobtaskstatus.JobTaskStatusID,
                Job_Task_Description_Status = jobtaskstatus.JobTaskDescriptionStatus
            };

            try
            {
                db.Job_Task_Status.Add(Newjobtaskstatus);
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



        // Update Job_Task_Status

        [Route("UpdateJob_Task_Status")]
        [HttpPut]
        public ResponseObject UpdateJob_Task_Status([FromBody] Job_Task_StatusVM jobtaskstatus)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();

            var toUpdate = db.Job_Task_Status.Where(zz => zz.Job_Task_Status_ID
            == jobtaskstatus.JobTaskStatusID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "Not found";
                return response;
            }

            try
            {
                toUpdate.Job_Task_Description_Status = jobtaskstatus.JobTaskDescriptionStatus;

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




        //Delete Job_Task_Status

        [System.Web.Http.Route("DeleteJob_Task_Status/{id:int}")]
        [System.Web.Mvc.HttpDelete]
        [HttpDelete]
        public object DeleteJob_Task_Status(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Job_Task_Status jobtaskstatus = db.Job_Task_Status.Find(id);
            if (jobtaskstatus == null)
            {
                return NotFound();
            }
            db.Job_Task_Status.Remove(jobtaskstatus);
            db.SaveChanges();

            return "Job task status deleted";

        }



    }


}