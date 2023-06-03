using Microsoft.EntityFrameworkCore;


namespace REST_API_NutriTEC.Models
{
    [Keyless]
    public class searchclient
    {
        public string client_name { get; set; } = string.Empty;

        public string email { get; set; } = string.Empty;
    }
}
