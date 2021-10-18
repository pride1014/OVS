using OVS_Team_3_API.Models;
using OVS_Team_3_API.ViewModels;
using OVS_Team_3_API.ViewModels.Job_Subsystem;
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

namespace OVS_Team_3_API.Controllers.Job_Subsystem
{
    [RoutePrefix("api/Recipe_Line_Unit")]
    public class Recipe_Line_UnitController : ApiController
    {
        OVSEntities5 db = new OVSEntities5();

        // GET: Recipe_Line_Unit
        [Route("GetRecipe_Line_Unit")]
        [HttpGet]
        public List<Recipe_Line_UnitVM> GetRecipe_Line_Unit()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Recipe_Line_Unit.Select(zz => new Recipe_Line_UnitVM
            {
                RecipeLineUnitID = zz.Recipe_Line_unit_ID,
                Unit = zz.Unit

            }).ToList();
        }


        // Get Recipe_Line_Unit by ID

        [System.Web.Http.Route("getRecipe_Line_UnitByID/{id:int}")]
        [System.Web.Mvc.HttpPost]
        [HttpPost]
        public object getRecipe_Line_Unit(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Recipe_Line_Unit recipelineunit = db.Recipe_Line_Unit.Find(id);
            if (recipelineunit == null)
            {
                return NotFound();
            }
            return recipelineunit;

        }



        //Add: Recipe_Line_Unit
        [Route("CreateRecipe_Line_Unit")]
        [HttpPost]
        public ResponseObject CreateRecipe_Line_Unite([FromBody] Recipe_Line_UnitVM recipelineunit)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();
            var Newrecipelineunit = new Recipe_Line_Unit
            {
                Recipe_Line_unit_ID = recipelineunit.RecipeLineUnitID,
                Unit = recipelineunit.Unit
            };

            try
            {
                db.Recipe_Line_Unit.Add(Newrecipelineunit);
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



        // Update Recipe_Line_Unit

        [Route("UpdateRecipe_Line_Unit")]
        [HttpPut]
        public ResponseObject UpdateRecipe_Line_Unit([FromBody] Recipe_Line_UnitVM recipelineunit)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();

            var toUpdate = db.Recipe_Line_Unit.Where(zz => zz.Recipe_Line_unit_ID
            == recipelineunit.RecipeLineUnitID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "Not found";
                return response;
            }

            try
            {
                toUpdate.Unit = recipelineunit.Unit;

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



        //Delete Recipe_Line_Unit
        [System.Web.Http.Route("DeleteRecipe_Line_Unit/{id:int}")]
        [System.Web.Mvc.HttpDelete]
        [HttpDelete]
        public object DeleteRecipe_Line_Unit(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Recipe_Line_Unit recipelineunit = db.Recipe_Line_Unit.Find(id);
            if (recipelineunit == null)
            {
                return NotFound();
            }
            db.Recipe_Line_Unit.Remove(recipelineunit);
            db.SaveChanges();

            return "Recipe Line Unit deleted";

        }


    }
}