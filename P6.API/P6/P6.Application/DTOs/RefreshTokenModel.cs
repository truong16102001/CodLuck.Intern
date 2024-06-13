using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P6.Application.DTOs
{
    public class RefreshTokenModel
    {
        public string? Token { get; set; }
        public DateTime? Expires { get; set; }
    }
}
