using System;
using System.Collections.Generic;

namespace REST_API_NutriTEC.Models;

public partial class Billing
{
    public int BillingId { get; set; }

    public string? BillingType { get; set; }

    public virtual ICollection<Nutritionist> Nutritionists { get; set; } = new List<Nutritionist>();
}
