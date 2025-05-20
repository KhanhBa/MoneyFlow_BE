using MoneyFlow.Repositories.Base;
using MoneyFlow.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyFlow.Services.Services
{
    public interface ICustomerService
    {
        Task<IBusinessResult> GetAllCustomer(); 
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
    }
}
