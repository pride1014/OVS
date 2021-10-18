using OVS_Team_3_API.Models;
using OVS_Team_3_API.ViewModels;
using OVS_Team_3_API.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OVS_Team_3_API.Controllers.Customer_Subsystem
{
    [RoutePrefix("api/orderstatus")]
    public class OrderStatusController : ApiController
    {
        OVSEntities5 db = new OVSEntities5();

        // GET: Order Status 
        [Route("GetOrderStatus")]
        [HttpGet]
        public List<OrderStatusVM> GetOrderStatus()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Order_Status.Select(zz => new OrderStatusVM
            {
                OrderStatusID= zz.Order_Status_ID,
                OrderStatusDescription = zz.Order_Status_Description

            }).ToList();
        }


        // Get Order Status by ID

        [System.Web.Http.Route("getOrderStatusByID/{id:int}")]
        [System.Web.Mvc.HttpPost]
        [HttpPost]
        public object GetOrderStatusByID(int id)

        {

            db.Configuration.ProxyCreationEnabled = false;

            Order_Status user = db.Order_Status.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;

        }

        //Add: Order Status
        [Route("CreateOrderStatus")]
        [HttpPost]
        public ResponseObject CreateUserAccess([FromBody] OrderStatusVM orderStatus)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();
            var newOrderStatus = new Order_Status
            {
                Order_Status_Description = orderStatus.OrderStatusDescription
               

            };

            try
            {
                db.Order_Status.Add(newOrderStatus);
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


        // Update OrderStatus

        [Route("UpdateOrderStatus")]
        [HttpPut]
        public ResponseObject UpdateOrderStatus([FromBody] OrderStatusVM orderStatus)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();

            var toUpdate = db.Order_Status.Where(zz => zz.Order_Status_ID
            == orderStatus.OrderStatusID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "The order that you are trying to update was not found in the system.";
                return response;
            }

            try
            {
                toUpdate.Order_Status_Description = orderStatus.OrderStatusDescription;

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

        //Delete OrderStatus
        [System.Web.Http.Route("DeleteOrderStatus/{id:int}")]
        [System.Web.Mvc.HttpDelete]
        [HttpDelete]
        public object DeleteOrderStatus(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Order_Status order = db.Order_Status.Find(id);
            if (order == null)
            {
                return NotFound();
            }
            db.Order_Status.Remove(order);
            db.SaveChanges();

            return "The Order Status has been deleted";

        }
    }
}
