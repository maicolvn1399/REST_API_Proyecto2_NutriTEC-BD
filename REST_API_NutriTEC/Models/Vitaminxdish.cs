using System;
using System.Collections.Generic;

namespace REST_API_NutriTEC.Models;

public partial class Vitaminxdish
{
    public int? DishId { get; set; }

    public string? Vitamin { get; set; }

    public virtual Dish? Dish { get; set; }

    public virtual Vitamin? VitaminNavigation { get; set; }
}
