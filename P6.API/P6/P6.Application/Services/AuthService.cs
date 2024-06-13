using AutoMapper;
using Microsoft.Extensions.Configuration;
using P6.Application.Abstract;
using P6.Application.DTOs;
using P6.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace P6.Application.Services
{
    public class AuthService : IAuthService
    {
        readonly IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;
        readonly IConfiguration _configuration;
        readonly ITokenService _tokenService;
        public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<ServiceResult> AuthenticateAsync(LoginModel loginModel)
        {
            var res = await _unitOfWork.UserRepository.GetByEmailAsync(loginModel.Email);
            if (res == null)
            {
                return new ServiceResult
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    Success = false,
                    DevMsg = "Login Failed. Invalid Email or Password!"
                };
            }

            // So sánh mật khẩu đã nhập với mật khẩu đã hash trong cơ sở dữ liệu
            if (!BCrypt.Net.BCrypt.Verify(loginModel.Password, res.PasswordHash))
            {
                return new ServiceResult
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    Success = false,
                    DevMsg = "Login Failed. Invalid Email or Password!"
                };
            }

            _unitOfWork.UserRepository.Update(res);

            TokenResultDTO tokenModel = await _tokenService.JWTGeneratorAsync(res);
            return new ServiceResult
            {
                StatusCode = HttpStatusCode.OK,
                Data = tokenModel,
                Success = true
            };
        }

    }
}
