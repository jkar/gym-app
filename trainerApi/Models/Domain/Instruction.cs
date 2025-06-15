using System;
using System.Collections.Generic;

namespace trainerApi.Models.Domain;

public partial class Instruction
{
    public int Id { get; set; }

    public int PersonalisedProgramExerciseId { get; set; }

    public string Description { get; set; } = null!;

    public virtual PersonalizedProgramExercise PersonalisedProgramExercise { get; set; } = null!;
}
