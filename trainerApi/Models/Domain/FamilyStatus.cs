using System;
using System.Collections.Generic;

namespace trainerApi.Models.Domain;

public partial class FamilyStatus
{
    public int Id { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
