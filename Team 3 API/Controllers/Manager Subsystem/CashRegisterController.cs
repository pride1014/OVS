using OVS_Team_3_API.Models;
using OVS_Team_3_API.ViewModels;
using OVS_Team_3_API.ViewModels.Manager_Subsystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OVS_Team_3_API.Controllers.Manager_Subsystem
{
    [RoutePrefix("api/CashRegister")]
    public class CashRegisterController : ApiController
    {
        OVSEntities5 db = new OVSEntities5();
        // GET: CashRegister
        [Route("GetCashRegister")]
        [HttpGet]
        public List<CashRegisterVM> GetCashRegister()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Cash_Register.Select(zz => new CashRegisterVM
            {
                RegisterID = zz.Register_ID,
                CashRegisterName = zz.Cash_Register_Name,
                BranchID = zz.Branch_ID,

            }).ToList();
        }

        [Route("GetCashRegisterByID/{id:int}")]
        [HttpGet]
        public object GetCashRegisterByID(int id)
        {

            db.Configuration.ProxyCreationEnabled = false;

            Cash_Register CR = db.Cash_Register.Find(id);
            if (CR == null)
            {
                return NotFound();
            }
            return CR;

        }
        //Add: CashRegister
        [Route("CreateCashRegister")]
        [HttpPost]
        public ResponseObject CreateCashRegister([FromBody] CashRegisterVM CR)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();
            var newCR = new Cash_Register
            {
                Cash_Register_Name = CR.CashRegisterName,
                Branch_ID = CR.BranchID,

            };
            try
            {
                db.Cash_Register.Add(newCR);
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

        // UpdateCashRegister

        [Route("UpdateCashRegister")]
        [HttpPut]
        public ResponseObject UpdateCashRegister([FromBody] CashRegisterVM CR)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();

            var toUpdate = db.Cash_Register.Where(zz => zz.Register_ID
            == CR.RegisterID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "The cash register that you are trying to edit was not found in the system.";
                return response;
            }

            try
            {
                toUpdate.Cash_Register_Name = CR.CashRegisterName;
                toUpdate.Branch_ID = CR.BranchID;
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
        //DeleteCashRegister
        [Route("DeleteCashRegister/{id:int}")]
        [HttpDelete]
        public object DeleteCashRegister(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Cash_Register cr = db.Cash_Register.Find(id);
            if (cr == null)
            {
                return NotFound();
            }
            db.Cash_Register.Remove(cr);
            db.SaveChanges();

            return "The Cash Register record has been deleted";

        }

    }
}
