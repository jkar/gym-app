using trainerApi.Models.Domain;

namespace trainerApi.Models.DTO
{
    public class TrainingSlotDTO
    {
        public DateTime DateTime { get; set; }
        public int MaximumNumberOfCustomers { get; set; }
        public decimal CostPerCustomer { get; set; }
        public ICollection<TrainingSlotCustomerDTO>? TrainingSlotCustomers { get; set; }
    }
}
