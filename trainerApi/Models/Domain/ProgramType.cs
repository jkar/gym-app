using System;
using System.Collections.Generic;

namespace trainerApi.Models.Domain;

public partial class ProgramType
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<PersonalizedProgram> PersonalizedPrograms { get; set; } = new List<PersonalizedProgram>();
}
