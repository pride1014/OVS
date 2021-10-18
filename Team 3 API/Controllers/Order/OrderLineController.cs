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
    [RoutePrefix("api/orderline")]
    public class OrderLineController : ApiController
    {
        OVSEntities5 db = new OVSEntities5();

        // GET: Order Line
        [Route("GetOrderLine")]
        [HttpGet]
        public List<OrderLineVM> GetOrderLine()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Order_Line.Select(zz => new OrderLineVM
            {
                OrderLineID = zz.Order_Line_ID,
                OrderID = zz.Order_ID,
                ProductID = zz.Product_ID,
                Quantity = zz.Quantity

            }).ToList();
        }
        
        // Get Order Line by ID

        [System.Web.Http.Route("getOrderLineByID/{id:int}")]
        [System.Web.Mvc.HttpPost]
        [HttpPost]
        public object getOrderLine(int id)

        {

            db.Configuration.ProxyCreationEnabled = false;

            Order_Line orderline = db.Order_Line.Find(id);
            if (orderline == null)
            {
                return NotFound();
            }
            return orderline;

        }


        //Add: OrderLine
        [Route("CreateCartLine")]
        [HttpPost]
        public ResponseObject CreateCartLine([FromBody] OrderLineVM orderLine)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();
            var neworderLine = new Order_Line
            {
                Product_ID = orderLine.ProductID,
                Order_ID = orderLine.OrderID,
                Quantity = orderLine.Quantity

            };

            try
            {
                db.Order_Line.Add(neworderLine);
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

        // Update Order line

        [Route("UpdateOrderLine")]
        [HttpPut]
        public ResponseObject UpdateOrderLine([FromBody] OrderLineVM orderLine)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();

            var toUpdate = db.Order_Line.Where(zz => zz.Order_Line_ID
            == orderLine.OrderLineID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "The OrderLine that you are trying to update was not found in the system.";
                return response;
            }

            try
            {
                toUpdate.Product_ID = orderLine.ProductID;
                toUpdate.Order_ID = orderLine.OrderID;
                toUpdate.Quantity = orderLine.Quantity;

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

        //Delete Order Line
        [System.Web.Http.Route("DeleteOrderLine/{id:int}")]
        [System.Web.Mvc.HttpDelete]
        [HttpDelete]
        public object DeleteOrderLine(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Order_Line line = db.Order_Line.Find(id);
            if (line == null)
            {
                return NotFound();
            }
            db.Order_Line.Remove(line);
            db.SaveChanges();

            return "The Order line has been deleted";

        }

    }
}
