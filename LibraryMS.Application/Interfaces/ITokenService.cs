using LibraryMS.Application.DTOs.Members;
using LibraryMS.Application.Helpers;
using LibraryMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Application.Interfaces
{
    public interface ITokenService
    {
        TokenResult GenerateToken(Member member);
    }
}
