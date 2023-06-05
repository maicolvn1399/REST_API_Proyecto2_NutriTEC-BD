namespace REST_API_NutriTEC.Models
{
    public class NewClient
    {
        public string name { get; set; } = string.Empty;
        public string lastname1 { get; set; } = string.Empty;
        public string lastname2 { get; set; } = string.Empty;
        public string birth_date { get; set; } = string.Empty;
        public System.Double weight { get; set; }
        public System.Double height { get; set; } 
        public string email { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string country { get; set; } = string.Empty;
        public int calorie_goal { get; set; } = 0;
    }
}
