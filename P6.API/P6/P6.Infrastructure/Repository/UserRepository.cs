using Microsoft.EntityFrameworkCore;
using P6.Core.Entities;

namespace P6.Infrastructure.Repository
{
    internal class UserRepository : BaseRepository<User>, IUserRepository
    {
        readonly CodLuckContext _context;
        public UserRepository(CodLuckContext codLuckContext) : base(codLuckContext)
        {
            _context = codLuckContext;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Email == email);
        }

        public async Task<User?> GetUserByTokenAsync(string? RefreshToken)
        {
            return await _context.Users.SingleOrDefaultAsync(user => user.RefreshToken!.Equals(RefreshToken));
        }
    }
}