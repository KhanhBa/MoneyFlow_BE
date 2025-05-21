using Microsoft.EntityFrameworkCore;
using MoneyFlow.Repositories.Base;
using MoneyFlow.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyFlow.Repositories.Repositories
{
    public class CustomerRepository:GenericRepository<Customer>
    {
        public CustomerRepository() {
            _context ??= new MoneyFlowContext();
        }
        public CustomerRepository(MoneyFlowContext context)
        {
            _context = context;
        }

        public async Task<Customer> Login(string email, string password)
        {
            var obj = await _context.Customers.Where(x => x.Email == email && x.Password == password)
                .FirstOrDefaultAsync();
            return obj;
        }
    }
}
