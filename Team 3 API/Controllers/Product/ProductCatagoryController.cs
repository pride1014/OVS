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
    [RoutePrefix("api/ProductCategory")]
    public class ProductCatagoryController : ApiController
    {
        OVSEntities5 db = new OVSEntities5();
        // GET: ProductCatagory
        [Route("GetProductCategory")]
        [HttpGet]
        public List<ProductCatagoryVM> GetProductCategory()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Product_Category.Select(zz => new ProductCatagoryVM
            {
                ProductCategoryID = zz.Product_Category_ID,
                ProductCategoryName = zz.Product_Category_Name,

            }).Where(zz => zz.ProductCategoryID == zz.ProductCategoryID).ToList();
        }

        // Get ProductCatagory by ID

        [System.Web.Http.Route("getProductCategoryByID/{id:int}")]
        [HttpGet]
        public object getProductCatagoryByID(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Models.Product_Category product = db.Product_Category.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        //Add: ProductCatagory
        [Route("CreateProductCategory")]
        [HttpPost]
        public ViewModels.ResponseObject CreateProductCatagory([FromBody] ProductCatagoryVM product)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ViewModels.ResponseObject();
            var Newpord = new Models.Product_Category
            {
                Product_Category_Name = product.ProductCategoryName,
            };

            try
            {
                db.Product_Category.Add(Newpord);
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

        // Update ProductCatagory

        [Route("UpdateProductCategory")]
        [HttpPut]
        public ViewModels.ResponseObject UpdateProductCatagory([FromBody] ProductCatagoryVM product)
        {

            db.Configuration.ProxyCreationEnabled = false;
            var response = new ViewModels.ResponseObject();

            var toUpdate = db.Product_Category.Where(zz => zz.Product_Category_ID
            == product.ProductCategoryID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "Not found";
                return response;
            }

            try
            {
                toUpdate.Product_Category_Name = product.ProductCategoryName;

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

        //Delete ProductCatagory
        [System.Web.Http.Route("DeleteProductCatagory/{id:int}")]
        [HttpDelete]
        public ViewModels.ResponseObject DeleteProductCatagory(int id)
        {
        
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ViewModels.ResponseObject();

        Product_Category product = db.Product_Category.Find(id);
            if (product == null)
            {
                response.Success = false;
                response.ErrorMessage = "Not found";
                return response;
            }

            try
            {
                db.Product_Category.Remove(product);
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