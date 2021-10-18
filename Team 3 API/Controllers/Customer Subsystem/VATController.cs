using OVS_Team_3_API.Models;
using OVS_Team_3_API.ViewModels;
using OVS_Team_3_API.ViewModels.Customer_Subsystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OVS_Team_3_API.Controllers.Customer_Subsystem
{
    [RoutePrefix("api/VAT")]
    public class VATController : ApiController
    {
        OVSEntities5 db = new OVSEntities5();
        // GET: VAT
        [Route("GetVAT")]
        [HttpGet]
        public List<VATVM> GetVAT()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.VATs.Select(zz => new VATVM
            {
                VATID = zz.VAT_ID,
                VATDate = zz.VAT_Date,
                VATPercentage =zz.VAT_Percentage,

            }).ToList();
        }

        [Route("GetVATByID/{id:int}")]
        [HttpPost]
        public object GetVATByID(int id)
        {

            db.Configuration.ProxyCreationEnabled = false;

            VAT vAT = db.VATs.Find(id);
            if (vAT == null)
            {
                return NotFound();
            }
            return vAT;

        }
        //Add: VAT
        [Route("AddVAT")]
        [HttpPost]
        public ResponseObject AddVAT([FromBody] VATVM CR)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();
            var newVat = new VAT
            {
                VAT_Date = CR.VATDate,
                VAT_Percentage = CR.VATPercentage,

            };
            try
            {
                db.VATs.Add(newVat);
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

        // UpdateVAT

        [Route("UpdateVAT")]
        [HttpPut]
        public ResponseObject UpdateVAT([FromBody] VATVM CR)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();

            var toUpdate = db.VATs.Where(zz => zz.VAT_ID
            == CR.VATID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "The VAT that you are trying to find was not found in the system.";
                return response;
            }

            try
            {
                toUpdate.VAT_Date = CR.VATDate;
                toUpdate.VAT_Percentage = CR.VATPercentage;
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
        //DeleteVAT
        [Route("DeleteVAT/{id:int}")]
        [HttpDelete]
        public object DeleteVAT(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            VAT aT = db.VATs.Find(id);
            if (aT == null)
            {
                return NotFound();
            }
            db.VATs.Remove(aT);
            db.SaveChanges();

            return "The VAT record has been deleted";

        }

    }
}
