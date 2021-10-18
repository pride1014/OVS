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
    [RoutePrefix("api/SupplierOrder")]
    public class SupplierOrderController : ApiController
    {
        OVSEntities5 db = new OVSEntities5();

        // GET: SupplierOrder
        [Route("GetSupplierOrder")]
        [HttpGet]
        public List<SupplierOrderVM> GetSupplierOrder()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Supplier_Order.Select(zz => new SupplierOrderVM
            {
                SupplierID = zz.Supplier_ID,
                SupplierOrderID = zz.Supplier_Order_ID,
                SupplierOrderDescription = zz.Supplier_Order_Description,

            }).ToList();
        }

        // Get SupplierOrder by ID

        [System.Web.Http.Route("getSupplierOrderByID/{id:int}")]
        [System.Web.Mvc.HttpPost]
        [HttpPost]
        public object getSupplierOrderByID(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Models.Supplier_Order supplier = db.Supplier_Order.Find(id);
            if (supplier == null)
            {
                return NotFound();
            }
            return supplier;

        }

        //Add: SupplierOrder
        [Route("CreateSupplierOrder")]
        [HttpPost]
        public ViewModels.ResponseObject CreateSupplierOrder([FromBody] SupplierOrderVM supplier)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ViewModels.ResponseObject();
            var NewSup = new Models.Supplier_Order
            {
                Supplier_ID = supplier.SupplierID,
                Supplier_Order_Description = supplier.SupplierOrderDescription,
            };

            try
            {
                db.Supplier_Order.Add(NewSup);
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

        // Update SupplierOrder

        [Route("UpdateSupplierOrder")]
        [HttpPut]
        public ViewModels.ResponseObject UpdateSupplierOrder([FromBody] SupplierOrderVM supplier)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ViewModels.ResponseObject();

            var toUpdate = db.Supplier_Order.Where(zz => zz.Supplier_Order_ID
            == supplier.SupplierOrderID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "Not found";
                return response;
            }

            try
            {
                toUpdate.Supplier_ID = supplier.SupplierID;
                toUpdate.Supplier_Order_Description = supplier.SupplierOrderDescription;

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

        //Delete SupplierOrder
        [System.Web.Http.Route("DeleteSupplierOrder/{id:int}")]
        [System.Web.Mvc.HttpDelete]
        [HttpDelete]
        public object DeleteSupplierOrder(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Models.Supplier_Order supplier = db.Supplier_Order.Find(id);
            if (supplier == null)
            {
                return NotFound();
            }
            db.Supplier_Order.Remove(supplier);
            db.SaveChanges();

            return "supplier order deleted";

        }
    }
}