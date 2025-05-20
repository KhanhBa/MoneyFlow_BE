using MoneyFlow.Repositories.Repositories;
using MoneyFlow.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyFlow.Repositories.Base
{
    public class UnitOfWork
    {
        private readonly MoneyFlowContext _context;
        private readonly CustomerRepository _customerRepository;

        public UnitOfWork()
        {
            _context = new MoneyFlowContext();
            _customerRepository ??= new CustomerRepository();
        }

        public UnitOfWork(CustomerRepository customerRepository)
        {
            _context = new MoneyFlowContext();
            _customerRepository = customerRepository;
        }

        public CustomerRepository CustomerRepository { get { return _customerRepository; } }
    }
}
