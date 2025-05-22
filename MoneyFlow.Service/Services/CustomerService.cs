using Microsoft.IdentityModel.Tokens;
using MoneyFlow.Repositories.Base;
using MoneyFlow.Repositories.Models;
using MoneyFlow.Services.Base;
using MoneyFlow.Services.Model.Customer;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MoneyFlow.Services.Services
{
    public interface ICustomerService
    {
        Task<IBusinessResult> GetAllCustomer();
        Task<IBusinessResult> Login(LoginForm request);
        Task<IBusinessResult> GetCustomerById(int Id);
    }
    public class CustomerService:ICustomerService
    {
        private UnitOfWork _unitOfWork;
        public CustomerService(UnitOfWork unitOfWork) {
            _unitOfWork ??= new UnitOfWork();
        }
        public async Task<IBusinessResult> GetAllCustomer() { 
            var list = await _unitOfWork.CustomerRepository.GetAllAsync();
            return new BusinessResult(200, "List customer", list);
        
        }

        public async Task<IBusinessResult> GetCustomerById(int Id)
        {
           
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(Id);
            if (customer == null) return new BusinessResult(404, "Customer not found");
            return new BusinessResult(200, "Get infomation of Customer", customer);
        }

        public async Task<IBusinessResult> Login(LoginForm request)
        {
            var customer = await _unitOfWork.CustomerRepository.Login(request.Email,request.Password);
            if (customer != null)
            {
                if (customer.Status == false) return new BusinessResult(400, "This account is inavailable");
                var token = CreateJwtToken(customer);
                return new BusinessResult(200,"Login successful",new { Token = token });
            }
            return new BusinessResult(400, "Login Fail");
        }
        
        private string CreateJwtToken(Customer user)
        {
            var utcNow = DateTime.UtcNow;
            var authClaims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new(JwtRegisteredClaimNames.Email, user.Email),
                new(ClaimTypes.Role, "Customer"),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var key = Encoding.ASCII.GetBytes("this_is_a_very_long_secret_key_with_at_least_32_chars!");

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(authClaims),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
                Expires = utcNow.Add(TimeSpan.FromDays(1)),
            };

            var handler = new JwtSecurityTokenHandler();

            var token = handler.CreateToken(tokenDescriptor);
            var result = handler.WriteToken(token);
            return result;
        }
    }

}
