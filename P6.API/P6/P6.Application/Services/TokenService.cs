using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using P6.Application.Abstract;
using P6.Application.DTOs;
using P6.Core.Entities;
using P6.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace P6.Application.Services
{
    public class TokenService : ITokenService
    {
        readonly IConfiguration _configuration;
        readonly IUnitOfWork _unitOfWork;
        readonly IHttpContextAccessor _httpContextAccessor;

        public TokenService(IConfiguration configuration, IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        HttpContext HttpContext => _httpContextAccessor.HttpContext;

        public string GenerateAccessToken(User user)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var roleName = user.Role;

            var authClaims = new List<Claim>
            {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, roleName),
                     new Claim(ClaimTypes.Email, user.Email!),
                     new Claim("Id", user.UserId.ToString())
            };


            var authSigningKey = Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]);

            _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"],
                Subject = new ClaimsIdentity(authClaims),
                Expires = DateTime.UtcNow.AddMinutes(tokenValidityInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(authSigningKey), SecurityAlgorithms.HmacSha256Signature)
            };


            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encrypterToken = tokenHandler.WriteToken(token);

            SetCookies("Access-Token", encrypterToken, DateTime.UtcNow.AddMinutes(tokenValidityInMinutes));

            return encrypterToken;
        }

        public async Task<TokenResultDTO> JWTGeneratorAsync(User user)
        {

            var encrypterToken = GenerateAccessToken(user);

            var newRefreshToken = await UpdateUserRefreshTokenAsync(user);

            return new TokenResultDTO
            {
                AccessToken = encrypterToken,
                RefreshToken = new RefreshTokenModel
                {
                    Token = newRefreshToken.RefreshToken,
                    Expires = newRefreshToken.ExpiredToken
                }
            };
        }

        public async Task<User> UpdateUserRefreshTokenAsync(User user)
        {
            var newRefreshToken = GenerateRefreshToken();
            user.RefreshToken = newRefreshToken.Token!.ToString();
            user.ExpiredToken = newRefreshToken.Expires;
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveChangeAsync();
            SetCookies("Refresh-Token", newRefreshToken.Token, (DateTime)newRefreshToken.Expires);

            return user;
        }

        public RefreshTokenModel GenerateRefreshToken()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var token = new string(Enumerable.Repeat(chars, 64)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            var rt = new RefreshTokenModel
            {
                Token = token,
                Expires = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["JWT:RefreshTokenValidityInMinutes"].ToString())),
            };
            return rt;
        }

        public void SetCookies(string? variable, string? value, DateTime expires)
        {
            HttpContext.Response.Cookies.Append(variable, value,
                new CookieOptions
                {
                    Expires = expires,
                    HttpOnly = true,
                    Secure = true,
                    IsEssential = true,
                    SameSite = SameSiteMode.None
                });
        }

        public async Task<ServiceResult> RefreshServiceAsync(string? refreshToken, string? action)
        {
            refreshToken = WebUtility.UrlEncode(refreshToken);
            var user = await _unitOfWork.UserRepository.GetUserByTokenAsync(refreshToken);
            if (user != null)
            {
                if (action.Equals("logout", StringComparison.Ordinal))
                {
                    user.RefreshToken = "";
                    user.ExpiredToken = DateTime.UtcNow.AddDays(-10);

                    // Đặt lại IsLogged của tất cả các email của người dùng thành false
                    user.Email = string.Empty;
                    _unitOfWork.UserRepository.Update(user);

                    SetCookies("Access-Token", "", DateTime.UtcNow.AddDays(-10));
                    SetCookies("Refresh-Token", "", DateTime.UtcNow.AddDays(-10));

                    return new ServiceResult
                    {
                        StatusCode = System.Net.HttpStatusCode.OK,
                        DevMsg = "Logout successfully",
                        UserMsg = "Logout successfully"
                    };
                }

                var newAccessToken = GenerateAccessToken(user);

                var newRefreshToken = await UpdateUserRefreshTokenAsync(user);

                var data = new TokenResultDTO
                {
                    AccessToken = newAccessToken,
                    RefreshToken = new RefreshTokenModel
                    {
                        Token = newRefreshToken.RefreshToken,
                        Expires = newRefreshToken.ExpiredToken
                    }
                };

                return new ServiceResult
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Data = data
                };
            }
            return new ServiceResult
            {
                StatusCode = System.Net.HttpStatusCode.NotFound,
                DevMsg = "There is no user with this token"
            };
        }
    }
}
