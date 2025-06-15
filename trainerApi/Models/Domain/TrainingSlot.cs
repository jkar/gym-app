using System;
using System.Collections.Generic;

namespace trainerApi.Models.Domain;

public partial class TrainingSlot
{
    public int Id { get; set; }

    public DateTime DateTime { get; set; }

    public int MaximumNumberOfCustomers { get; set; }

    public decimal CostPerCustomer { get; set; }

    public virtual ICollection<TrainingSlotCustomer> TrainingSlotCustomers { get; set; } = new List<TrainingSlotCustomer>();
}
