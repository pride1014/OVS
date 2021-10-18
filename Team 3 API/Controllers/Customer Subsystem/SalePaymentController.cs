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
    [RoutePrefix("api/SalePayment")]
    public class SalePaymentController : ApiController
    {

        //[RoutePrefix("api/SalePayment")]

        OVSEntities5 db = new OVSEntities5();
        // GET: SalePayment
        [Route("GetSalePayment")]
        [HttpGet]
        public List<SalePaymentVM> GetSalePayment()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Sale_Payment.Select(zz => new SalePaymentVM
            {
                SalePaymentID = zz.Sale_Payment_ID,
                SalePaymentAmount = zz.Sale_Payment_Amount,
                SalePaymentDate = zz.Sale_Payment_Date,
                SaleID = zz.Sale_ID,
                SalePaymentStatusID = zz.Sale_Payment_Status_ID,
                PaymentTypeID = zz.Payment_Type_ID,
                RegisterID = zz.Register_ID

            }).ToList();
        }

        // Get SalePayment by ID

        [System.Web.Http.Route("getSalePaymentByID/{id:int}")]
        [System.Web.Mvc.HttpPost]
        [HttpPost]
        public object getSalePayment(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Models.Sale_Payment salepay = db.Sale_Payment.Find(id);
            if (salepay == null)
            {
                return NotFound();
            }
            return salepay;

        }

        //Add: SalePayment
        [Route("CreateSalePayment")]
        [HttpPost]
        public ResponseObject CreateSalePayment([FromBody] SalePaymentVM salepay)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();
            var NewSalepay = new Models.Sale_Payment
            {
                Sale_Payment_ID = salepay.SalePaymentID,
                Sale_Payment_Amount = salepay.SalePaymentAmount,
                Sale_Payment_Date = salepay.SalePaymentDate,
                Sale_ID = salepay.SaleID,
                Sale_Payment_Status_ID = salepay.SalePaymentStatusID,
                Payment_Type_ID = salepay.PaymentTypeID

            };

            try
            {
                db.Sale_Payment.Add(NewSalepay);
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

        // Update SalePayment

        [Route("UpdateSalePayment")]
        [HttpPut]
        public ResponseObject UpdateSalePayment([FromBody] SalePaymentVM salepay)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();

            var toUpdate = db.Sale_Payment.Where(zz => zz.Sale_Payment_ID == salepay.SalePaymentID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "Not found";
                return response;
            }

            try
            {
                toUpdate.Sale_Payment_Amount = salepay.SalePaymentAmount;
                toUpdate.Sale_Payment_Date = salepay.SalePaymentDate;
                toUpdate.Sale_ID = salepay.SaleID;
                toUpdate.Sale_Payment_Status_ID = salepay.SalePaymentStatusID;
                toUpdate.Payment_Type_ID = salepay.SalePaymentStatusID;
                toUpdate.Register_ID = salepay.RegisterID;

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
        //Delete SalePayment
        [System.Web.Http.Route("DeleteSalePayment/{id:int}")]
        [System.Web.Mvc.HttpDelete]
        [HttpDelete]
        public object DeleteSalePayment(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Models.Sale_Payment salepay = db.Sale_Payment.Find(id);
            if (salepay == null)
            {
                return NotFound();
            }
            db.Sale_Payment.Remove(salepay);
            db.SaveChanges();

            return "Deleted";

        }

    }


}