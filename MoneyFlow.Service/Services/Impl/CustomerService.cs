using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ErrorHanding;
using Microsoft.IdentityModel.Tokens;
using MoneyFlow.Payload.Request;
using MoneyFlow.Repositories.Base;
using MoneyFlow.Repositories.Models;
using MoneyFlow.Repositories.Repositories;
using MoneyFlow.Services.Base;

namespace MoneyFlow.Services.Services.Impl;

public class CustomerService : ICustomerService
    {
        private UnitOfWork _unitOfWork;
        private MoneyFlowStoreProcedure _moneyFlowStoreProcedure;
        public CustomerService(UnitOfWork unitOfWork, MoneyFlowStoreProcedure moneyFlowStoreProcedure) {
            _unitOfWork = unitOfWork;
            _moneyFlowStoreProcedure = moneyFlowStoreProcedure;
        }
        public async Task<IBusinessResult> GetAllCustomer()
        {
            var list = await _moneyFlowStoreProcedure.GetAllCustomers();
            return new BusinessResult(200, "List customer", list);
        
        }

        public async Task<IBusinessResult> GetCustomerById(int Id)
        {
           
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(Id);
            if (customer == null) return new BusinessResult(404, "Customer not found");
            return new BusinessResult(200, "Get infomation of Customer", customer);
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var customer = await _unitOfWork.CustomerRepository.Login(request.Email,request.Password);
             
            
            if (customer != null)
            {
                if (customer.Status == false) throw new AppException(ErrorCode.LoginFail);
                var token = CreateJwtToken(customer);
                return token;
            }
            throw new AppException(ErrorCode.LoginFail);
        }

        private LoginResponse CreateJwtToken(Customer user)
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
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            // Access token (thường 15-60 phút)
            var accessTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(authClaims),
                Expires = utcNow.AddMinutes(30),
                SigningCredentials = signingCredentials
            };

            var handler = new JwtSecurityTokenHandler();
            var accessToken = handler.WriteToken(handler.CreateToken(accessTokenDescriptor));

            // Refresh token (thường 7-30 ngày)
            var refreshTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        }),
                Expires = utcNow.AddDays(7),
                SigningCredentials = signingCredentials
            };

            var refreshToken = handler.WriteToken(handler.CreateToken(refreshTokenDescriptor));

            return new LoginResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresIn = 1800, // 30 phút
                TokenType = "Bearer"
            };
        }

    }