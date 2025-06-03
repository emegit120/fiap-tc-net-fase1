using FIAPTechChallenge.Domain.Entities;

namespace FIAPTechChallenge.Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
    }
}
