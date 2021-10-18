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
    [RoutePrefix("api/ReturnOrderLine")]
    public class ReturnOrderRequestOrderLineController : ApiController
    {
        OVSEntities5 db = new OVSEntities5();
        // GET: ReturnOrderOrderLine
        [Route("GetReturnOrderLine")]
        [HttpGet]
        public List<ReturnOrderRequestOrderLineVM> GetReturnOrderLine()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Return_Order_Request_Order_Line.Select(zz => new ReturnOrderRequestOrderLineVM
            {
                ReturnOrderRequestOrderLineID = zz.Return_Order_Request_Order_Line_ID,
                ReturnOrderRequestID = zz.Return_Order_Request_ID,
                ReturnOrderRequest=zz.Return_Order_Request,
                OrderLineID = zz.Order_Line_ID,
                OrderLine=zz.Order_Line,
                ReturnReason=zz.Return_Reason,
                Quantity=zz.Quantity,


            }).ToList();
        }


        // Get ReturnOrderOrderLine by ID

        [System.Web.Http.Route("getReturnOrderOrderLineByID/{id:int}")]
        [System.Web.Mvc.HttpPost]
        [HttpPost]
        public object getReturnOrderLine(int id)

        {

            db.Configuration.ProxyCreationEnabled = false;

            Return_Order_Request_Order_Line line = db.Return_Order_Request_Order_Line.Find(id);
            if (line == null)
            {
                return NotFound();
            }
            return line;

        }


        //Add: ReturnOrderOrderLine
        [Route("CreateReturnOrderLine")]
        [HttpPost]
        public ResponseObject CreateReturnOrderLine([FromBody] ReturnOrderRequestOrderLineVM line)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();
            var NewuReturnOrderOrderLine = new Return_Order_Request_Order_Line
            {
                Return_Order_Request_ID = line.ReturnOrderRequestID,
                Order_Line_ID = line.OrderLineID,
                Return_Reason=line.ReturnReason,
                Quantity=line.Quantity

            };

            try
            {
                db.Return_Order_Request_Order_Line.Add(NewuReturnOrderOrderLine);
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



        // Update ReturnOrderOrderLine

        [Route("UpdateReturnOrderLine")]
        [HttpPut]
        public ResponseObject UpdateReturnOrderLine([FromBody] ReturnOrderRequestOrderLineVM line)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();

            var toUpdate = db.Return_Order_Request_Order_Line.Where(zz => zz.Return_Order_Request_Order_Line_ID
            == line.ReturnOrderRequestOrderLineID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "The Return Order Line that you are trying to update was not found in the system.";
                return response;
            }

            try
            {
                toUpdate.Order_Line_ID = line.OrderLineID;
                toUpdate.Return_Order_Request_ID = line.ReturnOrderRequestID;
                toUpdate.Return_Reason = line.ReturnReason;
                toUpdate.Quantity = line.Quantity;

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


        //Delete ReturnOrderLine
        [System.Web.Http.Route("DeleteReturnOrderOrderLine/{id:int}")]
        [System.Web.Mvc.HttpDelete]
        [HttpDelete]
        public object DeleteReturnOrderLine(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Return_Order_Request_Order_Line line = db.Return_Order_Request_Order_Line.Find(id);
            if (line == null)
            {
                return NotFound();
            }
            db.Return_Order_Request_Order_Line.Remove(line);
            db.SaveChanges();

            return "The Return Order Line has been deleted";

        }
    }
}
