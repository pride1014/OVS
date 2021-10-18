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
    [RoutePrefix("api/Order")]
    public class OrderController : ApiController
    {
        OVSEntities5 db = new OVSEntities5();
        // GET: Order
        [Route("GetOrder")]
        [HttpGet]
        public List<OrderVM> GetOrders()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Orders.Select(zz => new OrderVM
            {
                OrderID = zz.Order_ID,
                OrderDate = zz.Order_Date,
                OrderFinalizastionDate = zz.Order_Finalizastion_Date,
                Delivery = zz.Delivery,
                CustomerID = zz.Customer_ID,
                OrderStatusID = zz.Order_Status_ID,
                EmployeeID = zz.Employee_ID,

            }).ToList();
        }

        // Get Order by ID

        [System.Web.Http.Route("getOrderByID/{id:int}")]
        [System.Web.Mvc.HttpPost]
        [HttpPost]
        public object getOrder(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Models.Order order = db.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }
            return order;

        }

        //Add: Order
        [Route("CreateOrderr")]
        [HttpPost]
        public ResponseObject CreateOrder([FromBody] OrderVM order)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();
            var Neworder = new Models.Order
            {
               
                Order_Date = order.OrderDate,
                Order_Finalizastion_Date = order.OrderFinalizastionDate,
                Delivery = order.Delivery,
                Customer_ID = order.CustomerID,
                Order_Status_ID = order.OrderStatusID,
                Employee_ID = order.EmployeeID
            };

            try
            {
                db.Orders.Add(Neworder);
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

        // Update Order

        [Route("UpdateOrder")]
        [HttpPut]
        public ResponseObject UpdateOrder([FromBody] OrderVM order)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();

            var toUpdate = db.Orders.Where(zz => zz.Order_ID
            == order.OrderID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "Not found";
                return response;
            }

            try
            {
                toUpdate.Order_Date = order.OrderDate;
                toUpdate.Order_Finalizastion_Date = order.OrderFinalizastionDate;
                toUpdate.Delivery = order.Delivery;
                toUpdate.Customer_ID = order.CustomerID;
                toUpdate.Order_Status_ID = order.OrderStatusID;
                toUpdate.Employee_ID = order.EmployeeID;


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
        //Delete Order
        [System.Web.Http.Route("DeleteOrder/{id:int}")]
        [System.Web.Mvc.HttpDelete]
        [HttpDelete]
        public object DeleteOrder(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Models.Order order = db.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }
            db.Orders.Remove(order);
            db.SaveChanges();

            return "Order deleted";

        }

    }
}
