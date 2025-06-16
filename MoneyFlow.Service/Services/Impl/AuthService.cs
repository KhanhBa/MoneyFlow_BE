using MoneyFlow.Repositories.Base;

namespace MoneyFlow.Services.Services.Impl;

public class AuthService : IAuthService
{
    private UnitOfWork _unitOfWork;

    public AuthService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
}