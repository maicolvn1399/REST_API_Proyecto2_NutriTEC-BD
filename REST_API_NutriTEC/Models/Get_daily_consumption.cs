using Microsoft.EntityFrameworkCore;

namespace REST_API_NutriTEC.Models
{
    [Keyless]
    public class Get_daily_consumption
    {
        public string dish_name { get; set; } = string.Empty;

        public string date { get; set; } = string.Empty;    

        public string food_time { get; set; } = string.Empty; 

        public int serving { get; set; } = 0;
    }
}
