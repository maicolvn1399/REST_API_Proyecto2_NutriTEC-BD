using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace REST_API_NutriTEC.Models
{
    [Keyless]
    public class LoginClient
    {
        public string email { get; set; } = string.Empty;

        public string name { get; set; } = string.Empty;
        public string lastname1 { get; set; } = string.Empty;
        public string lastname2 { get; set; } = string.Empty;
        public string country { get; set; } = string.Empty;

        public System.Double height { get; set; }

        public System.Double weight { get; set; }
        public int calorie { get; set; }
        public DateTime birth_date { get; set; }
        public int age { get; set; }

        public System.Double bmi { get; set; }
    }
}
