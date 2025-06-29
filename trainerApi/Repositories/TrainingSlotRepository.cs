using Microsoft.EntityFrameworkCore;
using trainerApi.Data;
using trainerApi.Models.Domain;
using trainerApi.Models.DTO;
using trainerApi.Repositories.Interfaces;

namespace trainerApi.Repositories
{
    public class TrainingSlotRepository: ITrainingSlotRepository
    {
        private readonly TrainerDbContext trainerDbContext;

        public TrainingSlotRepository (TrainerDbContext trainerDbContext)
        {
            this.trainerDbContext = trainerDbContext;
        }

        public async Task<List<TrainingSlotDTO>> GetAllSlotsPaginatedAsync(int page = 1, int pageSize = 3)
        {
            var offset = pageSize * page;
            var customerSlots = trainerDbContext
                                    .TrainingSlots
                                    .AsNoTracking()
                                    .AsQueryable()
                                    .Include(ts => ts.TrainingSlotCustomers)
                                    .Select(ts => new TrainingSlotDTO
                                    {
                                        DateTime = ts.DateTime,
                                        MaximumNumberOfCustomers = ts.MaximumNumberOfCustomers,
                                        CostPerCustomer = ts.CostPerCustomer,
                                        TrainingSlotCustomers = (ICollection<TrainingSlotCustomerDTO>)ts.TrainingSlotCustomers
                                        .Select(ts => new TrainingSlotCustomerDTO
                                        {
                                            Cancellation = ts.Cancellation,
                                            Presence = ts.Presence,
                                            CancellationReason = ts.CancellationReason,
                                            PaymentAmount = ts.PaymentAmount,
                                            Customer = new CustomerDTO
                                            {
                                                FirstName = ts.Customer.Firstname,
                                                LastName = ts.Customer.Lastname,
                                                CityName = ts.Customer.City.Name
                                            }
                                        })
                                    })
                                    .Skip(offset)
                                    .Take(pageSize);
            
            return await customerSlots.ToListAsync();
        }
    }
}
