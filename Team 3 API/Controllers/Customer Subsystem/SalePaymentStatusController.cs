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
    [RoutePrefix("api/SalePaymentStatus")]
    public class SalePaymentStatustController : ApiController
    {

        //[RoutePrefix("api/SalePaymentStatus")]

        OVSEntities5 db = new OVSEntities5();
        // GET: SalePaymentStatus
        [Route("GetSalePaymentStatus")]
        [HttpGet]
        public List<SalePaymentStatusVM> GetSalePaymentStatus()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Sale_Payment_Status.Select(zz => new SalePaymentStatusVM
            {
                SalePaymentStatusID = zz.Sale_Payment_Status_ID,
                SalePaymentStatusDesc = zz.Sale_Payment_Status_Desc

            }).ToList();
        }

        // Get SalePaymentStatus by ID

        [System.Web.Http.Route("GetSalePaymentStatusByID/{id:int}")]
        [System.Web.Mvc.HttpPost]
        [HttpPost]
        public object getSalePaymentStatus(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Models.Sale_Payment_Status salepaystatus = db.Sale_Payment_Status.Find(id);
            if (salepaystatus == null)
            {
                return NotFound();
            }
            return salepaystatus;

        }

        //Add: SalePayment
        [Route("CreateSalePayment")]
        [HttpPost]
        public ResponseObject CreateSalePayment([FromBody] SalePaymentStatusVM salepaystatus)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();
            var NewSalePayStatus = new Models.Sale_Payment_Status
            {
                Sale_Payment_Status_ID = salepaystatus.SalePaymentStatusID,
                Sale_Payment_Status_Desc = salepaystatus.SalePaymentStatusDesc
            };

            try
            {
                db.Sale_Payment_Status.Add(NewSalePayStatus);
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

        // Update SalePaymentStatus

        [Route("UpdateSalePaymentStatus")]
        [HttpPut]
        public ResponseObject UpdateSalePaymentStatus([FromBody] SalePaymentStatusVM salepaystatus)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();

            var toUpdate = db.Sale_Payment_Status.Where(zz => zz.Sale_Payment_Status_ID == salepaystatus.SalePaymentStatusID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "Not found";
                return response;
            }

            try
            {
                toUpdate.Sale_Payment_Status_Desc = salepaystatus.SalePaymentStatusDesc;

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
        //Delete SalePaymentStatus
        [System.Web.Http.Route("DeleteSalePaymentStatus/{id:int}")]
        [System.Web.Mvc.HttpDelete]
        [HttpDelete]
        public object DeleteSalePaymentStatus(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Models.Sale_Payment_Status salepaystatus = db.Sale_Payment_Status.Find(id);
            if (salepaystatus == null)
            {
                return NotFound();
            }
            db.Sale_Payment_Status.Remove(salepaystatus);
            db.SaveChanges();

            return "Deleted";

        }

    }


}