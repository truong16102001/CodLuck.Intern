using P6.Application.DTOs;

namespace P6.Application.Abstract
{
    public interface IAuthService
    {
        Task<ServiceResult> AuthenticateAsync(LoginModel loginModel);
    }
}