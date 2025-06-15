using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using trainerApi.Models.Domain;

namespace trainerApi.Data;

public partial class TrainerDbContext : DbContext
{
    public TrainerDbContext()
    {
    }

    public TrainerDbContext(DbContextOptions<TrainerDbContext> dbContextOptions) : base(dbContextOptions)
    {

    }


    public virtual DbSet<AnnualQuarter> AnnualQuarters { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<EducationalLevel> EducationalLevels { get; set; }

    public virtual DbSet<Exercise> Exercises { get; set; }

    public virtual DbSet<FamilyStatus> FamilyStatuses { get; set; }

    public virtual DbSet<HealthProblem> HealthProblems { get; set; }

    public virtual DbSet<HealthProblemType> HealthProblemTypes { get; set; }

    public virtual DbSet<Instruction> Instructions { get; set; }

    public virtual DbSet<PersonalMedicalDatum> PersonalMedicalData { get; set; }

    public virtual DbSet<PersonalizedProgram> PersonalizedPrograms { get; set; }

    public virtual DbSet<PersonalizedProgramExercise> PersonalizedProgramExercises { get; set; }

    public virtual DbSet<Profession> Professions { get; set; }

    public virtual DbSet<ProgramType> ProgramTypes { get; set; }

    public virtual DbSet<TrainingSlot> TrainingSlots { get; set; }

    public virtual DbSet<TrainingSlotCustomer> TrainingSlotCustomers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=trainer;Username=postgres;Password=1234");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnnualQuarter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("annual_quarters_pkey");

            entity.ToTable("annual_quarters");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.MassRegistrationDate).HasColumnName("mass_registration_date");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("city_pkey");

            entity.ToTable("city");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("customer_pkey");

