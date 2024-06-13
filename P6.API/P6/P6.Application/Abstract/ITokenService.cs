using P6.Application.DTOs;
using P6.Core.Entities;

namespace P6.Application.Abstract
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user);
        RefreshTokenModel GenerateRefreshToken();
        Task<TokenResultDTO> JWTGeneratorAsync(User user);
        Task<ServiceResult> RefreshServiceAsync(string? refreshToken, string? action = "refresh");
        void SetCookies(string? variable, string? value, DateTime expires);
        Task<User> UpdateUserRefreshTokenAsync(User user);
    }
}