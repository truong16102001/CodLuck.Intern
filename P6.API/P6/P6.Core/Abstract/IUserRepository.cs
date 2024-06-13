using P6.Core.Entities;

namespace P6.Infrastructure.Repository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetUserByTokenAsync(string? RefreshToken);

        Task<User?> GetByEmailAsync(string email);
    }
}