            entity.ToTable("customer");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(80)
                .HasColumnName("address");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.CityId).HasColumnName("city_id");
            entity.Property(e => e.CustomerBalance)
                .HasPrecision(19, 4)
                .HasDefaultValueSql("0")
                .HasColumnName("customer_balance");
            entity.Property(e => e.EducationalLevelId).HasColumnName("educational_level_id");
            entity.Property(e => e.FamilyStatusId).HasColumnName("family_status_id");
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .HasColumnName("lastname");
            entity.Property(e => e.ProfessionId).HasColumnName("profession_id");
            entity.Property(e => e.RegistrationDate).HasColumnName("registration_date");

            entity.HasOne(d => d.City).WithMany(p => p.Customers)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("customer_city_id_fkey");

            entity.HasOne(d => d.EducationalLevel).WithMany(p => p.Customers)
                .HasForeignKey(d => d.EducationalLevelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("customer_educational_level_id_fkey");

            entity.HasOne(d => d.FamilyStatus).WithMany(p => p.Customers)
                .HasForeignKey(d => d.FamilyStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("customer_family_status_id_fkey");

            entity.HasOne(d => d.Profession).WithMany(p => p.Customers)
                .HasForeignKey(d => d.ProfessionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("customer_profession_id_fkey");
        });

        modelBuilder.Entity<EducationalLevel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("educational_level_pkey");

            entity.ToTable("educational_level");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Level)
                .HasMaxLength(50)
                .HasColumnName("level");
        });

        modelBuilder.Entity<Exercise>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("exercise_pkey");

            entity.ToTable("exercise");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(80)
                .HasColumnName("description");
        });

        modelBuilder.Entity<FamilyStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("family_status_pkey");

            entity.ToTable("family_status");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Status)
                .HasMaxLength(30)
                .HasColumnName("status");
        });

        modelBuilder.Entity<HealthProblem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("health_problem_pkey");

            entity.ToTable("health_problem");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Description)
                .HasMaxLength(150)
                .HasColumnName("description");
            entity.Property(e => e.HealthProblemTypeId).HasColumnName("health_problem_type_id");

            entity.HasOne(d => d.Customer).WithMany(p => p.HealthProblems)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("health_problem_customer_id_fkey");

            entity.HasOne(d => d.HealthProblemType).WithMany(p => p.HealthProblems)
                .HasForeignKey(d => d.HealthProblemTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("health_problem_health_problem_type_id_fkey");
        });

        modelBuilder.Entity<HealthProblemType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("health_problem_type_pkey");

            entity.ToTable("health_problem_type");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Type)
                .HasMaxLength(80)
                .HasColumnName("type");
        });

        modelBuilder.Entity<Instruction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("instruction_pkey");

            entity.ToTable("instruction");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.PersonalisedProgramExerciseId).HasColumnName("personalised_program_exercise_id");

            entity.HasOne(d => d.PersonalisedProgramExercise).WithMany(p => p.Instructions)
                .HasForeignKey(d => d.PersonalisedProgramExerciseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("instruction_personalised_program_exercise_id_fkey");
        });

        modelBuilder.Entity<PersonalMedicalDatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("personal_medical_data_pkey");

            entity.ToTable("personal_medical_data");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AnnualQuartersId).HasColumnName("annual_quarters_id");
            entity.Property(e => e.BodyMassIndex)
                .HasPrecision(8, 2)
                .HasColumnName("body_mass_index");
            entity.Property(e => e.CardiorespiratoryFunction).HasColumnName("cardiorespiratory_function");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.FatMassPercentage)
                .HasPrecision(8, 2)
                .HasColumnName("fat_mass_percentage");
            entity.Property(e => e.FlexibilityLevel).HasColumnName("flexibility_level");
            entity.Property(e => e.MusculoskeletalFunction).HasColumnName("musculoskeletal_function");

            entity.HasOne(d => d.AnnualQuarters).WithMany(p => p.PersonalMedicalData)
                .HasForeignKey(d => d.AnnualQuartersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("personal_medical_data_annual_quarters_id_fkey");

            entity.HasOne(d => d.Customer).WithMany(p => p.PersonalMedicalData)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("personal_medical_data_customer_id_fkey");
        });

        modelBuilder.Entity<PersonalizedProgram>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("personalized_program_pkey");

            entity.ToTable("personalized_program");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.ProgramTypeId).HasColumnName("program_type_id");
            entity.Property(e => e.StartDate).HasColumnName("start_date");

            entity.HasOne(d => d.Customer).WithMany(p => p.PersonalizedPrograms)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("personalized_program_customer_id_fkey");

            entity.HasOne(d => d.ProgramType).WithMany(p => p.PersonalizedPrograms)
                .HasForeignKey(d => d.ProgramTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("personalized_program_program_type_id_fkey");
        });

        modelBuilder.Entity<PersonalizedProgramExercise>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("personalized_program_exercise_pkey");

            entity.ToTable("personalized_program_exercise");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ExerciseId).HasColumnName("exercise_id");
            entity.Property(e => e.PersonalisedProgramId).HasColumnName("personalised_program_id");

            entity.HasOne(d => d.Exercise).WithMany(p => p.PersonalizedProgramExercises)
                .HasForeignKey(d => d.ExerciseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("personalized_program_exercise_exercise_id_fkey");

            entity.HasOne(d => d.PersonalisedProgram).WithMany(p => p.PersonalizedProgramExercises)
                .HasForeignKey(d => d.PersonalisedProgramId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("personalized_program_exercise_personalised_program_id_fkey");
        });

        modelBuilder.Entity<Profession>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("profession_pkey");

            entity.ToTable("profession");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(30)
                .HasColumnName("description");
        });

        modelBuilder.Entity<ProgramType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("program_type_pkey");

            entity.ToTable("program_type");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Type)
                .HasMaxLength(80)
                .HasColumnName("type");
        });

        modelBuilder.Entity<TrainingSlot>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("training_slot_pkey");

            entity.ToTable("training_slot");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CostPerCustomer)
                .HasPrecision(19, 4)
                .HasColumnName("cost_per_customer");
            entity.Property(e => e.DateTime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_time");
            entity.Property(e => e.MaximumNumberOfCustomers).HasColumnName("maximum_number_of_customers");
        });

        modelBuilder.Entity<TrainingSlotCustomer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("training_slot_customer_pkey");

            entity.ToTable("training_slot_customer");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cancellation)
                .HasDefaultValue(false)
                .HasColumnName("cancellation");
            entity.Property(e => e.CancellationReason)
                .HasMaxLength(150)
                .HasColumnName("cancellation_reason");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.PaymentAmount)
                .HasPrecision(19, 4)
                .HasDefaultValueSql("0")
                .HasColumnName("payment_amount");
            entity.Property(e => e.Presence)
                .HasDefaultValue(false)
                .HasColumnName("presence");
            entity.Property(e => e.TrainingSlotId).HasColumnName("training_slot_id");

            entity.HasOne(d => d.Customer).WithMany(p => p.TrainingSlotCustomers)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("training_slot_customer_customer_id_fkey");

            entity.HasOne(d => d.TrainingSlot).WithMany(p => p.TrainingSlotCustomers)
                .HasForeignKey(d => d.TrainingSlotId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("training_slot_customer_training_slot_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
