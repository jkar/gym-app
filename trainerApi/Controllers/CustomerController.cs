using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using trainerApi.Repositories.Interfaces;

namespace trainerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerController(ICustomerRepository customerRepository) {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var customers = await this._customerRepository.GetAllAsync();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
