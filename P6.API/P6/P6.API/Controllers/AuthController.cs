using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P6.Application.Abstract;
using P6.Application.DTOs;
using P6.Application.Services;

namespace P6.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        readonly ITokenService _tokenService;
        public AuthController(IAuthService authService, ITokenService tokenService)
        {
            _authService = authService;
            _tokenService = tokenService;
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SigninByLoginModel([FromBody] LoginModel loginModel)
        {
            var result = await _authService.AuthenticateAsync(loginModel);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] SignupModel signupModel)
        {
            var result = await _authService.SignupServiceAsync(signupModel);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken()
        {
            // Get the cookie header
            string cookieHeader = Request.Headers["Cookie"].ToString();

            // Parse the cookie header to extract individual cookies
            var cookies = cookieHeader.Split(';')
                                      .Select(cookie => cookie.Split('='))
                                      .ToDictionary(cookie => cookie[0].Trim(), cookie => cookie.Length > 1 ? cookie[1].Trim() : string.Empty);

            cookies.TryGetValue("Refresh-Token", out var refreshToken);

            var result = await _tokenService.RefreshServiceAsync(refreshToken);
            return StatusCode((int)result.StatusCode, result);
        }

        [Authorize]
        [HttpPost("signout")]
        public async Task<IActionResult> Logout()
        {
            // Get the cookie header
            string cookieHeader = Request.Headers["Cookie"].ToString();
            // Parse the cookie header to extract individual cookies
            var cookies = cookieHeader.Split(';')
                          .Select(cookie => cookie.Trim().Split('='))
                          .ToDictionary(cookie => cookie[0], cookie => Uri.UnescapeDataString(cookie[1]));

            cookies.TryGetValue("Refresh-Token", out var refreshToken);

            var result = await _tokenService.RefreshServiceAsync(refreshToken, "logout");
            return StatusCode((int)result.StatusCode, result);
        }

    }
}
