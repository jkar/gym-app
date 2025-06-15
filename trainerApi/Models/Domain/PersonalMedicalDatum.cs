using System;
using System.Collections.Generic;

namespace trainerApi.Models.Domain;

public partial class PersonalMedicalDatum
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int AnnualQuartersId { get; set; }

    public decimal FatMassPercentage { get; set; }

    public decimal BodyMassIndex { get; set; }

    public int CardiorespiratoryFunction { get; set; }

    public int MusculoskeletalFunction { get; set; }

    public int FlexibilityLevel { get; set; }

    public virtual AnnualQuarter AnnualQuarters { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;
}
