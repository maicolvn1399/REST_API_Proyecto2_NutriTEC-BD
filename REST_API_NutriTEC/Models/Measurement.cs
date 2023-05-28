﻿using System;
using System.Collections.Generic;

namespace REST_API_NutriTEC.Models;

public partial class Measurement
{
    public string? ClientId { get; set; }

    public DateOnly? DateM { get; set; }

    public double? Weight { get; set; }

    public double? Waist { get; set; }

    public double? Neck { get; set; }

    public double? Hip { get; set; }

    public string? MusclePercentage { get; set; }

    public string? FatPercentage { get; set; }

    public virtual Client? Client { get; set; }
}
