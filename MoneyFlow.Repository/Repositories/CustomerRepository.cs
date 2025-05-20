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
    }
}
