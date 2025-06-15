using System;
using System.Collections.Generic;

namespace trainerApi.Models.Domain;

public partial class HealthProblemType
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<HealthProblem> HealthProblems { get; set; } = new List<HealthProblem>();
}
