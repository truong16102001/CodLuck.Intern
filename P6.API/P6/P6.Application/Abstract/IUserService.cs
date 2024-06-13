using P6.Core.Entities;

namespace P6.Application.Abstract
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersListAsync();
    }
}