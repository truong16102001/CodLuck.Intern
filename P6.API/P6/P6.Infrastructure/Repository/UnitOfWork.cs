using P6.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P6.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly CodLuckContext _context;

        private IUserRepository? _userRepository;
        public UnitOfWork(CodLuckContext cokluckDbContext)
        {
            _context = cokluckDbContext;
        }

        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_context);


        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
