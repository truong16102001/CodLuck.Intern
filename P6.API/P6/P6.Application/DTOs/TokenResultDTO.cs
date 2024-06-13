using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P6.Application.DTOs
{
    public class TokenResultDTO
    {
        public string AccessToken { get; set; }
        public RefreshTokenModel RefreshToken { get; set; }
    }
}
