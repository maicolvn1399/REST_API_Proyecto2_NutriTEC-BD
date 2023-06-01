using Microsoft.EntityFrameworkCore;

namespace REST_API_NutriTEC.Models
{
    [Keyless]
    public class LoginNutritionist
    {
        public string nutri_code { get; set; } = string.Empty;
        public string nutritionist_id { get; set; } = string.Empty;
        public string nutritionist_name { get; set; } = string.Empty;
        public string lastname1 { get; set; } = string.Empty;
        public string lastname2 { get; set; } = string.Empty; 
        public string address { get; set; } = string.Empty;
        public string photo { get; set; } = string.Empty;   
        public string credit_card { get; set; } = string.Empty;
        public System.Double weight { get; set; }
        public System.Double height { get; set;}
        public string email { get; set; } = string.Empty;
        public string pass { get; set; } = string.Empty;
        public DateTime birth_date { get; set; }
        public string billing_type { get; set; } = string.Empty;
        public string role_name { get; set; } = string.Empty;   

    }
}
