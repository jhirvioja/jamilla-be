using JamillaBackend.Models;

namespace JamillaBackend.Services
{
    public interface IRecipeService
    {
        Task<List<Recipe>> GetAllRecipes();
        Task<Recipe> GetOneRecipe(string recipeid);
        int AddOneRecipe(Recipe recipe);
        Task<int> UpdateOneRecipe(Recipe recipe, string recipeid);
        Task<int> DeleteOneRecipe(string recipeid);
        Task<List<Recipe>> GetAllRecipesPagination(int skip, int take);
        Task<SearchResults> SearchRecipes(string keyword);
        Task<int> GetAmountOfRecipes(string userid);
    }
}
