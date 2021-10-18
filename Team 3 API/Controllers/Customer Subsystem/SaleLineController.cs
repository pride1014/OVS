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
    [RoutePrefix("api/SaleLine")]
    public class SaleLineController : ApiController
    {

        //[RoutePrefix("api/SaleLine")]

        OVSEntities5 db = new OVSEntities5();
        // GET: SaleLine
        [Route("GetSaleLine")]
        [HttpGet]
        public List<SaleLineVM> GetSaleLine()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Sale_Line.Select(zz => new SaleLineVM
            {
                SaleLineID = zz.Sale_Line_ID,
                Quantity = zz.Quantity,
                ProductID = zz.Product_ID,
                SaleID = zz.Sale_ID


            }).ToList();
        }

        // Get SaleLine by ID

        [System.Web.Http.Route("getSaleLineByID/{id:int}")]
        [System.Web.Mvc.HttpPost]
        [HttpPost]
        public object getSaleLine(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Models.Sale_Line saleline = db.Sale_Line.Find(id);
            if (saleline == null)
            {
                return NotFound();
            }
            return saleline;

        }

        //Add: SaleLine
        [Route("CreateSaleLine")]
        [HttpPost]
        public ResponseObject CreateQuoteLine([FromBody] SaleLineVM saleline)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();
            var NewSaleline = new Models.Sale_Line
            {
                Sale_Line_ID = saleline.SaleLineID,
                Quantity = saleline.Quantity,
                Sale_ID = saleline.SaleID,
                Product_ID = saleline.ProductID
            };

            try
            {
                db.Sale_Line.Add(NewSaleline);
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

        // Update SaleLine

        [Route("UpdateSaleLine")]
        [HttpPut]
        public ResponseObject UpdateSaleLine([FromBody] SaleLineVM saleline)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();

            var toUpdate = db.Sale_Line.Where(zz => zz.Sale_Line_ID == saleline.SaleLineID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "Not found";
                return response;
            }

            try
            {
                toUpdate.Quantity = saleline.Quantity;
                toUpdate.Sale_ID = saleline.SaleID;
                toUpdate.Product_ID = saleline.ProductID;

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
        //Delete SaleLine
        [System.Web.Http.Route("DeleteSaleLine/{id:int}")]
        [System.Web.Mvc.HttpDelete]
        [HttpDelete]
        public object DeleteSaleLine(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Models.Sale_Line saleline = db.Sale_Line.Find(id);
            if (saleline == null)
            {
                return NotFound();
            }
            db.Sale_Line.Remove(saleline);
            db.SaveChanges();

            return "Sale Line deleted";

        }

    }


}