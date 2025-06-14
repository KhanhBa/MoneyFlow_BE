using Microsoft.EntityFrameworkCore;
using MoneyFlow.Repositories.Base;
using MoneyFlow.Repositories.Models;

namespace MoneyFlow.Repositories.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>
    {
        public CustomerRepository(MoneyFlowContext context) : base(context)
        {
        }

        public async Task<Customer?> Login(string email, string password)
        {
            return await _context.Customers
                .FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
        }
    }
}
