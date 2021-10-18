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
    [RoutePrefix("api/OrderPayment")]
    public class OrderPaymentController : ApiController
    {

        //[RoutePrefix("api/OrderPayment")]

        OVSEntities5 db = new OVSEntities5();
        // GET:OrderPayment
        [Route("GetOrderPayment")]
        [HttpGet]
        public List<OrderPaymentVM> GetOrderPayment()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Order_Payment.Select(zz => new OrderPaymentVM
            {
                OrderPaymentID = zz.Order_Payment_ID,
                OrderPaymentAmount = zz.Order_Payment_Amount,
                OrderPaymentDate = zz.Order_Payment_Date,
                PaymentTypeID = zz.Payment_Type_ID,
                OrderPaymentStatusID = zz.Order_Payment_Status_ID,
                OrderID = zz.Order_ID

            }).ToList();
        }

        // GetOrderPayment by ID

        [System.Web.Http.Route("getOrderPaymentByID/{id:int}")]
        [System.Web.Mvc.HttpPost]
        [HttpPost]
        public object getOrderPayment(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Models.Order_Payment OrderPay = db.Order_Payment.Find(id);
            if (OrderPay == null)
            {
                return NotFound();
            }
            return OrderPay;

        }

        //Add:OrderPayment
        [Route("CreateOrderPayment")]
        [HttpPost]
        public ResponseObject CreateOrderPayment([FromBody] OrderPaymentVM OrderPay)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();
            var NewOrderPay = new Models.Order_Payment
            {
                Order_Payment_ID = OrderPay.OrderPaymentID,
                Order_Payment_Amount = OrderPay.OrderPaymentAmount,
                Order_Payment_Date = OrderPay.OrderPaymentDate,
                Payment_Type_ID = OrderPay.PaymentTypeID,
                Order_Payment_Status_ID = OrderPay.OrderPaymentStatusID,
                Order_ID = OrderPay.OrderID
            };

            try
            {
                db.Order_Payment.Add(NewOrderPay);
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

        // UpdateOrderPayment

        [Route("UpdateOrderPayment")]
        [HttpPut]
        public ResponseObject UpdateOrderPayment([FromBody] OrderPaymentVM OrderPay)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();

            var toUpdate = db.Order_Payment.Where(zz => zz.Order_Payment_ID == OrderPay.OrderPaymentID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "Not found";
                return response;
            }

            try
            {
                toUpdate.Order_Payment_Amount = OrderPay.OrderPaymentAmount;
                toUpdate.Order_Payment_Date = OrderPay.OrderPaymentDate;
                toUpdate.Payment_Type_ID = OrderPay.PaymentTypeID;
                toUpdate.Order_Payment_Status_ID = OrderPay.OrderPaymentStatusID;
                toUpdate.Order_ID = OrderPay.OrderID;

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
        //DeleteOrderPayment
        [System.Web.Http.Route("DeleteOrderPayment/{id:int}")]
        [System.Web.Mvc.HttpDelete]
        [HttpDelete]
        public object DeleteOrderPayment(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Models.Order_Payment OrderPay = db.Order_Payment.Find(id);
            if (OrderPay == null)
            {
                return NotFound();
            }
            db.Order_Payment.Remove(OrderPay);
            db.SaveChanges();

            return "Deleted";

        }

    }


}