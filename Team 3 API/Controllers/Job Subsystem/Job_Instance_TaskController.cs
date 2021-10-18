using Microsoft.Exchange.WebServices.Data;
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
    [RoutePrefix("api/Job_Instance_Task")]
    public class Job_Instance_TaskController : ApiController
    {
        OVSEntities5 db = new OVSEntities5();

        // GET: Job_Instance_Task
        [Route("GetJob_Instance_Task")]
        [HttpGet]
        public List<Job_Instance_TaskVM> GetJob_Instance_Task()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Job_Instance_Task.Select(zz => new Job_Instance_TaskVM
            {
                JobInstanceTaskID = zz.Job_Instance_Task_ID,
                JobTaskID = zz.Job_task_ID,
                JobInstanceID = zz.Job_Instance_ID,
                StartDate = zz.Start_Date,
                EndDate = zz.End_Date

            }).ToList();
        }


        // Get Job_Instance_Task by ID

        [System.Web.Http.Route("getJob_Instance_TaskByID/{id:int}")]
        [System.Web.Mvc.HttpPost]
        [HttpPost]
        public object getJob_Instance_Task(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Job_Instance_Task jonbinsttask = db.Job_Instance_Task.Find(id);
            if (jonbinsttask == null)
            {
                return NotFound();
            }
            return jonbinsttask;

        }


        //Add: Job_Instance_Task
        [Route("CreateJob_Instance_Task")]
        [HttpPost]
        public ResponseObject CreateJob_Instance_Task([FromBody] Job_Instance_TaskVM jonbinsttask)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();
            var Newjonbinsttask = new Job_Instance_Task
            {
                Job_Instance_Task_ID = jonbinsttask.JobInstanceTaskID,
                Job_task_ID = jonbinsttask.JobTaskID,
                Job_Instance_ID = jonbinsttask.JobInstanceID,
                Start_Date = jonbinsttask.StartDate,
                End_Date = jonbinsttask.EndDate
            };

            try
            {
                db.Job_Instance_Task.Add(Newjonbinsttask);
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


        // Update Job_Instance_Task

        [Route("UpdateJob_Instance_Task")]
        [HttpPut]
        public ResponseObject UpdateJob_Instance_Task([FromBody] Job_Instance_TaskVM jonbinsttask)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();

            var toUpdate = db.Job_Instance_Task.Where(zz => zz.Job_Instance_Task_ID
            == jonbinsttask.JobInstanceTaskID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "Not found";
                return response;
            }

            try
            {
                toUpdate.Start_Date = jonbinsttask.StartDate;
                toUpdate.End_Date = jonbinsttask.EndDate;

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


        //Delete Job_Instance_Task
        [System.Web.Http.Route("DeleteJob_Instance_Task/{id:int}")]
        [System.Web.Mvc.HttpDelete]
        [HttpDelete]
        public object DeleteJob_Instance_Task(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Job_Instance_Task jonbinsttask = db.Job_Instance_Task.Find(id);
            if (jonbinsttask == null)
            {
                return NotFound();
            }
            db.Job_Instance_Task.Remove(jonbinsttask);
            db.SaveChanges();

            return "Job Instance Task deleted";

        }




    }
}