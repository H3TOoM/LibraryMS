using LibraryMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Domain.Repoistries
{
    public interface IBorrowRepoistory
    {
        Task<BorrowRecord> GetByBookIdAsync(int bookId);
        Task<BorrowRecord> GetByMemberIdAsync(int memberId);
        Task<bool> ReturnAsync(int borrowRecordId, DateTime returnedAt);
    }
}
