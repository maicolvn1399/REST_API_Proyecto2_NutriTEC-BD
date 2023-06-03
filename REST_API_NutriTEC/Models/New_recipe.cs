namespace REST_API_NutriTEC.Models
{
    public class New_recipe
    {
        public string dish_name { get; set; } = string.Empty;

        public List<Recipe_product> ingredients { get; set; }
    }
}
