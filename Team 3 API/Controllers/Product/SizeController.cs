using OVS_Team_3_API.Models;
using OVS_Team_3_API.ViewModels;
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
    [RoutePrefix("api/Size")]
    public class SizeController : ApiController
    {
        OVSEntities5 db = new OVSEntities5();

        // GET: Size
        [Route("GetSize")]
        [HttpGet]
        public List<SizeVM> GetSize()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Sizes.Select(zz => new SizeVM
            {
                SizeID = zz.Size_ID,
                SizeDescription = zz.Size_Description

            }).ToList();
        }

        // Get Size by ID

        [System.Web.Http.Route("getSizeByID/{id:int}")]
        [HttpGet]
        public object getSizeByID(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Models.Size product = db.Sizes.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        //Add: Size
        [Route("CreateSize")]
        [HttpPost]
        public ViewModels.ResponseObject CreateSize([FromBody] SizeVM size)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ViewModels.ResponseObject();
            var NewSize = new Models.Size
            {
                Size_Description = size.SizeDescription,

            };

            try
            {
                db.Sizes.Add(NewSize);
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

        // Update Size

        [Route("UpdateSize")]
        [HttpPut]
        public ViewModels.ResponseObject UpdateSize([FromBody] SizeVM size)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ViewModels.ResponseObject();

            var toUpdate = db.Sizes.Where(zz => zz.Size_ID
            == size.SizeID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "Not found";
                return response;
            }

            try
            {
                toUpdate.Size_Description = size.SizeDescription;

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

        //Delete Size
        [System.Web.Http.Route("DeleteSize/{id:int}")]
        [HttpDelete]
        public ResponseObject DeleteSize(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ViewModels.ResponseObject();

            Models.Size size = db.Sizes.Find(id);
            if (size == null)
            {
                response.Success = false;
                response.ErrorMessage = "Not found";
                return response;
            }
          

            try
            {
                db.Sizes.Remove(size);
                db.SaveChanges();

                response.Success = true;
                response.ErrorMessage = "Size deleted";
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