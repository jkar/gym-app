using trainerApi.Models.Domain;
using trainerApi.Models.DTO;

namespace trainerApi.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<CustomerDTO>> GetAllAsync();
    }
}
