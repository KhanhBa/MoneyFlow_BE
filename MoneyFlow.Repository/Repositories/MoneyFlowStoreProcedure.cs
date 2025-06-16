using Microsoft.EntityFrameworkCore;
using MoneyFlow.Repositories.Models;

namespace MoneyFlow.Repositories.Repositories;

public class MoneyFlowStoreProcedure
{
    private readonly MoneyFlowContext _context;

    public MoneyFlowStoreProcedure(MoneyFlowContext context)
    {
        _context = context;
    }
    
    public async Task<List<Customer>> GetAllCustomers()
    {
        return await  _context.Customers
            .FromSqlRaw("EXEC GetAllCustomers")
            .ToListAsync();
    }

}