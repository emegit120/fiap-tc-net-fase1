using FIAPTechChallenge.Domain.Interfaces;
using FIAPTechChallenge.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FIAPTechChallenge.Infrastructure.Repositories
{
    public class Repository<T>(FiapDbContext context) : IRepository<T> where T : class
    {
        protected readonly FiapDbContext _context = context;
        private readonly DbSet<T> _dbSet = context.Set<T>();

        public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

        public void Update(T entity) => _dbSet.Update(entity);

        public void Remove(T entity) => _dbSet.Remove(entity);

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
