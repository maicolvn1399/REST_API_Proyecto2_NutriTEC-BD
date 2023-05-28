using System;
using System.Collections.Generic;

namespace REST_API_NutriTEC.Models;

public partial class Productxdish
{
    public string? RecipeName { get; set; }

    public int? ProductServing { get; set; }

    public int? ProductId { get; set; }

    public virtual Dish? Product { get; set; }

    public virtual Dish? RecipeNameNavigation { get; set; }
}
