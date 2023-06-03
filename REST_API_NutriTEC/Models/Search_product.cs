using Microsoft.EntityFrameworkCore;

namespace REST_API_NutriTEC.Models
{
    [Keyless]
    public class Search_product
    {
        public string product_name { get; set; } = string.Empty;

        public int barcode { get; set; } = 0;
    }
}
