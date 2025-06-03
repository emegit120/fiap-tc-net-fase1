using FIAPTechChallenge.Domain.Entities;
using FIAPTechChallenge.Domain.Interfaces;
using FIAPTechChallenge.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FIAPTechChallenge.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(FiapDbContext context) : base(context) { }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }
    }
}
