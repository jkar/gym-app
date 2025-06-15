using System;
using System.Collections.Generic;

namespace trainerApi.Models.Domain;

public partial class TrainingSlotCustomer
{
    public int Id { get; set; }

    public int TrainingSlotId { get; set; }

    public int CustomerId { get; set; }

    public bool? Cancellation { get; set; }

    public bool? Presence { get; set; }

    public string? CancellationReason { get; set; }

    public decimal? PaymentAmount { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual TrainingSlot TrainingSlot { get; set; } = null!;
}
