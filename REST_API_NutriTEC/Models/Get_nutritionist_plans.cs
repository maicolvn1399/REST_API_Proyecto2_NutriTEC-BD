using Microsoft.EntityFrameworkCore;

namespace REST_API_NutriTEC.Models
{
    [Keyless]
    public class Get_nutritionist_plans
    {
        public string plan_name { get; set; } = string.Empty;
    }
}
