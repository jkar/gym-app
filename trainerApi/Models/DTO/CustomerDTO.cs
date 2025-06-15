namespace trainerApi.Models.DTO
{
    public class CustomerDTO
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string CityName { get; set; } = null;
        public string EdLevel { get; set; } = null;
        public ICollection<HealthProblemDTO> HealthProblems { get; set; }
    }
}
