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
    [RoutePrefix("api/Sale")]
    public class SaleController : ApiController
    {

        //[RoutePrefix("api/Sale")]

        OVSEntities5 db = new OVSEntities5();
        // GET: Sale
        [Route("GetSale")]
        [HttpGet]
        public List<SaleVM> GetSale()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Sales.Select(zz => new SaleVM
            {
                SaleID = zz.Sale_ID,
                SaleDate = zz.Sale_Date,
                RequestQuoteID = zz.Request_Quote_ID

            }).ToList();
        }

        // Get Sale by ID

        [System.Web.Http.Route("getSaleByID/{id:int}")]
        [System.Web.Mvc.HttpPost]
        [HttpPost]
        public object getSale(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Models.Sale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return NotFound();
            }
            return sale;

        }

        //Add: Sale
        [Route("CreateSale")]
        [HttpPost]
        public ResponseObject CreateSale([FromBody] SaleVM sale)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();
            var NewSale = new Models.Sale
            {
                Sale_ID = sale.SaleID,
                Sale_Date = sale.SaleDate,
                Request_Quote_ID = sale.RequestQuoteID
            };

            try
            {
                db.Sales.Add(NewSale);
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

        // Update Sale

        [Route("UpdateSale")]
        [HttpPut]
        public ResponseObject UpdateRequestQuote([FromBody] SaleVM sale)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();

            var toUpdate = db.Sales.Where(zz => zz.Sale_ID == sale.SaleID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "Not found";
                return response;
            }

            try
            {
                toUpdate.Sale_Date = sale.SaleDate;
                toUpdate.Request_Quote_ID = sale.RequestQuoteID;

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
        //Delete Sale
        [System.Web.Http.Route("DeleteSale/{id:int}")]
        [System.Web.Mvc.HttpDelete]
        [HttpDelete]
        public object DeleteSale(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Models.Sale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return NotFound();
            }
            db.Sales.Remove(sale);
            db.SaveChanges();

            return "Sale deleted";

        }

    }


}