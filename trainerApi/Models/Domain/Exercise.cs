using System;
using System.Collections.Generic;

namespace trainerApi.Models.Domain;

public partial class Exercise
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<PersonalizedProgramExercise> PersonalizedProgramExercises { get; set; } = new List<PersonalizedProgramExercise>();
}
