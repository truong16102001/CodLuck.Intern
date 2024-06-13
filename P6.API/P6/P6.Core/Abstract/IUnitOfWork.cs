
namespace P6.Infrastructure.Repository
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        void Dispose();
        Task SaveChangeAsync();
    }
}