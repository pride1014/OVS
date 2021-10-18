using OVS_Team_3_API.Models;
using OVS_Team_3_API.ViewModels.Product;
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

namespace OVS_Team_3_API.Controllers.Product
{
    [RoutePrefix("api/ProductSpecial")]
    public class ProductSpecialController : ApiController
    {
        OVSEntities5 db = new OVSEntities5();

        // GET: ProductSpecial
        [Route("GetProductSpecial")]
        [HttpGet]
        public List<ProductSpecialVM> GetProductSpecial()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Product_Special.Select(zz => new ProductSpecialVM
            {
                ProductSpecialID = zz.Product_Special_ID,
                SpecialID = zz.Special_ID,
                ProductSizeID = zz.Product_Size_ID,
                PriceAmount = zz.Price_Amount

            }).ToList();
        }

        // Get ProductSpecial by ID

        [System.Web.Http.Route("getProductSpecialByID/{id:int}")]
        [System.Web.Mvc.HttpPost]
        [HttpPost]
        public object getProductSpecialByID(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Models.Product_Special product = db.Product_Special.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        //Add: ProductSpecial
        [Route("CreateProductSpecial")]
        [HttpPost]
        public ViewModels.ResponseObject CreateProductSpecial([FromBody] ProductSpecialVM product)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ViewModels.ResponseObject();
            var Newpord = new Models.Product_Special
            {
                Product_Size_ID = product.ProductSizeID,
                Special_ID = product.SpecialID,
                Price_Amount = product.PriceAmount,
            };

            try
            {
                db.Product_Special.Add(Newpord);
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

        // Update ProductSpecial

        [Route("UpdateProductSpecial")]
        [HttpPut]
        public ViewModels.ResponseObject UpdateProductSpecial([FromBody] ProductSpecialVM product)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ViewModels.ResponseObject();

            var toUpdate = db.Product_Special.Where(zz => zz.Product_Special_ID
            == product.ProductSpecialID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "Not found";
                return response;
            }

            try
            {
                toUpdate.Special_ID = product.SpecialID;
                toUpdate.Product_Size_ID = product.ProductSizeID;
                toUpdate.Price_Amount = product.PriceAmount;

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

        //Delete ProductSpecial

        [System.Web.Http.Route("DeleteProductSpecial/{id:int}")]
        [System.Web.Mvc.HttpDelete]
        [HttpDelete]
        public object DeleteProductSpecial(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Models.Product_Special product = db.Product_Special.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            db.Product_Special.Remove(product);
            db.SaveChanges();

            return "ProductSpecial deleted";

        }
    }
}