using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneyFlow.Services.Model.Customer;
using MoneyFlow.Services.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginForm request)
        {
            try
            {
                var result = await _customerService.Login(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpGet("tokens")]
        public async Task<IActionResult> GetCustomerByToken()
        {
            try
            {
                var userIdClaim = User.Claims.FirstOrDefault(c =>c.Type == JwtRegisteredClaimNames.NameId || 
                    c.Type == "nameid" || c.Type == ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return Unauthorized("Token does not contain user ID.");
                } 
                int customerId = int.Parse(userIdClaim.Value);
                var result = await _customerService.GetCustomerById(customerId);
                return Ok(result);
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
