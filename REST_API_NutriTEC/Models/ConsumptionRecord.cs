using System;
using System.Collections.Generic;

namespace REST_API_NutriTEC.Models;

public partial class ConsumptionRecord
{
    public string? ClientId { get; set; }

    public string? DishName { get; set; }

    public DateOnly? DateC { get; set; }

    public string? FoodTime { get; set; }

    public int? Serving { get; set; }

    public virtual Client? Client { get; set; }

    public virtual Dish? DishNameNavigation { get; set; }
}
