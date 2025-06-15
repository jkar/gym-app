using System;
using System.Collections.Generic;

namespace trainerApi.Models.Domain;

public partial class PersonalizedProgramExercise
{
    public int Id { get; set; }

    public int PersonalisedProgramId { get; set; }

    public int ExerciseId { get; set; }

    public virtual Exercise Exercise { get; set; } = null!;

    public virtual ICollection<Instruction> Instructions { get; set; } = new List<Instruction>();

    public virtual PersonalizedProgram PersonalisedProgram { get; set; } = null!;
}
