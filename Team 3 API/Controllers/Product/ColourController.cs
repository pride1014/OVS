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
    [RoutePrefix("api/Colour")]
    public class ColourController : ApiController
    {
        OVSEntities5 db = new OVSEntities5();

        // GET: Colour
        [Route("GetColour")]
        [HttpGet]
        public List<ColourVM> GetColour()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Colours.Select(zz => new ColourVM
            {
                ColourID = zz.Colour_ID,
                ColourDescription = zz.Colour_Description,
                ProductSizeID = zz.Product_Size_ID,

            }).ToList();
        }

        // Get Colour by ID

        [System.Web.Http.Route("getColourByID/{id:int}")]
        [System.Web.Mvc.HttpPost]
        [HttpPost]
        public object getColourByID(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Models.Colour colour = db.Colours.Find(id);
            if (colour == null)
            {
                return NotFound();
            }
            return colour;
        }

        //Add: Colour
        [Route("CreateColour")]
        [HttpPost]
        public ViewModels.ResponseObject CreateColour([FromBody] ColourVM colour)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ViewModels.ResponseObject();
            var NewCol = new Models.Colour
            {
                Colour_Description = colour.ColourDescription,
                Product_Size_ID = colour.ProductSizeID,
            };

            try
            {
                db.Colours.Add(NewCol);
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

        // Update Colour

        [Route("UpdateColour")]
        [HttpPut]
        public ViewModels.ResponseObject UpdateColour([FromBody] ColourVM colour)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ViewModels.ResponseObject();

            var toUpdate = db.Colours.Where(zz => zz.Colour_ID
            == colour.ColourID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "Not found";
                return response;
            }

            try
            {
                toUpdate.Colour_Description = colour.ColourDescription;
                toUpdate.Product_Size_ID = colour.ProductSizeID;

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

        //Delete Colour
        [System.Web.Http.Route("DeleteColour{id:int}")]
        [System.Web.Mvc.HttpDelete]
        [HttpDelete]
        public object DeleteColour(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Models.Colour colour = db.Colours.Find(id);
            if (colour == null)
            {
                return NotFound();
            }
            db.Colours.Remove(colour);
            db.SaveChanges();

            return "colour deleted";

        }
    }
}