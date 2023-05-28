using System;
using System.Collections.Generic;

namespace REST_API_NutriTEC.Models;

public partial class Clientxplan
{
    public string? ClientId { get; set; }

    public string? PlanName { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public virtual Client? Client { get; set; }

    public virtual Plan? PlanNameNavigation { get; set; }
}
