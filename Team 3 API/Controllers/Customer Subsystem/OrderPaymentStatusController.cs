using OVS_Team_3_API.Models;
using OVS_Team_3_API.ViewModels;
using OVS_Team_3_API.ViewModels.Customer_Subsystem;
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

namespace OVS_Team_3_API.Controllers.Customer_Subsystem
{
    [RoutePrefix("api/OrderPaymentStatus")]
    public class OrderPaymentStatusController : ApiController
    {

        //[RoutePrefix("api/OrderPaymentStatus")]

        OVSEntities5 db = new OVSEntities5();
        // GET: OrderPaymentStatus
        [Route("GetOrderPaymentStatus")]
        [HttpGet]
        public List<OrderPaymentStatusVM> GetOrderPaymentStatus()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Order_Payment_Status.Select(zz => new OrderPaymentStatusVM
            {
                OrderPaymentStatusID = zz.Order_Payment_Status_ID,
                OrderPaymentStatusDescription = zz.Order_Payment_Status_Description

            }).ToList();
        }

        // Get OrderPaymentStatus by ID

        [System.Web.Http.Route("getOrderPaymentStatusByID/{id:int}")]
        [System.Web.Mvc.HttpPost]
        [HttpPost]
        public object getOrderPaymentStatus(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Models.Order_Payment_Status OrderPayType = db.Order_Payment_Status.Find(id);
            if (OrderPayType == null)
            {
                return NotFound();
            }
            return OrderPayType;

        }

        //Add: OrderPaymentStatus
        [Route("CreateOrderPaymentStatus")]
        [HttpPost]
        public ResponseObject CreateOrderPaymentStatus([FromBody] OrderPaymentStatusVM OrderPayType)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();
            var NewOrderPayType = new Models.Order_Payment_Status
            {
                Order_Payment_Status_ID = OrderPayType.OrderPaymentStatusID,
                Order_Payment_Status_Description = OrderPayType.OrderPaymentStatusDescription
            };

            try
            {
                db.Order_Payment_Status.Add(NewOrderPayType);
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

        // Update OrderPaymentStatus

        [Route("UpdateOrderPaymentStatus")]
        [HttpPut]
        public ResponseObject UpdateOrderPaymentStatus([FromBody] OrderPaymentStatusVM PayType)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();

            var toUpdate = db.Order_Payment_Status.Where(zz => zz.Order_Payment_Status_ID == PayType.OrderPaymentStatusID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "Not found";
                return response;
            }

            try
            {
                toUpdate.Order_Payment_Status_Description = PayType.OrderPaymentStatusDescription;

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
        //Delete OrderPaymentStatus
        [System.Web.Http.Route("DeleteOrderPaymentStatus/{id:int}")]
        [System.Web.Mvc.HttpDelete]
        [HttpDelete]
        public object DeleteOrderPaymentStatus(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Models.Order_Payment_Status OrderPayType = db.Order_Payment_Status.Find(id);
            if (OrderPayType == null)
            {
                return NotFound();
            }
            db.Order_Payment_Status.Remove(OrderPayType);
            db.SaveChanges();

            return "Deleted";

        }

    }


}