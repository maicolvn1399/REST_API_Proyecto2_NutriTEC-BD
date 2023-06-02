using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace REST_API_NutriTEC.Models
{
    [Keyless]
    public class LoginAdmin
    {
        public string name { get; set; } = string.Empty;

        public string lastname1 { get; set; } = string.Empty;

        public string lastname2 { get; set; } = string.Empty;

        public string address { get; set; } = string.Empty;

        public string? photo { get; set; } = string.Empty;

        public string rol { get; set; } = string.Empty;

    }
}
