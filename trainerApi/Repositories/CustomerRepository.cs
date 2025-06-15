using Microsoft.EntityFrameworkCore;
using trainerApi.Data;
using trainerApi.Models.Domain;
using trainerApi.Models.DTO;
using trainerApi.Repositories.Interfaces;

namespace trainerApi.Repositories
{
    public class CustomerRepository: ICustomerRepository
    {
        private readonly TrainerDbContext dbContext;
        public CustomerRepository(TrainerDbContext dbContext) {
            this.dbContext = dbContext;
        }

        //SELECT c.firstname, c.lastname, ts.date_time, ts.cost_per_customer
        //    FROM public.customer as c
        //    left outer join public.training_slot_customer as tsc
        //    on c.id = tsc.customer_id
        //    left outer join public.training_slot as ts
        //    on tsc.training_slot_id = ts.id
        //    where ts.date_time between '2024/04/16' and '2024/05/27';
        public async Task<List<CustomerDTO>> GetAllAsync()

        {
            DateTime startDate = new DateTime(2024, 04, 16);
            DateTime endDate = new DateTime(2024, 05, 27);
            var customers = dbContext.Customers
                .AsNoTracking()
                .AsQueryable()
                .Include(c => c.City)
                .Include(c => c.EducationalLevel)
                .Include(c => c.HealthProblems)
                .ThenInclude(hp => hp.HealthProblemType)
                .Include(c => c.TrainingSlotCustomers)
                .ThenInclude(tsc => tsc.TrainingSlot)
                .Where(c => c.TrainingSlotCustomers
                    .Any(tsc => tsc.TrainingSlot != null &&
                                tsc.TrainingSlot.DateTime >= startDate &&
                                tsc.TrainingSlot.DateTime <= endDate))
                .Select(c => new CustomerDTO
                {
                    FirstName = c.Firstname,
                    LastName = c.Lastname,
                    CityName = c.City.Name,
                    EdLevel = c.EducationalLevel.Level,
                    HealthProblems = (ICollection<HealthProblemDTO>)c.HealthProblems
                        .Select(hp => new HealthProblemDTO
                        {
                            Description = hp.Description,
                            HealthProblemType = hp.HealthProblemType.Type
                        }),
                    TrainingSlots = (ICollection<TrainingSlotDTO>)c.TrainingSlotCustomers
                        .Where(tsc => tsc.TrainingSlot != null &&
                          tsc.TrainingSlot.DateTime >= startDate &&
                          tsc.TrainingSlot.DateTime <= endDate)
                        .Select(ts => new TrainingSlotDTO
                        {
                            DateTime = ts.TrainingSlot.DateTime,
                            MaximumNumberOfCustomers = ts.TrainingSlot.MaximumNumberOfCustomers,
                            CostPerCustomer = ts.TrainingSlot.CostPerCustomer
                        })
                });

            return await customers.ToListAsync();
        }
    }
}
