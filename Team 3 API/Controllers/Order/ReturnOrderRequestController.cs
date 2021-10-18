using OVS_Team_3_API.Models;
using OVS_Team_3_API.ViewModels;
using OVS_Team_3_API.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OVS_Team_3_API.Controllers.Order
{
    [RoutePrefix("api/returnorder")]
    public class ReturnOrderRequestController : ApiController
    {

        OVSEntities5 db = new OVSEntities5();
        // GET: ReturnOrder
        [Route("Getreturnorder")]
        [HttpGet]
        public List<ReturnOrderRequestVM> GetUserAccess()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Return_Order_Request.Select(zz => new ReturnOrderRequestVM
            {
                ReturnOrderRequest_ID = zz.Return_Order_Request_ID,
                RequestOrderDate = zz.Request_Order_Date,
                CustomerID = zz.Customer_ID,
                Customer=zz.Customer,
                OrderID=zz.Order_ID,
                Order=zz.Order,

            }).ToList();
        }


        // Get Return Order by ID

        [System.Web.Http.Route("getReturnOrderByID/{id:int}")]
        [System.Web.Mvc.HttpPost]
        [HttpPost]
        public object getReturnOrder(int id)

        {

            db.Configuration.ProxyCreationEnabled = false;

            Return_Order_Request req = db.Return_Order_Request.Find(id);
            if (req == null)
            {
                return NotFound();
            }
            return req;

        }



        //Add: Order Request
        [Route("CreateOrderReturn")]
        [HttpPost]
        public ResponseObject CreateOrderReturn([FromBody] ReturnOrderRequestVM request)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();
            var newRequest = new Return_Order_Request
            {
                Request_Order_Date = request.RequestOrderDate,
                Customer_ID = request.CustomerID,
                Customer=request.Customer,
                Order_ID=request.OrderID,
                Order=request.Order

            };

            try
            {
                db.Return_Order_Request.Add(newRequest);
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


        // Update OrderReturn

        [Route("UpdateOrderReturn")]
        [HttpPut]
        public ResponseObject UpdateOrderReturn([FromBody] ReturnOrderRequestVM OrderReturn)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();

            var toUpdate = db.Return_Order_Request.Where(zz => zz.Return_Order_Request_ID
            == OrderReturn.ReturnOrderRequest_ID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "The Order Return that you are trying to update was not found in the system.";
                return response;
            }

            try
            {
                toUpdate.Request_Order_Date = OrderReturn.RequestOrderDate;
                toUpdate.Customer_ID = OrderReturn.CustomerID;
                toUpdate.Order_ID = OrderReturn.OrderID;

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


        //Delete Order Return
        [System.Web.Http.Route("DeleteOrderReturn/{id:int}")]
        [System.Web.Mvc.HttpDelete]
        [HttpDelete]
        public object DeleteOrderReturn(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Return_Order_Request req = db.Return_Order_Request.Find(id);
            if (req == null)
            {
                return NotFound();
            }
            db.Return_Order_Request.Remove(req);
            db.SaveChanges();

            return "The Order Return permission has been deleted";

        }
    }
}
