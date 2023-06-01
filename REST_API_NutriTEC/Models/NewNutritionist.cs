namespace REST_API_NutriTEC.Models
{
    public class NewNutritionist
    {

        public string NutritionistId { get; set; } = string.Empty;

        public string NutritionistName { get; set; } = string.Empty;

        public string Lastname1 { get; set; } = string.Empty;

        public string Lastname2 { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string Photo { get; set; } = string.Empty;

        public string CreditCard { get; set; } = string.Empty;

        public System.Double Weight { get; set; }

        public System.Double Height { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Pass { get; set; } = string.Empty;

        public string BirthDate { get; set; }

        public string BillingId { get; set; } = string.Empty;

        public string RoleId { get; set; } = string.Empty;
    }
}
