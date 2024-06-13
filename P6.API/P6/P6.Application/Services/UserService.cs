using AutoMapper;
using P6.Application.Abstract;
using P6.Application.DTOs;
using P6.Core.Entities;
using P6.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace P6.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResult> DeleteMultipleUserServiceAsync(List<Guid> ids)
        {
            if (ids != null && ids.Any())
            {
                var users = await _unitOfWork.UserRepository.GetAllAsync(u => ids.Contains(u.UserId));
                await _unitOfWork.UserRepository.MultipleDelete(users);
                await _unitOfWork.SaveChangeAsync();
                return new ServiceResult
                {
                    StatusCode = HttpStatusCode.OK,
                    Success = true,
                    UserMsg = "Users deleted successfully"
                };
            }
            return new ServiceResult
            {
                StatusCode = HttpStatusCode.BadRequest,
                Success = false,
                UserMsg = "Invalid user IDs"
            };
        }

        public async Task<ServiceResult> DeleteUserServiceAsync(Guid id)
        {
            if (id != Guid.Empty)
            {
                var user = await _unitOfWork.UserRepository.GetSingleAsync(u => u.UserId == id);
                if (user != null)
                {
                    await _unitOfWork.UserRepository.Delete(user);
                    await _unitOfWork.SaveChangeAsync();
                    return new ServiceResult
                    {
                        StatusCode = HttpStatusCode.OK,
                        Success = true,
                        UserMsg = "User deleted successfully"
                    };
                }
                return new ServiceResult
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Success = false,
                    UserMsg = "User not found"
                };
            }
            return new ServiceResult
            {
                StatusCode = HttpStatusCode.BadRequest,
                Success = false,
                UserMsg = "Invalid user ID"
            };
        }

        public async Task<ServiceResult> GetUserByIdServiceAsync(Guid id)
        {
            if (id != Guid.Empty)
            {
                var user = await _unitOfWork.UserRepository.GetSingleAsync(u => u.UserId == id);
                if (user != null)
                {
                    return new ServiceResult
                    {
                        StatusCode = HttpStatusCode.OK,
                        Success = true,
                        Data = user
                    };
                }
                return new ServiceResult
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Success = false,
                    UserMsg = "User not found"
                };
            }
            return new ServiceResult
            {
                StatusCode = HttpStatusCode.BadRequest,
                Success = false,
                UserMsg = "Invalid user ID"
            };
        }

        public async Task<ServiceResult> InsertMultipleUserServiceAsync(List<UserSaveDTO> userDTOs)
        {
            if (userDTOs != null && userDTOs.Any())
            {
                // HashSet to store unique emails
                var uniqueEmails = new HashSet<string>();

                // Filter out users with duplicate emails and map to User entities
                var uniqueUsers = new List<User>();
                foreach (var userDto in userDTOs)
                {
                    if (uniqueEmails.Add(userDto.Email)) // Add returns true if email is added (not duplicate)
                    {
                        var userEntity = _mapper.Map<User>(userDto);
                        userEntity.UserId = Guid.NewGuid();
                        uniqueUsers.Add(userEntity);
                    }
                }

                // Check against database for existing users with same email
                var existingEmails = await _unitOfWork.UserRepository.GetAllAsync(u => uniqueEmails.Contains(u.Email));
                var existingEmailSet = new HashSet<string>(existingEmails.Select(u => u.Email));

                // Insert users with unique emails into database
                var usersToInsert = uniqueUsers.Where(u => !existingEmailSet.Contains(u.Email)).ToList();
                if (usersToInsert.Any())
                {
                    await _unitOfWork.UserRepository.MultipleCreate(usersToInsert);
                    await _unitOfWork.SaveChangeAsync();
                    return new ServiceResult
                    {
                        StatusCode = HttpStatusCode.Created,
                        Success = true,
                        UserMsg = "Users inserted successfully"
                    };
                }
                return new ServiceResult
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Success = false,
                    UserMsg = "All users already exist in the database"
                };
            }

            return new ServiceResult
            {
                StatusCode = HttpStatusCode.BadRequest,
                Success = false,
                UserMsg = "Invalid user data"
            };
        }

        public async Task<ServiceResult> InsertUserServiceAsync(UserSaveDTO user)
        {
            if (user != null)
            {
                var res = await _unitOfWork.UserRepository.GetByEmailAsync(user.Email);
                if (res != null)
                {
                    return new ServiceResult
                    {
                        StatusCode = HttpStatusCode.Unauthorized,
                        Success = false,
                        DevMsg = "Email registed by another user!"
                    };
                }
                var u = _mapper.Map<User>(user);
                u.UserId = Guid.NewGuid();
                await _unitOfWork.UserRepository.Create(u);
                await _unitOfWork.SaveChangeAsync();
                return new ServiceResult
                {
                    StatusCode = HttpStatusCode.Created,
                    Success = true,
                    UserMsg = "User created successfully"
                };
            }
            return new ServiceResult
            {
                StatusCode = HttpStatusCode.BadRequest,
                Success = false,
                UserMsg = "Invalid user data"
            };
        }

        public async Task<ServiceResult> UpdateMultipleUserServiceAsync(List<UserSaveDTO> userDTOs)
        {
            if (userDTOs != null && userDTOs.Any())
            {
                // HashSet to store unique emails
                var uniqueEmails = new HashSet<string>();

                // Filter out users with duplicate emails and map to User entities
                var uniqueUsers = new List<User>();
                foreach (var userDto in userDTOs)
                {
                    if (uniqueEmails.Add(userDto.Email)) // Add returns true if email is added (not duplicate)
                    {
                        var userEntity = _mapper.Map<User>(userDto);
                        uniqueUsers.Add(userEntity);
                    }
                }

                // Check against database for existing users with same email
                var existingEmails = await _unitOfWork.UserRepository.GetAllAsync(u => uniqueEmails.Contains(u.Email));
                var existingEmailSet = new HashSet<string>(existingEmails.Select(u => u.Email));

                // Update users with unique emails into database
                var usersToUpdate = uniqueUsers.Where(u => !existingEmailSet.Contains(u.Email)).ToList();
                if (usersToUpdate.Any())
                {
                    await _unitOfWork.UserRepository.MultipleUpdate(usersToUpdate);
                    await _unitOfWork.SaveChangeAsync();
                    return new ServiceResult
                    {
                        StatusCode = HttpStatusCode.Created,
                        Success = true,
                        UserMsg = "Users updated successfully"
                    };
                }
                return new ServiceResult
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Success = false,
                    UserMsg = "All users already exist in the database"
                };
            }
            return new ServiceResult
            {
                StatusCode = HttpStatusCode.BadRequest,
                Success = false,
                UserMsg = "Invalid user data"
            };
        }

        public async Task<ServiceResult> UpdateUserServiceAsync(UserSaveDTO user)
        {
            if (user != null)
            {
                var res = await _unitOfWork.UserRepository.GetByEmailAsync(user.Email);
                if (res != null)
                {
                    return new ServiceResult
                    {
                        StatusCode = HttpStatusCode.Unauthorized,
                        Success = false,
                        DevMsg = "Email registed by another user!"
                    };
                }
                var u = _mapper.Map<User>(user);
                await _unitOfWork.UserRepository.Update(u);
                await _unitOfWork.SaveChangeAsync();

                return new ServiceResult
                {
                    StatusCode = HttpStatusCode.Created,
                    Success = true,
                    UserMsg = "User updated successfully"
                };
            }
            return new ServiceResult
            {
                StatusCode = HttpStatusCode.BadRequest,
                Success = false,
                UserMsg = "Invalid user data"
            };
        }

        public async Task<ServiceResult> GetUserListServiceAsync()
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync();
            return new ServiceResult
            {
                StatusCode = HttpStatusCode.OK,
                Success = true,
                Data = users.ToList()
            };
        }
    }
}
