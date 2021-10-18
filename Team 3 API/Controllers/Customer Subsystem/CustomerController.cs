using OVS_Team_3_API.Models;
using OVS_Team_3_API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using HttpDeleteAttribute = System.Web.Http.HttpDeleteAttribute;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using HttpPutAttribute = System.Web.Http.HttpPutAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;
using RoutePrefixAttribute = System.Web.Http.RoutePrefixAttribute;

namespace OVS_Team_3_API.Controllers
{
    [RoutePrefix("api/Customer")]
    public class CustomerController : ApiController
    {
        OVSEntities5 db = new OVSEntities5();
        // GET: Customer
        [Route("GetCustomer")]
        [HttpGet]
        public List<CustomerVM> GetCustomer()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Customers.Include(zz=>zz.Customer_Type).Select(zz => new CustomerVM
            {
                CustomerID = zz.Customer_ID,
                CustomerName = zz.Customer_Name,
                CustomerSurname = zz.Customer_Surname,
                CustomerCellphoneNumber = zz.Customer_Cellphone_Number,
                CustomerDateOfBirth = zz.Customer_Date_Of_Birth,
                CustomerEmailAddress = zz.Customer_Email_Address,
                CustomerPhysicalAddress = zz.Customer_Physical_Address,
                CustomerTypeID = zz.Customer_Type_ID,
                UserID = zz.User_ID,
                CustomerTypeDescription=zz.Customer_Type.Customer_Type_Description

            }).ToList();
        }

        // Get Customer by ID

        [System.Web.Http.Route("getCustomerByID/{id:int}")]
        [System.Web.Mvc.HttpPost]
        [HttpPost]
        public object getCustomer(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Customer cust = db.Customers.Find(id);
            if (cust == null)
            {
                return NotFound();
            }
            return cust;

        }

        //Add: Customer
        [Route("CreateCustomer")]
        [HttpPost]
        public ResponseObject CreateCustomer([FromBody] CustomerVM cust)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();
            var NewCust = new Customer
            {
                Customer_Name = cust.CustomerName,
                Customer_Surname = cust.CustomerSurname,
                Customer_Cellphone_Number = cust.CustomerCellphoneNumber,
                Customer_Date_Of_Birth = cust.CustomerDateOfBirth,
                Customer_Email_Address = cust.CustomerEmailAddress,
                Customer_Physical_Address = cust.CustomerPhysicalAddress,
                Customer_Type_ID = cust.CustomerTypeID,
                User_ID = cust.UserID
            };

            try
            {
                db.Customers.Add(NewCust);
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

        // Update Customer

        [Route("UpdateCustomer")]
        [HttpPut]
        public ResponseObject UpdateCustomer([FromBody] CustomerVM cust)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();

            var toUpdate = db.Customers.Where(zz => zz.Customer_ID
            == cust.CustomerID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "Not found";
                return response;
            }

            try
            {
                toUpdate.Customer_Name = cust.CustomerName;
                toUpdate.Customer_Surname = cust.CustomerSurname;
                toUpdate.Customer_Cellphone_Number = cust.CustomerCellphoneNumber;
                toUpdate.Customer_Date_Of_Birth = cust.CustomerDateOfBirth;
                toUpdate.Customer_Email_Address = cust.CustomerEmailAddress;
                toUpdate.Customer_Physical_Address = cust.CustomerPhysicalAddress;
                toUpdate.Customer_Type_ID = cust.CustomerTypeID;
                toUpdate.User_ID = cust.UserID;

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

        //Delete Customer
        [System.Web.Http.Route("DeleteCustomer/{id:int}")]
        [System.Web.Mvc.HttpDelete]
        [HttpDelete]
        public object DeleteCustomer(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Customer cust = db.Customers.Find(id);
            if (cust == null)
            {
                return NotFound();
            }
            db.Customers.Remove(cust);
            db.SaveChanges();

            return "Customer deleted";

        }
    }
}