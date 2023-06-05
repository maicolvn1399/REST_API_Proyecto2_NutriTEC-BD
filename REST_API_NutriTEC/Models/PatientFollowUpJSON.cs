using Microsoft.EntityFrameworkCore;

namespace REST_API_NutriTEC.Models
{
    [Keyless]
    public class PatientFollowUpJSON
    {

        public int serving { get; set; } = 0;
        public string dish_name { get; set; } = string.Empty;

        public string food_time { get; set; } = string.Empty;
    }
}
