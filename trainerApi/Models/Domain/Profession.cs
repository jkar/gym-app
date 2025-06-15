using System;
using System.Collections.Generic;

namespace trainerApi.Models.Domain;

public partial class Profession
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
