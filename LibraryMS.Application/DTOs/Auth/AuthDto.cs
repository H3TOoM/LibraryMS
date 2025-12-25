using LibraryMS.Application.DTOs.Members;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Application.DTOs.Auth
{
    public class AuthResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
        public MemberReadDto Member { get; set; } = default!;
    }
}
