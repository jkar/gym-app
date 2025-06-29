using Microsoft.EntityFrameworkCore;
using System;
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

        //SELECT
        //    c.id AS customer_id,
        //    c.firstname,
        //    c.lastname,
        //    city.name AS city_name,
        //    el.level AS educational_level,

        //    hp.description AS health_problem_description,
        //    hpt.type AS health_problem_type,

        //    ts.date_time AS training_slot_datetime,
        //    ts.maximum_number_of_customers,
        //    ts.cost_per_customer

        //FROM customer c

        //// Join related tables
        //LEFT JOIN city ON c.city_id = city.id
        //LEFT JOIN educational_level el ON c.educational_level_id = el.id

        //// Health Problems
        //LEFT JOIN health_problem hp ON c.id = hp.customer_id
        //LEFT JOIN health_problem_type hpt ON hp.health_problem_type_id = hpt.id

        //// Training Slot Info
        //LEFT JOIN training_slot_customer tsc ON c.id = tsc.customer_id
        //LEFT JOIN training_slot ts ON tsc.training_slot_id = ts.id

        //// Filter only those with training slot within date range
        //WHERE ts.date_time BETWEEN '2024-04-01' AND '2024-06-01';
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
