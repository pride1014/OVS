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
    [RoutePrefix("api/ReturnSaleRequestSaleLine")]
    public class ReturnSaleRequestSaleLineController : ApiController
    {

        //[RoutePrefix("api/ReturnSaleRequestSaleLine")]

        OVSEntities5 db = new OVSEntities5();
        // GET: ReturnSaleRequestSaleLine
        [Route("GetReturnSaleRequestSaleLine")]
        [HttpGet]
        public List<ReturnSaleRequestSaleLineVM> GetReturnSaleRequestSaleLine()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Return_Sale_Request_Sale_Line.Select(zz => new ReturnSaleRequestSaleLineVM
            {
                ReturnSaleRequestSaleLineID = zz.Return_Sale_Request_Sale_Line_ID,
                ReturnSaleReason = zz.Return_Sale_Reason,
                Quantity = zz.Quantity,
                SaleLineID = zz.Sale_Line_ID,
                ReturnSaleRequestID = zz.Return_Sale_Request_ID


            }).ToList();
        }

        // Get ReturnSaleRequestSaleLine by ID

        [System.Web.Http.Route("getReturnSaleRequestSaleLineByID/{id:int}")]
        [System.Web.Mvc.HttpPost]
        [HttpPost]
        public object getReturnSaleRequestSaleLine(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Models.Return_Sale_Request_Sale_Line ReturnSaleReq = db.Return_Sale_Request_Sale_Line.Find(id);
            if (ReturnSaleReq == null)
            {
                return NotFound();
            }
            return ReturnSaleReq;

        }

        //Add: ReturnSaleRequestSaleLine
        [Route("CreateReturnSaleRequestSaleLine")]
        [HttpPost]
        public ResponseObject CreateReturnSaleRequestSaleLine([FromBody] ReturnSaleRequestSaleLineVM ReturnSaleReq)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();
            var NewReturnSaleReq = new Models.Return_Sale_Request_Sale_Line
            {
                Return_Sale_Request_Sale_Line_ID = ReturnSaleReq.ReturnSaleRequestSaleLineID,
                Return_Sale_Reason = ReturnSaleReq.ReturnSaleReason,
                Quantity = ReturnSaleReq.Quantity,
                Sale_Line_ID = ReturnSaleReq.SaleLineID,
                Return_Sale_Request_ID = ReturnSaleReq.ReturnSaleRequestID
            };

            try
            {
                db.Return_Sale_Request_Sale_Line.Add(NewReturnSaleReq);
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

        // Update ReturnSaleRequestSaleLine

        [Route("UpdateReturnSaleRequestSaleLine")]
        [HttpPut]
        public ResponseObject UpdateReturnSaleRequestSaleLine([FromBody] ReturnSaleRequestSaleLineVM ReturnSaleReq)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();

            var toUpdate = db.Return_Sale_Request_Sale_Line.Where(zz => zz.Return_Sale_Request_Sale_Line_ID == ReturnSaleReq.ReturnSaleRequestSaleLineID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "Not found";
                return response;
            }

            try
            {
                toUpdate.Return_Sale_Reason = ReturnSaleReq.ReturnSaleReason;
                toUpdate.Quantity = ReturnSaleReq.Quantity;
                toUpdate.Sale_Line_ID = ReturnSaleReq.SaleLineID;
                toUpdate.Return_Sale_Request_ID = ReturnSaleReq.ReturnSaleRequestID;

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
        //Delete ReturnSaleRequestSaleLine
        [System.Web.Http.Route("DeleteReturnSaleRequestSaleLine/{id:int}")]
        [System.Web.Mvc.HttpDelete]
        [HttpDelete]
        public object DeleteReturnSaleRequestSaleLine(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Models.Return_Sale_Request_Sale_Line ReturnSaleReq = db.Return_Sale_Request_Sale_Line.Find(id);
            if (ReturnSaleReq == null)
            {
                return NotFound();
            }
            db.Return_Sale_Request_Sale_Line.Remove(ReturnSaleReq);
            db.SaveChanges();

            return "Deleted";

        }

    }


}