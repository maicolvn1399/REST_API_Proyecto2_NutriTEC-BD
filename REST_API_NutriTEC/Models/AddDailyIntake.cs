using Microsoft.EntityFrameworkCore;

namespace REST_API_NutriTEC.Models
{
    [Keyless]
    public class AddDailyIntake
    {
        public int add_daily_intake { get; set; } = 0;
    }
}
