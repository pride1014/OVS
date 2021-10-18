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
    [RoutePrefix("api/SupplierOrderLine")]
    public class SupplierOrderLineController : ApiController
    {
        OVSEntities5 db = new OVSEntities5();

        // GET: SupplierOrderLine
        [Route("GetSupplierOrderLine")]
        [HttpGet]
        public List<SupplierOrderLineVM> GetSupplierOrderLine()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Supplier_Order_Line.Select(zz => new SupplierOrderLineVM
            {
                SupplierOrderLineID = zz.Supplier_Order_Line_ID,
                Quantity = zz.Quantity,
                ProductID = zz.Product_ID,
                RawMaterialID = zz.Raw_Material_ID,
                SupplierOrderID = zz.Supplier_Order_ID

            }).ToList();
        }

        // Get SupplierOrderLine by ID

        [System.Web.Http.Route("getSupplierOrderLineByID/{id:int}")]
        [System.Web.Mvc.HttpPost]
        [HttpPost]
        public object getSupplierOrderLineByID(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Models.Supplier_Order_Line supplier = db.Supplier_Order_Line.Find(id);
            if (supplier == null)
            {
                return NotFound();
            }
            return supplier;

        }

        //Add: SupplierOrderLine
        [Route("CreateSupplierOrderLine")]
        [HttpPost]
        public ViewModels.ResponseObject CreateSupplierOrderLine([FromBody] SupplierOrderLineVM supplier)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ViewModels.ResponseObject();
            var NewSup = new Models.Supplier_Order_Line
            {
                Supplier_Order_ID = supplier.SupplierOrderID,
                Quantity = supplier.Quantity,
                Product_ID = supplier.ProductID,
                Raw_Material_ID = supplier.RawMaterialID
            };

            try
            {
                db.Supplier_Order_Line.Add(NewSup);
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

        // Update SupplierOrderLine

        [Route("UpdateSupplierOrderLine")]
        [HttpPut]
        public ViewModels.ResponseObject UpdateSupplierOrderLine([FromBody] SupplierOrderLineVM supplier)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ViewModels.ResponseObject();

            var toUpdate = db.Supplier_Order_Line.Where(zz => zz.Supplier_Order_Line_ID
            == supplier.SupplierOrderLineID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "Not found";
                return response;
            }

            try
            {
                toUpdate.Supplier_Order_ID = supplier.SupplierOrderID;
                toUpdate.Quantity = supplier.Quantity;
                toUpdate.Product_ID = supplier.ProductID; 
                toUpdate.Raw_Material_ID = supplier.RawMaterialID;

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

        //Delete SupplierOrderLine
        [System.Web.Http.Route("DeleteSupplierOrderLine/{id:int}")]
        [System.Web.Mvc.HttpDelete]
        [HttpDelete]
        public object DeleteSupplierOrderLine(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Models.Supplier_Order_Line supplier = db.Supplier_Order_Line.Find(id);
            if (supplier == null)
            {
                return NotFound();
            }
            db.Supplier_Order_Line.Remove(supplier);
            db.SaveChanges();

            return "SupplierOrderLines deleted";

        }
    }
}