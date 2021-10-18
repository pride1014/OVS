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
    [RoutePrefix("api/Branch")]
    public class BranchController : ApiController
    {
        OVSEntities5 db = new OVSEntities5();
        // GET: Branch
        [Route("GetBranch")]
        [HttpGet]
        public List<BranchVM> GetBranch()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Branches.Select(zz => new BranchVM
            {
                BranchID = zz.Branch_ID,
                BranchName = zz.Branch_Name,
                BranchLocationStorageCapacity = zz.Branch_Location_Storage_Capacity,
                BranchAddress = zz.Branch_Address,


            }).ToList();
        }

        [Route("GetBranchByID/{id:int}")]
        [HttpGet]
        public object GetBranchByID(int id)
        {
          

            db.Configuration.ProxyCreationEnabled = false;

            Branch branch = db.Branches.Find(id);
            if (branch == null)
            {
                return NotFound();
            }
            return branch;

        }
        //Add: Branch
        [Route("CreateBranch")]
        [HttpPost]
        public ResponseObject CreateBranch([FromBody] BranchVM branch)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();
            var newBranch = new Branch
            {
                Branch_Name = branch.BranchName,
                Branch_Location_Storage_Capacity = branch.BranchLocationStorageCapacity,
                Branch_Address = branch.BranchAddress,

            };
            try
            {
                db.Branches.Add(newBranch);
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

        // UpdateBranch

        [Route("UpdateBranch")]
        [HttpPut]
        public ResponseObject UpdateBranch([FromBody] BranchVM branch)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();

            var toUpdate = db.Branches.Where(zz => zz.Branch_ID
            == branch.BranchID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "The Branch that you are trying to edit was not found in the system.";
                return response;
            }

            try
            {
                toUpdate.Branch_Name = branch.BranchName;
                toUpdate.Branch_Location_Storage_Capacity = branch.BranchLocationStorageCapacity;
                toUpdate.Branch_Address = branch.BranchAddress;
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
        //DeleteBranch
        [Route("DeleteBranch/{id:int}")]
        [HttpDelete]
        public ViewModels.ResponseObject DeleteBranch(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ViewModels.ResponseObject();

            Branch branch = db.Branches.Find(id);
            if (branch == null)
            {
                response.Success = false;
                response.ErrorMessage = "Not found";
                return response;
            }
            try
            {
                db.Branches.Remove(branch);
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

    }
}
