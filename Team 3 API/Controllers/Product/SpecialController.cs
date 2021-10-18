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
    [RoutePrefix("api/Special")]
    public class SpecialController : ApiController
    {
        OVSEntities5 db = new OVSEntities5();

        // GET: Special
        [Route("GetSpecial")]
        [HttpGet]
        public List<SpecialVM> GetSpecial()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Specials.Select(zz => new SpecialVM
            {
                SpecialID = zz.Special_ID,
                StartDate = zz.Start_Date,
                EndDate = zz.End_Date,

            }).ToList();
        }

        // Get Special by ID

        [System.Web.Http.Route("getSpecialByID/{id:int}")]
        [System.Web.Mvc.HttpPost]
        [HttpPost]
        public object getSpecialByID(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Models.Special special = db.Specials.Find(id);
            if (special == null)
            {
                return NotFound();
            }
            return special;
        }

        //Add: Special
        [Route("CreateSpecial")]
        [HttpPost]
        public ViewModels.ResponseObject CreateSpecial([FromBody] SpecialVM special)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ViewModels.ResponseObject();
            var NewSpecial = new Models.Special
            {
                Start_Date = special.StartDate,
                End_Date = special.EndDate,
            };

            try
            {
                db.Specials.Add(NewSpecial);
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

        // Update Special

        [Route("UpdateSpecial")]
        [HttpPut]
        public ViewModels.ResponseObject UpdateSpecial([FromBody] SpecialVM special)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ViewModels.ResponseObject();

            var toUpdate = db.Specials.Where(zz => zz.Special_ID
            == special.SpecialID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "Not found";
                return response;
            }

            try
            {
                toUpdate.Start_Date = special.StartDate;
                toUpdate.End_Date = special.EndDate;

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

        //Delete Special
        [System.Web.Http.Route("DeleteSpecial/{id:int}")]
        [System.Web.Mvc.HttpDelete]
        [HttpDelete]
        public object DeleteSpecial(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Models.Special special = db.Specials.Find(id);
            if (special == null)
            {
                return NotFound();
            }
            db.Specials.Remove(special);
            db.SaveChanges();

            return "special deleted";

        }
    }
}