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

        public async Task<List<CustomerDTO>> GetAllAsync()

        {
            var customers = dbContext.Customers
                .AsNoTracking()
                .AsQueryable()
                .Include(c => c.City)
                .Include(c => c.EducationalLevel)
                .Include(c => c.HealthProblems)
                .ThenInclude(hp => hp.HealthProblemType)
                .Select(c => new CustomerDTO
                {
                    FirstName = c.Firstname,
                    LastName = c.Lastname,
                    CityName = c.City.Name,
                    EdLevel = c.EducationalLevel.Level,
                    HealthProblems = (ICollection<HealthProblemDTO>)c.HealthProblems.Select(hp => new HealthProblemDTO
                    {
                        Description = hp.Description,
                        HealthProblemType = hp.HealthProblemType.Type
                    })
                });

            return await customers.ToListAsync();
        }
    }
}
