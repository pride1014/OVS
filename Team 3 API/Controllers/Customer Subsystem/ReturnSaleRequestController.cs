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
    [RoutePrefix("api/ReturnSaleRequest")]
    public class ReturnSaleRequestController : ApiController
    {

        //[RoutePrefix("api/ReturnSaleRequest")]

        OVSEntities5 db = new OVSEntities5();
        // GET: ReturnSaleRequest
        [Route("GetReturnSaleRequest")]
        [HttpGet]
        public List<ReturnSaleRequestVM> GetReturnSaleRequest()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Return_Sale_Request.Select(zz => new ReturnSaleRequestVM
            {
                ReturnSaleRequestID = zz.Return_Sale_Request_ID,
                ReturnRequestDate = zz.Return_Request_Date,
                CustomerID = zz.Customer_ID


            }).ToList();
        }

        // Get ReturnSaleRequest by ID

        [System.Web.Http.Route("getReturnSaleRequestByID/{id:int}")]
        [System.Web.Mvc.HttpPost]
        [HttpPost]
        public object getReturnSaleRequest(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Models.Return_Sale_Request ReturnSaleReq = db.Return_Sale_Request.Find(id);
            if (ReturnSaleReq == null)
            {
                return NotFound();
            }
            return ReturnSaleReq;

        }

        //Add: ReturnSaleRequest
        [Route("CreateReturnSaleRequest")]
        [HttpPost]
        public ResponseObject CreateReturnSaleRequest([FromBody] ReturnSaleRequestVM ReturnSaleReq)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();
            var NewReturnSaleReq = new Models.Return_Sale_Request
            {
                Return_Sale_Request_ID = ReturnSaleReq.ReturnSaleRequestID,
                Return_Request_Date = ReturnSaleReq.ReturnRequestDate,
                Customer_ID = ReturnSaleReq.CustomerID
            };

            try
            {
                db.Return_Sale_Request.Add(NewReturnSaleReq);
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

        // Update ReturnSaleRequest

        [Route("UpdateReturnSaleRequest")]
        [HttpPut]
        public ResponseObject UpdateReturnSaleRequest([FromBody] ReturnSaleRequestVM ReturnSaleReq)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();

            var toUpdate = db.Return_Sale_Request.Where(zz => zz.Return_Sale_Request_ID == ReturnSaleReq.ReturnSaleRequestID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "Not found";
                return response;
            }

            try
            {
                toUpdate.Return_Sale_Request_ID = ReturnSaleReq.ReturnSaleRequestID;
                toUpdate.Return_Request_Date = ReturnSaleReq.ReturnRequestDate;
                toUpdate.Customer_ID = ReturnSaleReq.CustomerID;

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
        //Delete ReturnSaleRequest
        [System.Web.Http.Route("DeleteReturnSaleRequest/{id:int}")]
        [System.Web.Mvc.HttpDelete]
        [HttpDelete]
        public object DeleteReturnSaleRequest(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Models.Return_Sale_Request ReturnSaleReq = db.Return_Sale_Request.Find(id);
            if (ReturnSaleReq == null)
            {
                return NotFound();
            }
            db.Return_Sale_Request.Remove(ReturnSaleReq);
            db.SaveChanges();

            return "Deleted";

        }

    }


}