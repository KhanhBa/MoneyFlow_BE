using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneyFlow.Services.Services;

namespace MoneyFlow.APIs.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private ICustomerService _customerService;
        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCustomer()
        {
            try
            {
                var result = await _customerService.GetAllCustomer();
                return Ok(result);
            }
            catch (Exception ex) { 
            return BadRequest(ex.Message);
            }
        }
    }
}
