using OVS_Team_3_API.Models;
using OVS_Team_3_API.ViewModels;
using OVS_Team_3_API.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OVS_Team_3_API.Controllers.Order
{
    public class WishListController : ApiController
    {
        OVSEntities5 db = new OVSEntities5();
        // GET: Cart items
        [Route("GetUserAccess")]
        [HttpGet]
        public List<WishListVM> GetWishList()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Wishlists.Select(zz => new WishListVM
            {
                WishlistID = zz.Wishlist_ID,
                CustomerID = zz.Customer_ID,
       


            }).ToList();
        }


        // Get WishList Item by ID

        [System.Web.Http.Route("getWishListByID/{id:int}")]
        [System.Web.Mvc.HttpPost]
        [HttpPost]
        public object getWishListByID(int id)

        {

            db.Configuration.ProxyCreationEnabled = false;

            Models.Wishlist list = db.Wishlists.Find(id);
            if (list == null)
            {
                return NotFound();
            }
            return list;

        }

        //Add: Wishlist
        [Route("CreateWishlist")]
        [HttpPost]
        public ResponseObject CreateCart([FromBody] WishListVM list)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();
            var newWishlist = new Models.Wishlist
            {
                Wishlist_ID = list.WishlistID,
                Customer_ID = list.CustomerID,
        

            };

            try
            {
                db.Wishlists.Add(newWishlist);
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


        // Update Wishlist

        [Route("UpdateWishlist")]
        [HttpPut]
        public ResponseObject UpdateWishlist([FromBody] WishListVM cart)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();

            var toUpdate = db.Wishlists.Where(zz => zz.Wishlist_ID
            == cart.WishlistID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "The Wishlist item that you are trying to update was not found in the system.";
                return response;
            }

            try
            {
              
                toUpdate.Customer_ID = cart.CustomerID;

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


        //Delete Wishlist
        [System.Web.Http.Route("DeleteWishlist/{id:int}")]
        [System.Web.Mvc.HttpDelete]
        [HttpDelete]
        public object DeleteWishlist(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Models.Wishlist list = db.Wishlists.Find(id);
            if (list == null)
            {
                return NotFound();
            }
            db.Wishlists.Remove(list);
            db.SaveChanges();

            return "The Wishlist item has been deleted";

        }

    }
}
