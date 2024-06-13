using P6.Application.Abstract;
using P6.Core.Exceptions;
using System.Security.Claims;


namespace P6.Application.Services
{
    public class CommonService
    {
        ITokenService _tokenService;

        public CommonService(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        public async Task<Guid> GetUserIdByAccesTokenAsync(string? accessToken)
        {
            if (accessToken == null)
            {
                throw new BadRequestException("Bạn không có quyền truy cập dung này");
            }
            ClaimsPrincipal claimsPrincipal = await _tokenService.GetPrincipalFromExpiredTokenAsync(accessToken);
            var idClaim = claimsPrincipal.FindFirst("Id");
            // user without account was viewed
            if (idClaim == null)
            {
                throw new BadRequestException("Bạn không có quyền truy cập nội dung này");
            }
            return Guid.Parse(idClaim.Value);
        }
    }
}
