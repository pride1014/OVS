using OVS_Team_3_API.Models;
using OVS_Team_3_API.ViewModels;
using OVS_Team_3_API.ViewModels.Raw_Materials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OVS_Team_3_API.Controllers.Raw_Materials
{
    [RoutePrefix("api/rawmaterials")]
    public class RawMaterialController : ApiController
    {
        OVSEntities5 db = new OVSEntities5();
        // GET: UserAccess
        [Route("Getrawmaterials")]
        [HttpGet]
        public List<RawMaterialVM> GetUserAccess()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Raw_Material.Include(z=>z.Unit).Select(zz => new RawMaterialVM
            {
                RawMaterialID = zz.Raw_Material_ID,
                RawMaterialName = zz.Raw_Material_Name,
                Rawmaterialdescription = zz.Raw_material_description,
                QuantityOnhand=zz.Quantity_on_hand,
                UnitID=zz.Unit_ID,
                UnitMeasurement=zz.Unit.Unit_Measurement,
                Unit =zz.Unit

            }).ToList();
        }



        // Get Raw Materials by ID

        [System.Web.Http.Route("getRawMaterialByID/{id:int}")]
        [HttpGet]
        public object getRawMaterial(int id)

        {

            db.Configuration.ProxyCreationEnabled = false;

            Raw_Material mat = db.Raw_Material.Find(id);
            if (mat == null)
            {
                return NotFound();
            }
            return mat;

        }


        //Add: RawMaterial
        [Route("CreateRawMaterial")]
        [HttpPost]
        public ResponseObject CreateRawMaterial([FromBody] RawMaterialVM raw)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();
            var newraw = new Raw_Material
            {
                Raw_Material_Name = raw.RawMaterialName,
                Raw_material_description = raw.Rawmaterialdescription,
                Quantity_on_hand=raw.QuantityOnhand,
                Unit_ID=raw.UnitID,
                Unit=raw.Unit

            };

            try
            {
                db.Raw_Material.Add(newraw);
                db.SaveChanges();
                var newUnit = new Models.Unit
                {
                    
                    Unit_Measurement=raw.UnitMeasurement

                };
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


        // Update RawMaterial

        [Route("UpdateRawMaterial")]
        [HttpPut]
        public ResponseObject UpdateRawMaterial([FromBody] RawMaterialVM raw)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();

            var toUpdate = db.Raw_Material.Where(zz => zz.Raw_Material_ID
            == raw.RawMaterialID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "The Raw Material that you are trying to update was not found in the system.";
                return response;
            }

            try
            {
                toUpdate.Raw_Material_Name = raw.RawMaterialName;
                toUpdate.Raw_material_description = raw.Rawmaterialdescription;
                toUpdate.Quantity_on_hand = raw.QuantityOnhand;
                toUpdate.Unit_ID = raw.UnitID;
                toUpdate.Unit = raw.Unit;

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


        //Delete Raw Material
        [System.Web.Http.Route("DeleteRawMaterial/{id:int}")]
        [System.Web.Mvc.HttpDelete]
        [HttpDelete]
        public object DeleteRawMaterial(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Raw_Material raw = db.Raw_Material.Find(id);
            if (raw == null)
            {
                return NotFound();
            }
            db.Raw_Material.Remove(raw);
            db.SaveChanges();

            return "The raw material permission has been deleted";

        }
    }
}
