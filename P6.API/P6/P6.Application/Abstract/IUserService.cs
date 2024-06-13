using P6.Application.DTOs;
using P6.Core.Entities;

namespace P6.Application.Abstract
{
    public interface IUserService
    {
        Task<ServiceResult> DeleteMultipleUserServiceAsync(List<Guid> ids);
        Task<ServiceResult> DeleteUserServiceAsync(Guid id);
        Task<ServiceResult> GetUserByIdServiceAsync(Guid id);
        Task<ServiceResult> GetUserListServiceAsync();
        Task<ServiceResult> InsertMultipleUserServiceAsync(List<UserSaveDTO> users);
        Task<ServiceResult> InsertUserServiceAsync(UserSaveDTO user);
        Task<ServiceResult> UpdateMultipleUserServiceAsync(List<UserSaveDTO> users);
        Task<ServiceResult> UpdateUserServiceAsync(UserSaveDTO user);
    }
}