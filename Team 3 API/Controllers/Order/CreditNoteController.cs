using OVS_Team_3_API.Models;
using OVS_Team_3_API.ViewModels;
using OVS_Team_3_API.ViewModels.Credit_Note;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OVS_Team_3_API.Controllers.Employee_Subsystem
{
    [RoutePrefix("api/User")]
    public class CreditNoteController : ApiController
    {
        OVSEntities5 db = new OVSEntities5();
        // GET: User
        [Route("GetCreditNotes")]
        [HttpGet]
        public List<CreditNoteVM> GetCreditNotes()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Credit_Note.Select(zz => new CreditNoteVM
            {
                CreditNoteID = zz.Credit_Note_ID,
                ReturnOrderRequestID = zz.Return_Order_Request_ID,
                CustomerID = zz.Customer_ID,

            }).ToList();
        }


        // Get Credit Note by ID
        [System.Web.Http.Route("GetCreditNoteByID/{id:int}")]
        [System.Web.Mvc.HttpPost]
        [HttpPost]
        public object GetCreditNoteByID(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Credit_Note user = db.Credit_Note.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;

        }


        //Add: Credit Note
        [Route("CreateCreditNote")]
        [HttpPost]
        public ResponseObject CreateCreditNote([FromBody] CreditNoteVM credit)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();
            var newcreditNote = new Credit_Note
            {
                Credit_Note_ID = credit.CreditNoteID,
                Return_Order_Request_ID = credit.ReturnOrderRequestID,
                Customer_ID = credit.CustomerID,


            };

            try
            {
                db.Credit_Note.Add(newcreditNote);
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


        // Update Credit Note

        [Route("UpdateCreditNote")]
        [HttpPut]
        public ResponseObject UpdateCreditNote([FromBody] CreditNoteVM credit)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();

            var toUpdate = db.Credit_Note.Where(zz => zz.Credit_Note_ID
            == credit.CreditNoteID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "Not found";
                return response;
            }

            try
            {
              
                toUpdate.Return_Order_Request_ID = credit.ReturnOrderRequestID;
                toUpdate.Customer_ID = credit.CustomerID;

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

        //Delete Credit NOte
        [System.Web.Http.Route("DeleteCreditNote/{id:int}")]
        [System.Web.Mvc.HttpDelete]
        [HttpDelete]
        public object DeleteCreditNote(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Credit_Note creditN = db.Credit_Note.Find(id);
            if (creditN == null)
            {
                return NotFound();
            }
            db.Credit_Note.Remove(creditN);
            db.SaveChanges();

            return "deleted";

        }

    }
}
