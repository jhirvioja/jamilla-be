using JamillaBackend.Models;
using System.Globalization;

namespace ServiceTests;
public class RecipeServiceTests
{
    private readonly DbContextOptions<RecipesContext> _options;

    public RecipeServiceTests()
    {
        _options = new DbContextOptionsBuilder<RecipesContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using (var context = new RecipesContext(_options))
        {
            context.Recipes.AddRange(
                new Recipe { Id = "754b4e09-d25b-4886-9bda-c4670a86c0e9", UserId = "5f144332-12d3-4d55-9d1a-ac3ce1935f14", Name = "Recipe 1", Description = "Description 1", Date = DateTimeOffset.ParseExact("2023-03-21T11:56:31+00:00", "yyyy-MM-dd'T'HH:mm:sszzz", CultureInfo.InvariantCulture), Tags = "Testablestring" },
                new Recipe { Id = "2f949b6f-6b65-4ef6-8cdf-fa405472f66e", UserId = "5f144332-12d3-4d55-9d1a-ac3ce1935f14", Name = "Recipe 2", Description = "Description 2", Date = DateTimeOffset.ParseExact("2023-03-22T11:56:31+00:00", "yyyy-MM-dd'T'HH:mm:sszzz", CultureInfo.InvariantCulture), Tags = "Testablestring" },
                new Recipe { Id = "d7934cd1-2608-4bc5-b0f5-f565d9142f04", UserId = "5f144332-12d3-4d55-9d1a-ac3ce1935f14", Name = "Recipe 3", Description = "Description 3", Date = DateTimeOffset.ParseExact("2023-03-23T11:56:31+00:00", "yyyy-MM-dd'T'HH:mm:sszzz", CultureInfo.InvariantCulture), Tags = "Testablestring" },
                new Recipe { Id = "a9ef3e0d-dee1-4d9c-9f2a-e16e3e8776d2", UserId = "5f144332-12d3-4d55-9d1a-ac3ce1935f14", Name = "Recipe 4", Description = "Description 4", Date = DateTimeOffset.ParseExact("2023-03-24T11:56:31+00:00", "yyyy-MM-dd'T'HH:mm:sszzz", CultureInfo.InvariantCulture), Tags = "Testablestring" },
                new Recipe { Id = "bcce7312-93b1-4f12-b061-605cd5b440a3", UserId = "5f144332-12d3-4d55-9d1a-ac3ce1935f14", Name = "Recipe 5", Description = "Description 5", Date = DateTimeOffset.ParseExact("2023-03-25T11:56:31+00:00", "yyyy-MM-dd'T'HH:mm:sszzz", CultureInfo.InvariantCulture), Tags = "5555" },
                new Recipe { Id = "879ba391-7e6a-40a0-84b8-392fd8b9a694", UserId = "5f144332-12d3-4d55-9d1a-ac3ce1935f14", Name = "Recipe 6", Description = "Description 6", Date = DateTimeOffset.ParseExact("2023-03-26T11:56:31+00:00", "yyyy-MM-dd'T'HH:mm:sszzz", CultureInfo.InvariantCulture), Tags = "Testablestring" },
                new Recipe { Id = "e8d8c39b-50d2-4175-aaae-95ce786e5f58", UserId = "a83aa1aa-52dc-405c-842a-cf8c19baf5e3", Name = "Recipe 7", Description = "Description 7", Date = DateTimeOffset.ParseExact("2023-03-27T11:56:31+00:00", "yyyy-MM-dd'T'HH:mm:sszzz", CultureInfo.InvariantCulture), Tags = "Testablestring" },
                new Recipe { Id = "c47aaa03-f363-48cc-a91f-0a7a1a6c71d0", UserId = "a83aa1aa-52dc-405c-842a-cf8c19baf5e3", Name = "Recipe 8", Description = "Description 8", Date = DateTimeOffset.ParseExact("2023-03-28T11:56:31+00:00", "yyyy-MM-dd'T'HH:mm:sszzz", CultureInfo.InvariantCulture), Tags = "Testablestring" },
                new Recipe { Id = "43ccf7ca-179a-4eea-a03d-c5731de90ccd", UserId = "a83aa1aa-52dc-405c-842a-cf8c19baf5e3", Name = "Recipe 9", Description = "Description 9", Date = DateTimeOffset.ParseExact("2023-03-29T11:56:31+00:00", "yyyy-MM-dd'T'HH:mm:sszzz", CultureInfo.InvariantCulture), Tags = "Testablestring" },
                new Recipe { Id = "3bf4dfbe-0523-4fba-bc83-e4cfbb8a78b4", UserId = "a83aa1aa-52dc-405c-842a-cf8c19baf5e3", Name = "Recipe 10", Description = "Description 10", Date = DateTimeOffset.ParseExact("2023-03-30T11:56:31+00:00", "yyyy-MM-dd'T'HH:mm:sszzz", CultureInfo.InvariantCulture) , Tags = "Testablestring" }
            );
            context.SaveChanges();
        }
    }

