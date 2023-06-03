namespace REST_API_NutriTEC.Models
{
    public class PlanAssigner
    {
        public string plan_name { get; set; } = string.Empty;

        public string start_date { get; set; } = string.Empty;

        public string end_date { get; set;} = string.Empty;

        public string email_client { get; set; } = string.Empty;
    }
}
