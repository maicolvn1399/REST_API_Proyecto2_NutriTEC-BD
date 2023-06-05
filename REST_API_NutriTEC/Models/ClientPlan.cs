using Microsoft.EntityFrameworkCore;

namespace REST_API_NutriTEC.Models
{
    [Keyless]
    public class ClientPlan
    {
        public string food_time { get; set; } = string.Empty;
        public System.Double serving { get; set; } 
        public string dish_name { get; set; } = string.Empty;
    }
}
