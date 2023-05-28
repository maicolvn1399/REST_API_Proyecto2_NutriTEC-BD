using System;
using System.Collections.Generic;

namespace REST_API_NutriTEC.Models;

public partial class Dishxplan
{
    public string? PlanId { get; set; }

    public int? DishId { get; set; }

    public string? FoodTime { get; set; }

    public virtual Dish? Dish { get; set; }

    public virtual Plan? Plan { get; set; }
}
