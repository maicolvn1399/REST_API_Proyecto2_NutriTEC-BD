using System;
using System.Collections.Generic;

namespace REST_API_NutriTEC.Models;

public partial class Plan
{
    public string PlanName { get; set; } = null!;

    public int? TotalCalories { get; set; }

    public string? NutritionistId { get; set; }

    public virtual Nutritionist? Nutritionist { get; set; }
}
