using P6.Application.Abstract;
using P6.Core.Entities;
using P6.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P6.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<User>> GetUsersListAsync()
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync();
            return users;
        }
    }
}
