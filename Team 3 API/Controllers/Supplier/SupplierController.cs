using OVS_Team_3_API.Models;
using OVS_Team_3_API.ViewModels.Supplier;
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

namespace OVS_Team_3_API.Controllers.Supplier
{
    [RoutePrefix("api/Supplier")]
    public class SupplierController : ApiController
    {
        OVSEntities5 db = new OVSEntities5();

        // GET: Supplier
        [Route("GetSupplierr")]
        [HttpGet]
        public List<SupplierVM> GetSupplierr()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Suppliers.Select(zz => new SupplierVM
            {
                SupplierID = zz.Supplier_ID,
                SupplierAddress = zz.Supplier_Address,
                SupplierName = zz.Supplier_Name,
                SupplierPhoneNumber = zz.Supplier_Phone_Number,

            }).ToList();
        }

        // Get Supplier by ID

        [System.Web.Http.Route("getSupplierByID/{id:int}")]
        [HttpGet]
        public object getSupplierByID(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Models.Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return NotFound();
            }
            return supplier;

        }

        //Add: Supplier
        [Route("CreateSupplier")]
        [HttpPost]
        public ViewModels.ResponseObject CreateSupplier([FromBody] SupplierVM supplier)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ViewModels.ResponseObject();
            var NewSup = new Models.Supplier
            {
                Supplier_Name = supplier.SupplierName,
                Supplier_Address = supplier.SupplierName,
                Supplier_Phone_Number = supplier.SupplierPhoneNumber,
            };

            try
            {
                db.Suppliers.Add(NewSup);
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

        // Update Supplier

        [Route("UpdateSupplier")]
        [HttpPut]
        public ViewModels.ResponseObject UpdateSupplier([FromBody] SupplierVM supplier)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ViewModels.ResponseObject();

            var toUpdate = db.Suppliers.Where(zz => zz.Supplier_ID
            == supplier.SupplierID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "Not found";
                return response;
            }

            try
            {
                toUpdate.Supplier_Name = supplier.SupplierName;
                toUpdate.Supplier_Address = supplier.SupplierAddress;
                toUpdate.Supplier_Phone_Number = supplier.SupplierPhoneNumber;

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

        //Delete Supplier
        [System.Web.Http.Route("DeleteSupplier/{id:int}")]
        [System.Web.Mvc.HttpDelete]
        [HttpDelete]
        public object DeleteSupplier(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Models.Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return NotFound();
            }
            db.Suppliers.Remove(supplier);
            db.SaveChanges();

            return "supplier deleted";

        }
    }
}