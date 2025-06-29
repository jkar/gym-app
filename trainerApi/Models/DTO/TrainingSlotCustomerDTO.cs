using trainerApi.Models.Domain;

namespace trainerApi.Models.DTO
{
    public class TrainingSlotCustomerDTO
    {
        public bool? Cancellation { get; set; }

        public bool? Presence { get; set; }

        public string? CancellationReason { get; set; }

        public decimal? PaymentAmount { get; set; }

        public CustomerDTO? Customer { get; set; }
    }
}
