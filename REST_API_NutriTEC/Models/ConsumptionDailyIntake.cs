namespace REST_API_NutriTEC.Models
{
    public class ConsumptionDailyIntake
    {
        public string dish_name { get; set; } = string.Empty;

        public string food_time { get; set; } = string.Empty;
        public int serving { get; set; } = 0;
    }
}
