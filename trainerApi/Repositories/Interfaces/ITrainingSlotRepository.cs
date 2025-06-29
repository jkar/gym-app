using trainerApi.Models.DTO;

namespace trainerApi.Repositories.Interfaces
{
    public interface ITrainingSlotRepository
    {
        Task<List<TrainingSlotDTO>> GetAllSlotsPaginatedAsync(int page = 1, int pageSize = 3);
    }
}
