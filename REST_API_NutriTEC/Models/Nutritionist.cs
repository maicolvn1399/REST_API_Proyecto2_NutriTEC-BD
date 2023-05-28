using System;
using System.Collections.Generic;

namespace REST_API_NutriTEC.Models;

public partial class Nutritionist
{
    public string NutriCode { get; set; } = null!;

    public string? NutritionistId { get; set; }

    public string? NutritionistName { get; set; }

    public string? Lastname1 { get; set; }

    public string? Lastname2 { get; set; }

    public string? Address { get; set; }

    public string? Photo { get; set; }

    public string? CreditCard { get; set; }

    public double? Weight { get; set; }

    public double? Height { get; set; }

    public string? Email { get; set; }

    public string? Pass { get; set; }

    public DateOnly? BirthDate { get; set; }

    public int? BillingId { get; set; }

    public int? RoleId { get; set; }

    public virtual Billing? Billing { get; set; }

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

    public virtual ICollection<Plan> Plans { get; set; } = new List<Plan>();

    public virtual Role? Role { get; set; }
}
