using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P6.Application.DTOs
{
    public class UserSaveDTO
    {
        public Guid? UserId { get; set; }

        public string? Email { get; set; }

        public string? Status { get; set; }

        public string? Role { get; set; }

    }
}
