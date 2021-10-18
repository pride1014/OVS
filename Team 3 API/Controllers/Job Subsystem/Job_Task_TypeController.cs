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
    [RoutePrefix("api/Job_Task_Type")]
    public class Job_Task_TypeController : ApiController
    {
        OVSEntities5 db = new OVSEntities5();

        // GET: Job_Task_Type
        [Route("GetJob_Task_Type")]
        [HttpGet]
        public List<Job_Task_TypeVM> GetJob_Task_Type()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Job_Task_Type.Select(zz => new Job_Task_TypeVM
            {
                JobTaskTypeID = zz.Job_Task_Type_ID,
                JobTaskTypeDescription = zz.Job_Task_Type_Description

            }).ToList();
        }


        // Get Job_Instance by ID

        [System.Web.Http.Route("getJob_Task_TypeByID/{id:int}")]
        [System.Web.Mvc.HttpPost]
        [HttpPost]
        public object getJob_Task_Type(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Job_Task_Type jobtasktype = db.Job_Task_Type.Find(id);
            if (jobtasktype == null)
            {
                return NotFound();
            }
            return jobtasktype;

        }



        //Add: Job_Task_Type
        [Route("CreateJob_Task_Type")]
        [HttpPost]
        public ResponseObject CreateJob_Task_Type([FromBody] Job_Task_TypeVM jobtasktype)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();
            var Newjobtasktype = new Job_Task_Type
            {
                Job_Task_Type_ID = jobtasktype.JobTaskTypeID,
                Job_Task_Type_Description = jobtasktype.JobTaskTypeDescription
            };

            try
            {
                db.Job_Task_Type.Add(Newjobtasktype);
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



        // Update Job_Task_Type

        [Route("UpdateJob_Task_Type")]
        [HttpPut]
        public ResponseObject UpdateJob_Task_Type([FromBody] Job_Task_TypeVM jobtasktype)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();

            var toUpdate = db.Job_Task_Type.Where(zz => zz.Job_Task_Type_ID
            == jobtasktype.JobTaskTypeID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "Not found";
                return response;
            }

            try
            {
                toUpdate.Job_Task_Type_Description = jobtasktype.JobTaskTypeDescription;

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


        //Delete Job_Task_Type
        [System.Web.Http.Route("DeleteJob_Task_Type/{id:int}")]
        [System.Web.Mvc.HttpDelete]
        [HttpDelete]
        public object DeleteJob_Task_Type(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Job_Task_Type jobtasktype = db.Job_Task_Type.Find(id);
            if (jobtasktype == null)
            {
                return NotFound();
            }
            db.Job_Task_Type.Remove(jobtasktype);
            db.SaveChanges();

            return "Job Instance deleted";

        }


    }
}