using OVS_Team_3_API.Models;
using OVS_Team_3_API.ViewModels;
using OVS_Team_3_API.ViewModels.Customer_Subsystem;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    [RoutePrefix("api/QuoteLine")]
    public class QuoteLineController : ApiController
    {

        //[RoutePrefix("api/QuoteLine")]

        OVSEntities5 db = new OVSEntities5();
        // GET: QuoteLine
        [Route("GetQuoteLine")]
        [HttpGet]
        public List<QuoteLineVM> GetQuoteLine()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Quote_Line.Include(zz => zz.Request_Quote).Include(zz=>zz.Product_Size).Include(zz => zz.Product).Select(zz => new QuoteLineVM
            {
                QuoteLineID = zz.Quote_Line_ID,
                Quantity = zz.Quantity,
                ProductID = zz.Product_ID,
                RequestQuoteID = zz.Product_ID,
                Date = DateTime.Now.Date,
                QuoteStatusDescription =zz.Request_Quote.Quote_Status.Quote_Status_Description,
                ProductName=zz.Product.Product_Name,
                ProductDescription=zz.Product.Product_Description,
                
            }).ToList();
        }

        // Get QuoteLine by ID

        [System.Web.Http.Route("getQuoteLineByID/{id:int}")]
        [System.Web.Mvc.HttpPost]
        [HttpPost]
        public object getQuoteLine(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Models.Quote_Line quoteline = db.Quote_Line.Find(id);
            if (quoteline == null)
            {
                return NotFound();
            }
            return quoteline;

        }

        //Add: QuoteLine
        [Route("CreateQuoteLine")]
        [HttpPost]
        public ResponseObject CreateQuoteLine([FromBody] QuoteLineVM quoteline)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();
            var Newquoteline = new Models.Quote_Line
            {
                Quote_Line_ID = quoteline.QuoteLineID,
                Quantity = quoteline.Quantity,
                Product_ID = quoteline.ProductID,
                Request_Quote_ID = quoteline.RequestQuoteID
            };

            try
            {
                db.Quote_Line.Add(Newquoteline);
                db.SaveChanges();

                var newQuote = new Request_Quote
                {
                    Date = DateTime.Now.Date,
                    Quote_Status_ID = quoteline.QuoteStatusID
                };
                db.Request_Quote.Add(newQuote);
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

        // Update QuoteLine

        [Route("UpdateQuoteLine")]
        [HttpPut]
        public ResponseObject UpdateQuoteLine([FromBody] QuoteLineVM quoteline)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();

            var toUpdate = db.Quote_Line.Where(zz => zz.Quote_Line_ID == quoteline.QuoteLineID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "Not found";
                return response;
            }

            try
            {
                toUpdate.Quantity = quoteline.Quantity;
                toUpdate.Product_ID = quoteline.ProductID;
                toUpdate.Request_Quote_ID = quoteline.RequestQuoteID;

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
        //Delete QuoteLine
        [System.Web.Http.Route("DeleteQuoteLine/{id:int}")]
        [System.Web.Mvc.HttpDelete]
        [HttpDelete]
        public object DeleteQuoteLine(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Models.Quote_Line quoteline = db.Quote_Line.Find(id);
            if (quoteline == null)
            {
                return NotFound();
            }
            db.Quote_Line.Remove(quoteline);
            db.SaveChanges();

            return "Quotation Status deleted";

        }

    }


}