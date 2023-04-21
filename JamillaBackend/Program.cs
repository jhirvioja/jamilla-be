using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using JamillaBackend.Models;
using JamillaBackend.Services;

var builder = WebApplication.CreateBuilder(args);
var DbKey = builder.Configuration["JamillaDb:PrimaryKey"];

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000",
                "https://localhost:3000",
                "http://jamilla.azurewebsites.net/," +
                "https://jamilla.azurewebsites.net/")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddDbContext<RecipesContext>(options =>
    options.UseCosmos(
        "https://cosno-jamilla-weu.documents.azure.com:443",
        DbKey,
        databaseName: "JamillaDB"));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "JamillaRecipes API",
        Description = "Contains Recipes for the Jamilla App",
        Version = "v1"
    });
    var filePath = Path.Combine(AppContext.BaseDirectory, "JamillaBackend.xml");
    c.IncludeXmlComments(filePath);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowSpecificOrigins");

app.Run();
