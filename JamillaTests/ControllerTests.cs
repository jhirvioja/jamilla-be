using Microsoft.AspNetCore.Mvc;
using JamillaBackend.Controllers;
using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace ControllerTests;
public class ControllerTests
{
    private readonly Mock<IRecipeService> _recipeServiceMock;
    private readonly RecipesController _controller;

    public ControllerTests()
    {
        _recipeServiceMock = new Mock<IRecipeService>();
        _controller = new RecipesController(_recipeServiceMock.Object);
    }

    [Fact]
    public async Task GetAllRecipesFromUser_ReturnsOk()
    {
        var recipes = new List<Recipe> { new Recipe { Id = Guid.NewGuid().ToString(), Name = "Recipe 1", Description = "Description 1", Date = DateTimeOffset.ParseExact("2023-03-21T11:56:31+00:00", "yyyy-MM-dd'T'HH:mm:sszzz", CultureInfo.InvariantCulture), Tags = "Testablestring" } };
        _recipeServiceMock.Setup(s => s.GetAllRecipes()).ReturnsAsync(recipes);

        IActionResult response = await _controller.GetAllRecipes();

        var result = Assert.IsType<OkObjectResult>(response);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task GetOneRecipe_WithValidUserId_ReturnsOk()
    {
        var validUserId = Guid.NewGuid().ToString();
        var recipe = new Recipe { Id = Guid.NewGuid().ToString(), Name = "Recipe 1", Description = "Description 1", Date = DateTimeOffset.ParseExact("2023-03-21T11:56:31+00:00", "yyyy-MM-dd'T'HH:mm:sszzz", CultureInfo.InvariantCulture), Tags = "Testablestring" };
        _recipeServiceMock.Setup(s => s.GetOneRecipe(validUserId)).ReturnsAsync(recipe);

        IActionResult response = await _controller.GetRecipe(validUserId);

        var result = Assert.IsType<OkObjectResult>(response);
        Assert.Equal(200, result.StatusCode);
    }
}