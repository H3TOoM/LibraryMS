using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Domain.Repoistries
{
    public interface IMemberRepoistory
    {
        Task<int> GetActiveBorrowCountAsync(int memberId);
    }
}
