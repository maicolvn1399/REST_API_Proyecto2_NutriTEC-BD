namespace REST_API_NutriTEC.Models
{
    public class NewMeasurement
    {
        public string client_id { get; set; } = string.Empty;
        public string date { get; set; } = string.Empty;
        public System.Double weight { get; set; }
        public System.Double waist { get; set;}
        public System.Double neck { get; set;}
        public System.Double hip { get; set; }
        public string muscle_percentage { get; set; } = string.Empty;
        public string fat_percentage { get; set;} = string.Empty;
    }
}
