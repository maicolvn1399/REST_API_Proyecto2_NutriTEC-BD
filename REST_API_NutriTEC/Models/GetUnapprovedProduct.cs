using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace REST_API_NutriTEC.Models
{
    [Keyless]
    public class GetUnapprovedProduct
    {
        public int barcode { get; set; }

        public string name { get; set; } = null!;

        public System.Double size { get; set; }

        public System.Double calcium { get; set; }

        public System.Double sodium { get; set; }

        public System.Double carbs { get; set; }

        public System.Double fat { get; set; }

        public System.Double calories { get; set; }

        public System.Double iron { get; set; }

        public System.Double protein { get; set; }
    }
}
