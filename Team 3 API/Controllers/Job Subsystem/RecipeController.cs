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
    [RoutePrefix("api/Recipe")]
    public class RecipeController : ApiController
    {

        OVSEntities5 db = new OVSEntities5();

        // GET: Recipe
        [Route("GetRecipe")]
        [HttpGet]
        public List<RecipeVM> GetRecipe()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Recipes.Select(zz => new RecipeVM
            {
                RecipeID = zz.Recipe_ID,
                RecipeDescription = zz.Recipe_Description,
                QuantityProduced = zz.Quantity_produced,
                RecipeName = zz.Recipe_Name

            }).ToList();
        }


        // Get Recipe by ID

        [System.Web.Http.Route("getRecipeByID/{id:int}")]
        [HttpGet]
        public object getRecipe(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Recipe recipe = db.Recipes.Find(id);
            if (recipe == null)
            {
                return NotFound();
            }
            return recipe;

        }


        //Add: Recipe
        [Route("CreateRecipe")]
        [HttpPost]
        public ResponseObject CreateRecipe([FromBody] RecipeVM recipe)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();
            var Newrecipe = new Recipe
            {
                Recipe_ID = recipe.RecipeID,
                Recipe_Description = recipe.RecipeDescription,
                Quantity_produced = recipe.QuantityProduced,
                Recipe_Name = recipe.RecipeName
            };

            try
            {
                db.Recipes.Add(Newrecipe);
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



        // Update Recipe

        [Route("UpdateRecipe")]
        [HttpPut]
        public ResponseObject UpdateRecipe([FromBody] RecipeVM recipe)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var response = new ResponseObject();

            var toUpdate = db.Recipes.Where(zz => zz.Recipe_ID
            == recipe.RecipeID).FirstOrDefault();

            if (toUpdate == null)
            {
                response.Success = false;
                response.ErrorMessage = "Not found";
                return response;
            }

            try
            {
                toUpdate.Recipe_Description = recipe.RecipeDescription;
                toUpdate.Quantity_produced = recipe.QuantityProduced;
                toUpdate.Recipe_Name = recipe.RecipeName;

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


        //Delete Recipe
        [System.Web.Http.Route("DeleteRecipe/{id:int}")]
        [System.Web.Mvc.HttpDelete]
        [HttpDelete]
        public object DeleteRecipe(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            Recipe recipe = db.Recipes.Find(id);
            if (recipe == null)
            {
                return NotFound();
            }
            db.Recipes.Remove(recipe);
            db.SaveChanges();

            return "Recipe deleted";

        }






    }
}