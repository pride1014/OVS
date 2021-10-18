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
    [RoutePrefix("api/cart")]
    public class CartController : ApiController
    {
        OVSEntities5 db = new OVSEntities5();
        // GET: Cart items
        [Route("GetCart")]
        [HttpGet]
        public List<CartVM> GetCart()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Carts.Select(zz => new CartVM
            {
                CartID = zz.Cart_ID,
                CustomerID = zz.Customer_ID,
                UserID = zz.User_ID,
               

            }).ToList();
        }


        // Get Cart Item by ID

        [System.Web.Http.Route("getCartByID/{id:int}")]
        [System.Web.Mvc.HttpPost]
        [HttpPost]
        public object getCartByID(int id)

        {

            db.Configuration.ProxyCreationEnabled = false;

            Cart cart = db.Carts.Find(id);
            if (cart == null)
            {
                return NotFound();
            }
            return cart;

        }

        //Add: Cart
        [Route("CreateCart")]
        [HttpPost]
        public ResponseObject CreateCart([FromBody] CartVM cart)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();
            var newCart = new Cart
            {
                Cart_ID = cart.CartID,
                Customer_ID = cart.CustomerID,
                User_ID=cart.UserID

            };

            try
            {
                db.Carts.Add(newCart);
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


        // Update Cart

        [Route("Updatecart")]
        [HttpPut]
        public ResponseObject Updatecart([FromBody] CartVM cart)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();

            var toUpdate = db.Carts.Where(zz => zz.Cart_ID
            == cart.CartID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "The Cart item that you are trying to update was not found in the system.";
                return response;
            }

            try
            {
                toUpdate.User_ID = cart.UserID;
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


        //Delete Cart
        [System.Web.Http.Route("DeleteCart/{id:int}")]
        [System.Web.Mvc.HttpDelete]
        [HttpDelete]
        public object DeleteCart(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Cart cart = db.Carts.Find(id);
            if (cart == null)
            {
                return NotFound();
            }
            db.Carts.Remove(cart);
            db.SaveChanges();

            return "The cart items has been deleted";

        }


    }
}
