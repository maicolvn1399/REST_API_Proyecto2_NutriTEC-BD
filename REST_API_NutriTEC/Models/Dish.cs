using System;
using System.Collections;
using System.Collections.Generic;

namespace REST_API_NutriTEC.Models;

public partial class Dish
{
    public int Barcode { get; set; }

    public string DishName { get; set; } = null!;

    public int? DishSize { get; set; }

    public double? Calcium { get; set; }

    public double? Sodium { get; set; }

    public double? Carbs { get; set; }

    public double? Fat { get; set; }

    public int? Calories { get; set; }

    public double? Iron { get; set; }

    public double? Protein { get; set; }

    public BitArray? Active { get; set; }
}
