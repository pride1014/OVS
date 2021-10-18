using OVS_Team_3_API.Models;
using OVS_Team_3_API.ViewModels;
using OVS_Team_3_API.ViewModels.Raw_Materials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OVS_Team_3_API.Controllers.Raw_Materials
{
    [RoutePrefix("api/unit")]
    public class UnitController : ApiController
    {
        OVSEntities5 db = new OVSEntities5();
        // GET: Unit
        [Route("GetUnit")]
        [HttpGet]
        public List<UnitVM> GetUnit()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Units.Select(zz => new UnitVM
            {
                UnitID = zz.Unit_ID,
                UnitMeasurement = zz.Unit_Measurement,

            }).ToList();
        }


        // Get Unit by ID

        [System.Web.Http.Route("getUnitByID/{id:int}")]
        [System.Web.Mvc.HttpPost]
        [HttpPost]
        public object getUnitByID(int id)

        {

            db.Configuration.ProxyCreationEnabled = false;

            Unit unit = db.Units.Find(id);
            if (unit == null)
            {
                return NotFound();
            }
            return unit;

        }


        //Add: Unit
        [Route("CreateUnit")]
        [HttpPost]
        public ResponseObject CreateUnit([FromBody] UnitVM unit)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();
            var newUnit = new Unit
            {
                Unit_Measurement = unit.UnitMeasurement,

            };

            try
            {
                db.Units.Add(newUnit);
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


        // Update Unit

        [Route("UpdateUnit")]
        [HttpPut]
        public ResponseObject UpdateUnit([FromBody] UnitVM unit)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();

            var toUpdate = db.Units.Where(zz => zz.Unit_ID
            == unit.UnitID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "The Unit that you are trying to update was not found in the system.";
                return response;
            }

            try
            {
                toUpdate.Unit_Measurement = unit.UnitMeasurement;
             

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


        //Delete Unit
        [System.Web.Http.Route("DeleteUnit/{id:int}")]
        [System.Web.Mvc.HttpDelete]
        [HttpDelete]
        public object DeleteUnit(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Unit unit = db.Units.Find(id);
            if (unit == null)
            {
                return NotFound();
            }
            db.Units.Remove(unit);
            db.SaveChanges();

            return "The Unit has been deleted";

        }
    }
}
