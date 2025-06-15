using System;
using System.Collections.Generic;

namespace trainerApi.Models.Domain;

public partial class PersonalizedProgram
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int ProgramTypeId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<PersonalizedProgramExercise> PersonalizedProgramExercises { get; set; } = new List<PersonalizedProgramExercise>();

    public virtual ProgramType ProgramType { get; set; } = null!;
}
