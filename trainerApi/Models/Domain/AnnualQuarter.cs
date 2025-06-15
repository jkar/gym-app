using System;
using System.Collections.Generic;

namespace trainerApi.Models.Domain;

public partial class AnnualQuarter
{
    public int Id { get; set; }

    public DateOnly MassRegistrationDate { get; set; }

    public virtual ICollection<PersonalMedicalDatum> PersonalMedicalData { get; set; } = new List<PersonalMedicalDatum>();
}
