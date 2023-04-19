namespace JamillaBackend.Models
{
    public class Recipe
    {
        public string? Id { get; set; } = Guid.NewGuid().ToString();
        public string? UserId { get; set; }
        public DateTimeOffset? Date { get; set; } = DateTimeOffset.Now;
        public string? PrepTime { get; set; }
        public int? Cost { get; set; }
        public string? Name { get; set; }
        public string? ImgSrc { get; set; }
        public string? Description { get; set; }
        public string? Tags { get; set; }
        public ICollection<RecipeIngredient>? RecipeIngredients { get; set; }
        public ICollection<Step>? Steps { get; set; }
    }
}
