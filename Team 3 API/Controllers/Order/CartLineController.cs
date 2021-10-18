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
    [RoutePrefix("api/cartline")]
    public class CartLineController : ApiController
    {
        OVSEntities5 db = new OVSEntities5();

        // GET: Cart Line
        [Route("GetCartLine")]
        [HttpGet]
        public List<CartLineVM> GetCartLine()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Cart_Line.Select(zz => new CartLineVM
            {
                CartLineID = zz.Cart_Line_ID,
                CartID = zz.Cart_ID,
                ProductID = zz.Product_ID,
                Quantity=zz.Quantity

            }).ToList();
        }

        // Get Cart Line by ID

        [System.Web.Http.Route("getCartLineByID/{id:int}")]
        [System.Web.Mvc.HttpPost]
        [HttpPost]
        public object getCartLine(int id)

        {

            db.Configuration.ProxyCreationEnabled = false;

            Cart_Line cartline = db.Cart_Line.Find(id);
            if (cartline == null)
            {
                return NotFound();
            }
            return cartline;

        }

        //Add: CartLine
        [Route("CreateCartLine")]
        [HttpPost]
        public ResponseObject CreateCartLine([FromBody] CartLineVM cartLine)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();
            var newCartLine = new Cart_Line
            {
                Product_ID = cartLine.ProductID,
                Cart_ID = cartLine.CartID,
                Quantity= cartLine.Quantity,
             

            };

            try
            {
                db.Cart_Line.Add(newCartLine);
                db.SaveChanges();


                var newCart = new Models.Cart
                {
                    Cart_ID = (int)newCartLine.Cart_ID,
                  
                    Customer_ID = cartLine.CustomerID,
                    User_ID = cartLine.UserID,

                };
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

        // Update cart line

        [Route("UpdateCartLine")]
        [HttpPut]
        public ResponseObject UpdateCartLine([FromBody] CartLineVM cartLine)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();

            var toUpdate = db.Cart_Line.Where(zz => zz.Cart_Line_ID
            == cartLine.CartLineID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "The cartLine that you are trying to update was not found in the system.";
                return response;
            }

            try
            {
                toUpdate.Product_ID = cartLine.ProductID;
                toUpdate.Cart_ID = cartLine.CartID;
                toUpdate.Quantity = cartLine.Quantity;

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

        //Delete Cart Line
        [System.Web.Http.Route("DeleteCartLine/{id:int}")]
        [System.Web.Mvc.HttpDelete]
        [HttpDelete]
        public object DeleteCartLine(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Cart_Line line = db.Cart_Line.Find(id);
            if (line == null)
            {
                return NotFound();
            }
            db.Cart_Line.Remove(line);
            db.SaveChanges();

            return "The cart line has been deleted";

        }


    }
}
