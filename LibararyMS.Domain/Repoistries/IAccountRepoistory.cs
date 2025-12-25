using LibraryMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Domain.Repoistries
{
    public interface IAccountRepoistory
    {
        Task<bool> IsEmailTakenAsync(string email);
        Task<Member> GetUserByEmail(string email);
    }
}
