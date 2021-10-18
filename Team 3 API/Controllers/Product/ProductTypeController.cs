using OVS_Team_3_API.Models;
using OVS_Team_3_API.ViewModels;
using OVS_Team_3_API.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Data.Entity;
using System.Web.Mvc;
using HttpDeleteAttribute = System.Web.Http.HttpDeleteAttribute;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using HttpPutAttribute = System.Web.Http.HttpPutAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;
using RoutePrefixAttribute = System.Web.Http.RoutePrefixAttribute;

namespace OVS_Team_3_API.Controllers.Product
{
    [RoutePrefix("api/ProductType")]
    public class ProductTypeController : ApiController
    {
        OVSEntities5 db = new OVSEntities5();
        // GET: ProductType
        [Route("GetProductType")]
        [HttpGet]

        //public IQueryable<Object> GetProductTypes()
        //{
        //    return from a in db.Product_Type
        //           join p in db.Product_Category on a.Product_Category_ID equals p.Product_Category_ID
        //           select new
        //           {
        //               ProductTypeID = a.Product_Type_ID,
        //               ProductTypeName = a.Product_Type_Name,
        //               ProductCategoryName = p.Product_Category_Name,
        //               ProductCategoryID = p.Product_Category_ID
        //           };
        //}


        public List<ProductTypeVM> GetProductType()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var productTypes= db.Product_Type.Include(x=>x.Product_Category).Select(zz => new ProductTypeVM
            {
                ProductTypeID = zz.Product_Type_ID,
                ProductTypeName = zz.Product_Type_Name,
                ProductCategoryID = zz.Product_Category_ID,
                ProductCategoryName=zz.Product_Category.Product_Category_Name

            });
            // )

            return productTypes.Where(zz => zz.ProductCategoryID== zz.ProductCategoryID).ToList();

        }


        // Get Product Types By Category ID

        [System.Web.Http.Route("getProductTypeCategoryID/{id:int}")]
        [HttpGet]
        public object getProductTypeByCategoryID(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            var ProductTypes = db.Product_Type.Join(db.Product_Category,
                a => a.Product_Category_ID,
                t => t.Product_Category_ID,
                (a, t) => new
                {
                    ProductCategoryID = a.Product_Category_ID,
                    ProductTypeName = a.Product_Type_Name,
                    ProductTypeID = a.Product_Type_ID
                 
                }).Where(pp => pp.ProductCategoryID == id);

            return Ok(ProductTypes);

       
         
        }

        // Get ProductType by ID

        [System.Web.Http.Route("getProductTypeByID/{id:int}")]
        [HttpGet]
        public object getProductTypeByID(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Models.Product_Type product = db.Product_Type.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        //Add: ProductType
        [Route("CreateProductType")]
        [HttpPost]
        public ResponseObject CreateProductType([FromBody] ProductTypeVM product)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();
            var Newpord = new Product_Type
            {
                Product_Type_ID = product.ProductTypeID,
                Product_Type_Name = product.ProductTypeName,
                Product_Category_ID = product.ProductCategoryID,
            };

            try
            {
                db.Product_Type.Add(Newpord);
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

        // Update ProductType

        [Route("UpdateProductType")]
        [HttpPut]
        public ViewModels.ResponseObject UpdateProductType([FromBody] ProductTypeVM product)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ViewModels.ResponseObject();

            var toUpdate = db.Product_Type.Where(zz => zz.Product_Type_ID
            == product.ProductTypeID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "Not found";
                return response;
            }

            try
            {
                toUpdate.Product_Type_Name = product.ProductTypeName;
                toUpdate.Product_Category_ID = product.ProductCategoryID;

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

        //Delete ProductType
        [System.Web.Http.Route("DeleteProductType/{id:int}")]
        [System.Web.Mvc.HttpDelete]
        [HttpDelete]
        public ViewModels.ResponseObject DeleteProductType(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ViewModels.ResponseObject();

            Models.Product_Type product = db.Product_Type.Find(id);
            if (product == null)
            {
                response.Success = false;
                response.ErrorMessage = "Not found";
                return response;
            }
         

            try
            {
                db.Product_Type.Remove(product);
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
