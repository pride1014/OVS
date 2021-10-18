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
    [RoutePrefix("api/Task")]

    public class TaskController : ApiController
    {
        OVSEntities5 db = new OVSEntities5();

        // GET: Task
        [Route("GetTask")]
        [HttpGet]
        public List<TaskVM> GetJob_Instance()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Tasks.Select(zz => new TaskVM
            {
                TaskID = zz.Task_ID,
                TaskDescription = zz.Task_Description

            }).ToList();
        }



        // Get Task by ID

        [System.Web.Http.Route("getTaskByID/{id:int}")]
        [System.Web.Mvc.HttpPost]
        [HttpPost]
        public object getTaskByID(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return NotFound();
            }
            return task;

        }


        //Add: Task
        [Route("CreateTask")]
        [HttpPost]
        public ResponseObject CreateTask([FromBody] TaskVM task)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();
            var Newtask = new Task
            {
                Task_ID = task.TaskID,
                Task_Description = task.TaskDescription
            };

            try
            {
                db.Tasks.Add(Newtask);
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



        // Update Task

        [Route("UpdateTask")]
        [HttpPut]
        public ResponseObject UpdateTask([FromBody] TaskVM task)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();

            var toUpdate = db.Tasks.Where(zz => zz.Task_ID
            == task.TaskID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "Not found";
                return response;
            }

            try
            {
                toUpdate.Task_Description = task.TaskDescription;

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

        //Delete Task
        [System.Web.Http.Route("DeleteTask/{id:int}")]
        [System.Web.Mvc.HttpDelete]
        [HttpDelete]
        public object DeleteTask(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return NotFound();
            }
            db.Tasks.Remove(task);
            db.SaveChanges();

            return "Task deleted";

        }


    }
}