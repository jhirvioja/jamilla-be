using JamillaBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace JamillaBackend.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly RecipesContext _context;

        public RecipeService(RecipesContext context)
        {
            _context = context;
        }

        public async Task<List<Recipe>> GetAllRecipes()
        {
            return await _context.Recipes.ToListAsync();

        }
        public async Task<Recipe> GetOneRecipe(string recipeid)
        {
            return await _context.Recipes.Where(r => r.Id == recipeid).FirstOrDefaultAsync();
        }
        public int AddOneRecipe(Recipe recipe)
        {

            Recipe newRecipe = new()
            {
                Id = recipe.Id,
                UserId = recipe.UserId,
                PrepTime = recipe.PrepTime,
                Cost = recipe.Cost,
                Date = recipe.Date,
                Name = recipe.Name,
                ImgSrc = recipe.ImgSrc,
                Description = recipe.Description,
                Tags = recipe.Tags,
                RecipeIngredients = recipe.RecipeIngredients,
                Steps = recipe.Steps,
            };

            _context.Add(newRecipe);
            return _context.SaveChanges();
        }

        public async Task<int> UpdateOneRecipe(Recipe recipe, string recipeid)
        {
            var recipeOld = await _context.Recipes.Where(r => r.Id == recipeid).FirstOrDefaultAsync();

            recipeOld.PrepTime = recipe.PrepTime;
            recipeOld.Cost = recipe.Cost;
            recipeOld.Name = recipe.Name;
            recipeOld.ImgSrc = recipe.ImgSrc;
            recipeOld.Description = recipe.Description;
            recipeOld.Tags = recipe.Tags;
            recipeOld.RecipeIngredients = recipe.RecipeIngredients; 
            recipeOld.Steps = recipe.Steps;
            
            return _context.SaveChanges();
        }

        public async Task<int> DeleteOneRecipe(string recipeid)
        {
            _context.Recipes.Remove(await _context.Recipes.Where(r => r.Id == recipeid).FirstOrDefaultAsync());
            return _context.SaveChanges();
        }

        public async Task<List<Recipe?>> GetAllRecipesPagination(int skip, int take)
        {
            var results = await _context.Recipes
                .OrderByDescending(r => r.Date)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            return results;
        }

        public async Task<SearchResults> SearchRecipes(string keyword)
        {
            var nameQueryResults = _context.Recipes
                .Where(r => r.Name
                    .ToLower()
                    .Contains(keyword.ToLower()))
                .ToList();

            var tagsQueryResults = _context.Recipes
                .Where(r => r.Tags
                    .ToLower()
                    .Contains(keyword.ToLower()))
                .ToList();

            var searchResults = new SearchResults()
            {
                NameResults = nameQueryResults,
                TagsResults = tagsQueryResults
            };

            return searchResults;
        }

        public async Task<int> GetAmountOfRecipes(string userid)
        {
            var count = _context.Recipes.Where(r => r.UserId == userid).Count();
            return count;
        }
    }
}

