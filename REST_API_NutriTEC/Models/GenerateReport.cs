using Microsoft.EntityFrameworkCore;

namespace REST_API_NutriTEC.Models
{
    [Keyless]
    public class GenerateReport
    {
        public DateTime date { get; set; }
        public System.Double weight { get; set; }
        public System.Double waist { get; set;}
        public System.Double hip { get; set; }
        public string muscle_percentage { get; set; } = string.Empty;
        public string fat_percentage { get; set; } = string.Empty;
    }
}
