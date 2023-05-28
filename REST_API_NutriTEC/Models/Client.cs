using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace REST_API_NutriTEC.Models;

public partial class Client
{
    public string Email { get; set; } = null!;

    public string? ClientName { get; set; }

    public string? Lastname1 { get; set; }

    public string? Lastname2 { get; set; }

    public string? Pass { get; set; }

    public string? Country { get; set; }

    public int? CalorieGoal { get; set; }

    public double? Height { get; set; }

    public double? Weight { get; set; }

    public string? BirthDate { get; set; }
    
    public string? NutritionistId { get; set; }
    [JsonIgnore]
    public virtual Nutritionist? Nutritionist { get; set; }
}
