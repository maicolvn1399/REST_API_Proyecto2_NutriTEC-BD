namespace REST_API_NutriTEC.Models
{
    public class NewNutritionist
    {

        public string id { get; set; } = string.Empty;

        public string name { get; set; } = string.Empty;

        public string lastname_1 { get; set; } = string.Empty;

        public string lastname_2 { get; set; } = string.Empty;

        public string email { get; set; } = string.Empty;

        public string password { get; set; } = string.Empty;

        public string birth_date { get; set; }

        public System.Double weight { get; set; }

        public System.Double height { get; set; }

        public string photo { get; set; } = string.Empty;

        public string credit_card { get; set; } = string.Empty;

        public string payment_type { get; set; } = string.Empty;

        public string address { get; set; } = string.Empty;
    }
}
