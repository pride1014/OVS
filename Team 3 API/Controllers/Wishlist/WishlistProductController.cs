using OVS_Team_3_API.Models;
using OVS_Team_3_API.ViewModels.Wishlist;
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
namespace OVS_Team_3_API.Controllers.Wishlist
{
    [RoutePrefix("api/ProductType")]
    public class WishlistProductController : ApiController
    {
        OVSEntities5 db = new OVSEntities5();

        // GET: WishlistProduct
        [Route("GetWishlistProduct")]
        [HttpGet]
        public List<WishlistProductVM> GetWishlistProduct()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Wishlist_Product.Select(zz => new WishlistProductVM
            {
                WishlistID = zz.Wishlist_ID,
                WishlistProductID = zz.Wishlist_Product_ID,
                Quantity = zz.Quantity,
                ProductID = zz.Product_ID

            }).ToList();
        }

        // Get WishlistProduct by ID

        [System.Web.Http.Route("getWishlistProductByID/{id:int}")]
        [System.Web.Mvc.HttpPost]
        [HttpPost]
        public object getWishlistProductByID(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Models.Wishlist_Product wishlist_Product = db.Wishlist_Product.Find(id);
            if (wishlist_Product == null)
            {
                return NotFound();
            }
            return wishlist_Product;
        }

        //Add: WishlistProduct
        [Route("CreateWishlistProduct")]
        [HttpPost]
        public ViewModels.ResponseObject CreateWishlistProduct([FromBody] WishlistProductVM wishlistProduct)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ViewModels.ResponseObject();
            var NewWP = new Models.Wishlist_Product
            {
                Wishlist_ID = wishlistProduct.WishlistID,
                Quantity = wishlistProduct.Quantity,
                Product_ID = wishlistProduct.ProductID
            };

            try
            {
                db.Wishlist_Product.Add(NewWP);
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

        // Update WishlistProduct

        [Route("UpdateWishlistProduct")]
        [HttpPut]
        public ViewModels.ResponseObject UpdateWishlistProduct([FromBody] WishlistProductVM wishlistProduct)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ViewModels.ResponseObject();

            var toUpdate = db.Wishlist_Product.Where(zz => zz.Wishlist_Product_ID
            == wishlistProduct.WishlistProductID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "Not found";
                return response;
            }

            try
            {
                toUpdate.Wishlist_ID = wishlistProduct.WishlistID;
                toUpdate.Quantity = wishlistProduct.Quantity;
                toUpdate.Product_ID = wishlistProduct.ProductID;

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

        //Delete WishlistProduct
        [System.Web.Http.Route("DeleteWishlistProduct/{id:int}")]
        [System.Web.Mvc.HttpDelete]
        [HttpDelete]
        public object DeleteWishlistProduct(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Models.Wishlist_Product wishlist_Product = db.Wishlist_Product.Find(id);
            if (wishlist_Product == null)
            {
                return NotFound();
            }
            db.Wishlist_Product.Remove(wishlist_Product);
            db.SaveChanges();

            return "wishlist_Product deleted";

        }
    }
}