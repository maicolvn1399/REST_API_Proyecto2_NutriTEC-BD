using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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

    public DateTime? BirthDate { get; set; }

    public int? BillingId { get; set; }

    public int? RoleId { get; set; }
    [NotMapped]
    public virtual Billing? Billing { get; set; }

    [NotMapped]
    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

    [NotMapped]
    public virtual ICollection<Plan> Plans { get; set; } = new List<Plan>();
    [NotMapped]
    public virtual Role? Role { get; set; }
}
