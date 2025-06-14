using MoneyFlow.Repositories.Models;
using MoneyFlow.Repositories.Repositories;

namespace MoneyFlow.Repositories.Base
{
    public class UnitOfWork
    {
        private readonly MoneyFlowContext _context;

        public CustomerRepository CustomerRepository { get; }

        public UnitOfWork(MoneyFlowContext context, CustomerRepository customerRepository)
        {
            _context = context;
            CustomerRepository = customerRepository;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
