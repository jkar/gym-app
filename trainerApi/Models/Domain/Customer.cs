using System;
using System.Collections.Generic;

namespace trainerApi.Models.Domain;

public partial class Customer
{
    public int Id { get; set; }

    public int ProfessionId { get; set; }

    public int CityId { get; set; }

    public int EducationalLevelId { get; set; }

    public int FamilyStatusId { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Address { get; set; } = null!;

    public DateOnly BirthDate { get; set; }

    public DateOnly RegistrationDate { get; set; }

    public decimal? CustomerBalance { get; set; }

    public virtual City City { get; set; } = null!;

    public virtual EducationalLevel EducationalLevel { get; set; } = null!;

    public virtual FamilyStatus FamilyStatus { get; set; } = null!;

    public virtual ICollection<HealthProblem> HealthProblems { get; set; } = new List<HealthProblem>();

    public virtual ICollection<PersonalMedicalDatum> PersonalMedicalData { get; set; } = new List<PersonalMedicalDatum>();

    public virtual ICollection<PersonalizedProgram> PersonalizedPrograms { get; set; } = new List<PersonalizedProgram>();

    public virtual Profession Profession { get; set; } = null!;

    public virtual ICollection<TrainingSlotCustomer> TrainingSlotCustomers { get; set; } = new List<TrainingSlotCustomer>();
}
