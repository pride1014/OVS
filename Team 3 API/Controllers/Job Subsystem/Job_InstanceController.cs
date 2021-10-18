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
    [RoutePrefix("api/Job_Instance")]
    public class Job_InstanceController : ApiController
    {
        OVSEntities5 db = new OVSEntities5();

        // GET: Job_Instance
        [Route("GetJob_Instance")]
        [HttpGet]
        public List<Job_InstanceVM> GetJob_Instance()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Job_Instance.Select(zz => new Job_InstanceVM
            {
                JobInstanceID = zz.Job_Instance_ID,
                JobtaskID = zz.Job_task_ID,
                ShiftBranchEmployeeID = zz.Shift_Branch_Employee_ID,
                StartDate = zz.Start_Date,
                EndDate = zz.End_Date

            }).ToList();
        }

        // Get Job_Instance by ID

        [System.Web.Http.Route("getJob_InstanceByID/{id:int}")]
        [System.Web.Mvc.HttpPost]
        [HttpPost]
        public object getJob_Instance(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Job_Instance jonbinst = db.Job_Instance.Find(id);
            if (jonbinst == null)
            {
                return NotFound();
            }
            return jonbinst;

        }


        //Add: Job_Instance
        [Route("CreateJob_Instance")]
        [HttpPost]
        public ResponseObject CreateJob_Instance([FromBody] Job_InstanceVM jonbinst)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();
            var Newjonbinst = new Job_Instance
            {
                Job_Instance_ID = jonbinst.JobInstanceID,
                Job_task_ID = jonbinst.JobtaskID,
                Shift_Branch_Employee_ID = jonbinst.ShiftBranchEmployeeID,
                Start_Date = jonbinst.StartDate,
                End_Date = jonbinst.EndDate
            };

            try
            {
                db.Job_Instance.Add(Newjonbinst);
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

        // Update Job_Instance

        [Route("UpdateJob_Instance")]
        [HttpPut]
        public ResponseObject UpdateJob_Instance([FromBody] Job_InstanceVM jobinst)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();

            var toUpdate = db.Job_Instance.Where(zz => zz.Job_Instance_ID
            == jobinst.JobInstanceID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "Not found";
                return response;
            }

            try
            {

                toUpdate.Start_Date = jobinst.StartDate;
                toUpdate.End_Date = jobinst.EndDate;

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



        //Delete Job_Instance
        [System.Web.Http.Route("DeleteJob_Instance/{id:int}")]
        [System.Web.Mvc.HttpDelete]
        [HttpDelete]
        public object DeleteJob_Instance(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Job_Instance jobinst = db.Job_Instance.Find(id);
            if (jobinst == null)
            {
                return NotFound();
            }
            db.Job_Instance.Remove(jobinst);
            db.SaveChanges();

            return "Job Instance deleted";

        }





    }
}