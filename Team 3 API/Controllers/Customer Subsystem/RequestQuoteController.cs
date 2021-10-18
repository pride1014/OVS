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
    [RoutePrefix("api/RequestQuote")]
    public class RequestQuoteController : ApiController
    {

        //[RoutePrefix("api/RequestQuote")]

        private OVSEntities5 db = new OVSEntities5();
        // GET: RequestQuote
        [Route("GetRequestQuote")]
        [HttpGet]
        public List<RequestQuoteVM> GetRequestQuote()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Request_Quote.Select(zz => new RequestQuoteVM
            {
                RequestQuoteID = zz.Request_Quote_ID,
                Date = zz.Date,
                QuoteStatusID = zz.Quote_Status_ID

            }).ToList();
        }

        // Get RequestQuote by ID

        [System.Web.Http.Route("getRequestQuoteByID/{id:int}")]
        [System.Web.Mvc.HttpPost]
        [HttpPost]
        public object GetRequestQuote(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Models.Request_Quote quote = db.Request_Quote.Find(id);
            if (quote == null)
            {
                return NotFound();
            }
            return quote;

        }

        //Add: Order
        [Route("CreateRequestQuote")]
        [HttpPost]
        public ResponseObject CreateOrder([FromBody] RequestQuoteVM quote)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();
            var NewQuote = new Models.Request_Quote
            {
                Request_Quote_ID = quote.RequestQuoteID,
                Date = quote.Date,
                Quote_Status_ID = quote.QuoteStatusID
            };

            try
            {
                db.Request_Quote.Add(NewQuote);
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

        // Update RequestQuote

        [Route("UpdateRequestQuote")]
        [HttpPut]
        public ResponseObject UpdateRequestQuote([FromBody] RequestQuoteVM quote)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();

            var toUpdate = db.Request_Quote.Where(zz => zz.Request_Quote_ID
            == quote.RequestQuoteID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "Not found";
                return response;
            }

            try
            {
                toUpdate.Date = quote.Date;
                toUpdate.Quote_Status_ID = quote.QuoteStatusID;

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
        //Delete RequestQuote
        [System.Web.Http.Route("DeleteRequestQuote/{id:int}")]
        [System.Web.Mvc.HttpDelete]
        [HttpDelete]
        public object DeleteOrder(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Models.Request_Quote quote = db.Request_Quote.Find(id);
            if (quote == null)
            {
                return NotFound();
            }
            db.Request_Quote.Remove(quote);
            db.SaveChanges();

            return "Quotation deleted";

        }

    }


}