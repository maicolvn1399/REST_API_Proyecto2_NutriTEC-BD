using System.Collections;
using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;

namespace REST_API_NutriTEC.Models
{
    public class AddProduct
    {
        public string name { get; set; } = null!;

        public int? size { get; set; } = 0;

        public int? calories { get; set; } = 0;

        public double? fat { get; set; } = 0;

        public double? sodium { get; set; } = 0;

        public double? carbs { get; set; } = 0;

        public double? protein { get; set; } = 0;

        public double? calcium { get; set; } = 0;

        public double? iron { get; set; } = 0;

        public List<string>? vitamins { get; set; }
    }
}
