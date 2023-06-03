using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace REST_API_NutriTEC.Models
{
    [Keyless]
    public class Calculate_billing
    {
        public string billing_type { get; set; } = string.Empty;
        public string nutri_email { get; set; } = string.Empty;
        public string nutri_fullname { get; set; } = string.Empty;
        public string credit_card { get; set; } = string.Empty;
        public System.Double total { get; set;}
        public System.Double discount { get; set; }
        public System.Double payment { get; set; }
    }
}