    [Fact]
    public async Task GetAllRecipes_ReturnsAllRecipes()
    {
        using (var context = new RecipesContext(_options))
        {
            var service = new RecipeService(context);

            var result = await service.GetAllRecipes();

            Assert.Equal(10, result.Count);
        }
    }

    [Fact]
    public async Task GetOneRecipe_ReturnsOneRecipe()
    {
        using (var context = new RecipesContext(_options))
        {
            var service = new RecipeService(context);

            var result = await service.GetOneRecipe("754b4e09-d25b-4886-9bda-c4670a86c0e9");

            Assert.NotNull(result);
            Assert.Equal("Recipe 1", result.Name);
        }
    }

    [Fact]
    public void AddOneRecipe_AddsOneRecipe()
    {
        using (var context = new RecipesContext(_options))
        {
            var service = new RecipeService(context);

            var recipe = new Recipe
            {
                Id = "1dcfecc3-d182-4ad2-93c8-7f6ba933a792",
                Name = "Recipe 11",
                Description = "Description 11",
            };

            var result = service.AddOneRecipe(recipe);

            var savedRecipe = context.Recipes.FirstOrDefault(r => r.Id == "1dcfecc3-d182-4ad2-93c8-7f6ba933a792");
            Assert.Equal(1, result);
            Assert.NotNull(savedRecipe);
            Assert.Equal("Recipe 11", savedRecipe.Name);
        }
    }

    [Fact]
    public async Task UpdateOneRecipe_UpdatesOneRecipe()
    {

        using (var context = new RecipesContext(_options))
        {
            var service = new RecipeService(context);

            var recipe = new Recipe
            {
                Id = "754b4e09-d25b-4886-9bda-c4670a86c0e9",
                Name = "Recipe U",
                Description = "Description U",
            };

            var result = await service.UpdateOneRecipe(recipe, "754b4e09-d25b-4886-9bda-c4670a86c0e9");
            
            var updatedRecipe = await context.Recipes.FirstOrDefaultAsync(r => r.Id == "754b4e09-d25b-4886-9bda-c4670a86c0e9");
            Assert.Equal(1, result);
            Assert.NotNull(updatedRecipe);
            Assert.Equal("Recipe U", updatedRecipe.Name);
        }
    }

    [Fact]
    public async Task DeleteOneRecipe_DeletesOneRecipe()
    {
        using (var context = new RecipesContext(_options))
        {
            var service = new RecipeService(context);

            var result = await service.DeleteOneRecipe("2f949b6f-6b65-4ef6-8cdf-fa405472f66e");

            var recipes = await service.GetAllRecipes();
            Assert.Equal(9, recipes.Count);
        }
    }

    [Fact]
    public async Task GetAllRecipesPagination_ReturnsPageOneAndTwo()
    {
        using (var context = new RecipesContext(_options))
        {
            var service = new RecipeService(context);

            var recipes1stPage = await service.GetAllRecipesPagination(0, 5);
            var recipes2ndPage = await service.GetAllRecipesPagination(5, 5);
            Assert.Equal("Recipe 10", recipes1stPage[0].Name);
            Assert.Equal("Recipe 5", recipes2ndPage[0].Name);
        }
    }

    [Fact]
    public async Task SearchRecipe_ReturnsValidResults()
    {
        using (var context = new RecipesContext(_options))
        {
            var service = new RecipeService(context);

            var searchOne = await service.SearchRecipes("keyword");
            var searchTwo = await service.SearchRecipes("5");
            var searchThree = await service.SearchRecipes("Recipe");
            var searchFour = await service.SearchRecipes("Test");

            Assert.Empty(searchOne.NameResults);
            Assert.Empty(searchOne.TagsResults);
            Assert.Equal("Recipe 5", searchTwo.NameResults[0].Name);
            Assert.Equal("Recipe 5", searchTwo.TagsResults[0].Name);
            Assert.Equal(10, searchThree.NameResults.Count);
            Assert.Equal(9, searchFour.TagsResults.Count);
        }
    }

    [Fact]
    public async Task GetAmountOfRecipes_GetsValidAmount()
    {
        using (var context = new RecipesContext(_options))
        {
            var service = new RecipeService(context);

            var result = await service.GetAmountOfRecipes("5f144332-12d3-4d55-9d1a-ac3ce1935f14");

            Assert.Equal(6, result);
        }
    }
}