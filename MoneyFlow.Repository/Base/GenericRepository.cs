using Microsoft.EntityFrameworkCore;
using MoneyFlow.Repositories.Models;
using System.Linq.Expressions;

namespace MoneyFlow.Repositories.Base
{
    public class GenericRepository<T> where T : class
    {
        protected readonly MoneyFlowContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(MoneyFlowContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T?> GetByIdAsync(string code)
        {
            return await _dbSet.FindAsync(code);
        }

        public async Task<List<T>> FindByConditionAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<List<T>> GetAllIncludingAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;
            foreach (var include in includes)
                query = query.Include(include);

            return await query.ToListAsync();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity); 
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

    }
}
