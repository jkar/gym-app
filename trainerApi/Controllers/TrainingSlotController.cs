using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using trainerApi.Repositories.Interfaces;

namespace trainerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingSlotController : ControllerBase
    {
        private readonly ITrainingSlotRepository _trainingSlotRepository;

        public TrainingSlotController(ITrainingSlotRepository repository)
        {
            _trainingSlotRepository = repository;
        }

        [HttpGet("getTrainingSlotsPaginated")]
        public async Task<IActionResult> GetTrainingSlotsPaginated([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 3)
        {
            try
            {
                var trainingSlots = await this._trainingSlotRepository.GetAllSlotsPaginatedAsync();
                return Ok(trainingSlots);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
