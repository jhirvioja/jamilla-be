using Microsoft.EntityFrameworkCore;

namespace JamillaBackend.Models
{
    public class RecipesContext : DbContext
    {
        public RecipesContext(DbContextOptions<RecipesContext> options)
            : base(options)
        {
        }

        public DbSet<Recipe?> Recipes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Recipe>()
                .ToContainer("Recipes");

            modelBuilder.Entity<Recipe>()
                .Property(r => r.Id)
                .ToJsonProperty("id");

            modelBuilder.Entity<Recipe>()
                .Property(r => r.UserId)
                .ToJsonProperty("userid");

            modelBuilder.Entity<Recipe>()
                .Property(r => r.Date)
                .ToJsonProperty("date");

            modelBuilder.Entity<Recipe>()
                .Property(r => r.PrepTime)
                .ToJsonProperty("preptime");

            modelBuilder.Entity<Recipe>()
                .Property(r => r.Cost)
                .ToJsonProperty("cost");

            modelBuilder.Entity<Recipe>()
                .Property(r => r.Name)
                .ToJsonProperty("name");

            modelBuilder.Entity<Recipe>()
                .Property(r => r.ImgSrc)
                .ToJsonProperty("imgsrc");

            modelBuilder.Entity<Recipe>()
                .Property(r => r.Description)
                .ToJsonProperty("description");

            modelBuilder.Entity<Recipe>()
                .Property(r => r.Tags)
                .ToJsonProperty("tags");

            modelBuilder.Entity<Recipe>().OwnsMany(
                r => r.RecipeIngredients,
                ri =>
                {
                    ri.ToJsonProperty("recipeingredients");
                    ri.Property(p => p.Stock).ToJsonProperty("stock");
                    ri.Property(p => p.Name).ToJsonProperty("name");
                    ri.Property(p => p.AmountValue).ToJsonProperty("amountvalue");
                    ri.Property(p => p.AmountUnit).ToJsonProperty("amountunit");
                });

            modelBuilder.Entity<Recipe>().OwnsMany(
                r => r.Steps,
                ri =>
                {
                    ri.ToJsonProperty("steps");
                    ri.Property(p => p.Part).ToJsonProperty("part");
                    ri.Property(p => p.Description).ToJsonProperty("description");
                    ri.Property(p => p.Steplast).ToJsonProperty("steplast");
                });

            modelBuilder.Entity<Recipe>()
                .HasNoDiscriminator();

            modelBuilder.Entity<Recipe>()
                .HasPartitionKey(r => r.UserId);

        }
    }
}
