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
    [RoutePrefix("api/Job")]
    public class JobController : ApiController
    {
        OVSEntities5 db = new OVSEntities5();

        // GET: Job
        [Route("GetJob")]
        [HttpGet]
        public List<JobVM> GetJob()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Jobs.Select(zz => new JobVM
            {
                JobID = zz.Job_ID,
                JobDescription = zz.Job_Description,
                JobStatusID = zz.Job_Status_ID,
                ProductID = zz.Product_ID

            }).ToList();
        }


        // Get Job by ID

        [System.Web.Http.Route("getJobByID/{id:int}")]
        [System.Web.Mvc.HttpPost]
        [HttpPost]
        public object getJob(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return NotFound();
            }
            return job;

        }





        //Add: Job
        [Route("CreateJob")]
        [HttpPost]
        public ResponseObject CreateJob([FromBody] JobVM job)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();
            var Newjob = new Job
            {
                Job_ID = job.JobID,
                Job_Description = job.JobDescription,
                Job_Status_ID = job.JobStatusID,
                Product_ID = job.ProductID
            };

            try
            {
                db.Jobs.Add(Newjob);
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



        // Update Job

        [Route("UpdateJob")]
        [HttpPut]
        public ResponseObject UpdateJob([FromBody] JobVM job)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();

            var toUpdate = db.Jobs.Where(zz => zz.Job_ID
            == job.JobID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "Not found";
                return response;
            }

            try
            {
                toUpdate.Job_Description = job.JobDescription;
                toUpdate.Job_Status_ID = job.JobStatusID;
                toUpdate.Product_ID = job.ProductID;

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



        //Delete Job
        [System.Web.Http.Route("DeleteJob/{id:int}")]
        [System.Web.Mvc.HttpDelete]
        [HttpDelete]
        public object DeleteJob(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return NotFound();
            }
            db.Jobs.Remove(job);
            db.SaveChanges();

            return "Job deleted";

        }
























    }
}