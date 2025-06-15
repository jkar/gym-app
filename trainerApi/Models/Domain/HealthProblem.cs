using System;
using System.Collections.Generic;

namespace trainerApi.Models.Domain;

public partial class HealthProblem
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int HealthProblemTypeId { get; set; }

    public string Description { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;

    public virtual HealthProblemType HealthProblemType { get; set; } = null!;
}
