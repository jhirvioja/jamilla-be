namespace JamillaBackend.Models
{
    public class RecipeIngredient
    {
        public bool? Stock { get; set; } = false;
        public string? Name { get; set; }
        public string? AmountValue { get; set; }
        public string? AmountUnit { get; set; }
    }
}
