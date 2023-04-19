using JamillaBackend.Models;
using JamillaBackend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace JamillaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class RecipesController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipesController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        /// <summary>
        /// Returns all recipes from database
        /// </summary>
        /// <returns>Recipes</returns>
        [HttpGet()]
        public async Task<IActionResult> GetAllRecipes()
        {
            try
            {
                var recipes = await _recipeService.GetAllRecipes();
                if (recipes.Count > 0)
                {
                    return Ok(recipes);
                }
                return StatusCode(404, "Recipes not found");
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Returns one recipe from database
        /// </summary>
        /// <returns>Recipe</returns>
        [HttpGet("{recipeid}")]
        public async Task<IActionResult> GetRecipe(string recipeid)
        {
            try
            {
                return Ok(await _recipeService.GetOneRecipe(recipeid));
            }
            catch (CosmosException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return StatusCode(404, "Recipe not found");
                }
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Posts one recipe to database
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Recipe>> AddRecipe([FromBody] Recipe recipe)
        {
            try
            {
                var numberOfStateEntries = _recipeService.AddOneRecipe(recipe);
                if (numberOfStateEntries > 0)
                {
                    return Ok("Recipe added to database");
                }
                return StatusCode(500, "Something went wrong");
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Updates one recipe in database
        /// </summary>
        /// <returns></returns>
        [HttpPut("{recipeid}")]
        public async Task<ActionResult<Recipe>> UpdateRecipe([FromBody] Recipe recipe, string recipeid)
        {
            try
            {
                var numberOfStateEntries = await _recipeService.UpdateOneRecipe(recipe, recipeid);
                if (numberOfStateEntries > 0)
                {
                    return Ok("Recipe updated in database");
                }
                return StatusCode(500, "Something went wrong");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Deletes one recipe from database
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{recipeid}")]
        public async Task<IActionResult> DeleteRecipe(string recipeid)
        {
            try
            {
                var numberOfStateEntries = await _recipeService.DeleteOneRecipe(recipeid);
                if (numberOfStateEntries > 0)
                {
                    return Ok("Recipe deleted from database");
                }
                return StatusCode(500, "Something went wrong");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Search from "name" and "tags" according to a keyword
        /// </summary>
        /// <returns></returns>
        [HttpGet("search/{keyword}")]
        public async Task<IActionResult> SearchRecipes(string keyword)
        {
            try
            {
                var results = await _recipeService.SearchRecipes(keyword);
                return Ok(results);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Get Recipes with pagination
        /// </summary>
        /// <returns></returns>
        [HttpGet("pagination/{skip}/{take}")]
        public async Task<IActionResult> GetAllRecipesPagination(int skip, int take)
        {
            try
            {
                var results = await _recipeService.GetAllRecipesPagination(skip, take);
                return Ok(results);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Returns amount of recipes user has in database
        /// </summary>
        /// <returns>Recipes</returns>
        [HttpGet("amount/{userid}")]
        public async Task<IActionResult> GetAmountOfRecipes(string userid)
        {
            try
            {
                var count = await _recipeService.GetAmountOfRecipes(userid);
                return Ok(count);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
