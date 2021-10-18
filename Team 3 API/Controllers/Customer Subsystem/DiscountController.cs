using OVS_Team_3_API.Models;
using OVS_Team_3_API.ViewModels;
using OVS_Team_3_API.ViewModels.Customer_Subsystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OVS_Team_3_API.Controllers.Customer_Subsystem
{
    [RoutePrefix("api/discount")]
    public class DiscountController : ApiController
    {
        OVSEntities5 db = new OVSEntities5();
        // GET: Discount
        [Route("GetDiscount")]
        [HttpGet]
        public List<DiscountVM> GetDiscount()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Discounts.Select(zz => new DiscountVM
            {
                DiscountID = zz.Discount_ID,
                DiscountName = zz.Discount_Name,
                DiscountDescription = zz.Discount_Description,
                DiscountPercentage=zz.Discount_Percentage

            }).ToList();
        }


        // Get Discount by ID

        [System.Web.Http.Route("getDiscountByID/{id:int}")]
        [System.Web.Mvc.HttpPost]
        [HttpPost]
        public object GetDiscountByID(int id)

        {

            db.Configuration.ProxyCreationEnabled = false;

            Discount discount = db.Discounts.Find(id);
            if (discount == null)
            {
                return NotFound();
            }
            return discount;

        }

        //Add: Discount
        [Route("CreateDiscount")]
        [HttpPost]
        public ResponseObject CreateDiscount([FromBody] DiscountVM discount)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();
            var newDiscount = new Discount
            {
                Discount_Name = discount.DiscountName,
                Discount_Description = discount.DiscountDescription,
                Discount_Percentage =discount.DiscountPercentage

            };

            try
            {
                db.Discounts.Add(newDiscount);
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


        // Update Discount

        [Route("updatediscount")]
        [HttpPut]
        public ResponseObject UpdateDiscount([FromBody] DiscountVM discount)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();

            var toUpdate = db.Discounts.Where(zz => zz.Discount_ID
            == discount.DiscountID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "The discount that you are trying to update was not found in the system.";
                return response;
            }

            try
            {
                toUpdate.Discount_Name = discount.DiscountName;
                toUpdate.Discount_Percentage = discount.DiscountPercentage;
                toUpdate.Discount_Description = discount.DiscountDescription;
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


        //Delete Discount
        [System.Web.Http.Route("DeleteDiscount/{id:int}")]
        [System.Web.Mvc.HttpDelete]
        [HttpDelete]
        public object DeleteDiscount(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Discount dis = db.Discounts.Find(id);
            if (dis == null)
            {
                return NotFound();
            }
            db.Discounts.Remove(dis);
            db.SaveChanges();

            return "The discount record has been deleted";

        }
    }
}
