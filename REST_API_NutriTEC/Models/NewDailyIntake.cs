namespace REST_API_NutriTEC.Models
{
    public class NewDailyIntake
    {
        public string client_id { get; set; } = string.Empty;
        public string date { get; set; } = string.Empty;
        
        public List<ConsumptionDailyIntake> consumption { get; set; }
    }
}
