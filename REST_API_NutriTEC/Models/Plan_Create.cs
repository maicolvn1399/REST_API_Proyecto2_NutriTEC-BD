namespace REST_API_NutriTEC.Models
{
    public class Plan_Create
    {
        public string plan_name { get; set; } = string.Empty;

        public string nutritionist_id { get; set; } = string.Empty;

        public List<Plan_dish> plan { get; set;}
    }
}
