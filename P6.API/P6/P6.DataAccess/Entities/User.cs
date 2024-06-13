using System;
using System.Collections.Generic;

namespace P6.DataAccess.Entities;

public partial class User
{
    public Guid UserId { get; set; }

    public string? Email { get; set; }

    public string? PasswordHash { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime? ExpiredToken { get; set; }

    public string? Status { get; set; }

    public string? Role { get; set; }
}
