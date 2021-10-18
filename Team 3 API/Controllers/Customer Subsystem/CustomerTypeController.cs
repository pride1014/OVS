using OVS_Team_3_API.Models;
using OVS_Team_3_API.ViewModels;
using OVS_Team_3_API.ViewModels.Customer_Subsystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

namespace OVS_Team_3_API.Controllers.Customer_Subsystem
{
    [RoutePrefix("api/CustomerType")]
    public class CustomerTypeController : ApiController
    {
        OVSEntities5 db = new OVSEntities5();
        // GET: Customer Type
        [Route("GetCustomerType")]
        [HttpGet]
        public List<CustomerTypeVM> GetCustomerType()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Customer_Type.Include(x => x.Discount).Select(zz => new CustomerTypeVM
            {
                CustomerTypeID= zz.Customer_Type_ID,
                CustomerTypeDescription = zz.Customer_Type_Description,
                DiscountID = zz.Discount_ID,
                DiscountName = zz.Discount.Discount_Name

            }).ToList();
        }


        // Get Customer Type by ID

        [System.Web.Http.Route("getCustomerTypeByID/{id:int}")]

        [HttpGet]
        public object GetCustomerTypeByID(int id)

        {

            db.Configuration.ProxyCreationEnabled = false;

            Customer_Type user = db.Customer_Type.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;

        }

        //Add: Customer Type
        [Route("CreateCustomerType")]
        [HttpPost]
        public ResponseObject CreateDiscount([FromBody] CustomerTypeVM customerType)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();
            var newCustomerType = new Customer_Type
            {
                Customer_Type_ID = customerType.CustomerTypeID,
                Customer_Type_Description = customerType.CustomerTypeDescription,
                Discount_ID = customerType.DiscountID

            };

            try
            {
                db.Customer_Type.Add(newCustomerType);
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


        // Update Customer Type 

        [Route("updatecustomertype")]
        [HttpPut]
        public ResponseObject UpdateCustomerType([FromBody] CustomerTypeVM customerType)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();

            var toUpdate = db.Customer_Type.Where(zz => zz.Customer_Type_ID
            == customerType.CustomerTypeID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "The customer type that you are trying to update was not found in the system.";
                return response;
            }

            try
            {
                toUpdate.Customer_Type_Description = customerType.CustomerTypeDescription;
                toUpdate.Discount_ID = customerType.DiscountID;
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

        //Delete Customer Type
        [System.Web.Http.Route("DeleteCustomerType/{id:int}")]
        [System.Web.Mvc.HttpDelete]
        [HttpDelete]
        public ResponseObject DeleteCustomerType(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ViewModels.ResponseObject();

            Customer_Type c_Type = db.Customer_Type.Find(id);
            if (c_Type == null)
            {
                response.Success = false;
                response.ErrorMessage = "Not found";
                return response;
            }
           
      

            try
            {
                db.Customer_Type.Remove(c_Type);
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

    }
}
