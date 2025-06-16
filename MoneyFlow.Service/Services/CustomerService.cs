using Microsoft.IdentityModel.Tokens;
using MoneyFlow.Repositories.Base;
using MoneyFlow.Repositories.Models;
using MoneyFlow.Services.Base;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ErrorHanding;
using MoneyFlow.Payload.Request;
using MoneyFlow.Payload.Response;

namespace MoneyFlow.Services.Services
{
    public interface ICustomerService
    {
        Task<IBusinessResult> GetAllCustomer();
        Task<LoginResponse> Login(LoginRequest request);
        Task<IBusinessResult> GetCustomerById(int Id);
    }
    
}
