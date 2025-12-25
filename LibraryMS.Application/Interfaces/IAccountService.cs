using LibraryMS.Application.DTOs.Members;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Application.Interfaces
{
    public interface IAccountService
    {
        Task<bool> IsEmailTakenAsync(string email);
        Task<int> CreateAccount(MemberCreateDto dto);
        Task<bool> UpdateAccount(int id, MemberUpdateDto dto);
        Task<int> LoginAsync(string email, string password);

    }
}
