using OVS_Team_3_API.Models;
using OVS_Team_3_API.ViewModels.Product;
using System;
using System.Collections.Generic;
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
using OVS_Team_3_API.ViewModels;

namespace OVS_Team_3_API.Controllers.Product
{
    [RoutePrefix("api/ProductSize")]
    public class ProductSizeController : ApiController
    {
        OVSEntities5 db = new OVSEntities5();

        // GET: ProductSize
        [Route("GetProductSize")]
        [HttpGet]
        public List<ProductSizeVM> GetProductSize()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Product_Size.Include(X => X.Product ).Include(x=> x.Prices).Include(x=>x.Size).Select(zz => new ProductSizeVM
            {
                ProductID = zz.Product_ID,
                ProductSizeID = zz.Product_Size_ID,
                ProductName = zz.Product.Product_Name,
                ProductImage=zz.Product.Product_Image,
                ProductDescription = zz.Product.Product_Description,
                PriceAmount =zz.Prices.Where(x=> x.Product_Size.Product_ID== zz.Product_ID).OrderByDescending(x=> x.Price_Date).Select(x=> x.Price_Amount).FirstOrDefault(),
                SizeDescription= zz.Size.Size_Description,
                SizeID = zz.Size_ID,
                PriceID=zz.Prices.Where(x => x.Product_Size.Product_ID == zz.Product_ID).OrderByDescending(x => x.Price_Date).Select(x => x.Price_ID).FirstOrDefault(),

            }).ToList();
        }

        // Get ProductSize by ID

        [System.Web.Http.Route("getProductSizeByID/{id:int}")]
     
        [HttpGet]
        public object getProductSizeByID(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Models.Product_Size product = db.Product_Size.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        [System.Web.Http.Route("getProductSIzeByProductID/{id:int}")]
        [HttpGet]
        public object getProductSIzeByProductID(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            return db.Product_Size.Include(X => X.Product).Include(x => x.Prices).Include(x => x.Size).Select(zz => new ProductSizeVM
            {
                ProductID = zz.Product_ID,
                ProductSizeID = zz.Product_Size_ID,
                ProductName = zz.Product.Product_Name,
                ProductImage = zz.Product.Product_Image,
                ProductDescription = zz.Product.Product_Description,
                PriceAmount = zz.Prices.Where(x => x.Product_Size.Product_ID == zz.Product_ID).OrderByDescending(x => x.Price_Date).Select(x => x.Price_Amount).FirstOrDefault(),
                SizeDescription = zz.Size.Size_Description,
                SizeID = zz.Size_ID,
                PriceID = zz.Prices.Where(x => x.Product_Size.Product_ID == zz.Product_ID).OrderByDescending(x => x.Price_Date).Select(x => x.Price_ID).FirstOrDefault(),
                Quantityonhand=zz.Product.Quantity_on_hand

            }).Where(x => x.ProductID ==id).FirstOrDefault();



        }



        [System.Web.Http.Route("getProductSIzeBySizeID/{id:int}")]
        [HttpGet]
        public object getProductSIzeBySizeID(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            var productsizes = db.Product_Size.Join(db.Sizes,
                a => a.Size_ID,
                t => t.Size_ID,
                (a, t) => new
                {
                    
                    SizeID = a.Size_ID,
                    ProductID = a.Product_ID,
                     ProductSizeID= a.Product_Size_ID

                }).Where(pp => pp.SizeID == id);

            return Ok(productsizes);



        }

        //Add: ProductSize
        [Route("CreateProductSize")]
        [HttpPost]
        public ViewModels.ResponseObject CreateProductSize([FromBody] ProductSizeVM product)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ViewModels.ResponseObject();
            var Newpord = new Models.Product_Size
            {
                Product_ID = product.ProductID,
                Product_Size_ID = product.ProductSizeID,
                Size_ID = product.SizeID,

             
            };

            try
            {
                db.Product_Size.Add(Newpord);
                db.SaveChanges();
                var newPrice = new Models.Price
                {
                    Product_Size_ID = Newpord.Product_Size_ID,
                    Price_Amount = (float)product.PriceAmount,
                    Price_Date = DateTime.Now.Date
                };
                db.Prices.Add(newPrice);
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

        // Update ProductSize

        [Route("UpdateProductSize")]
        [HttpPut]
        public ViewModels.ResponseObject UpdateProductSize([FromBody] ProductSizeVM product)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ViewModels.ResponseObject();

            var toUpdate = db.Product_Size.Where(zz => zz.Product_Size_ID
            == product.ProductSizeID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "Not found";
                return response;
            }

            try
            {
                toUpdate.Product_ID = product.ProductID;
          
                toUpdate.Size_ID = product.SizeID;

                db.SaveChanges();

               // var UpdatePrice = db.Prices.Where(zz => zz.Price_ID == product.PriceID && zz.Price_ID ==zz.Product_Size_ID).FirstOrDefault();
              //  UpdatePrice.Price_Amount = (float)product.PriceAmount;
              //  UpdatePrice.Price_Date = DateTime.Now.Date;
               // db.SaveChanges();

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

        //Delete ProductSize
        [System.Web.Http.Route("DeleteProductSize/{id:int}")]
        [System.Web.Mvc.HttpDelete]
        [HttpDelete]
        public ResponseObject DeleteProductSize(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ViewModels.ResponseObject();

            Models.Product_Size product = db.Product_Size.Find(id);
            if (product == null)
            {
                response.Success = false;
                response.ErrorMessage = "Not found";
                return response;
            }


            try
            {
                db.Product_Size.Remove(product);
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