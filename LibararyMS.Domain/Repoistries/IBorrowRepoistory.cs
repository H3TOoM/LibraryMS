using LibraryMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Domain.Repoistries
{
    /// <summary>
    /// Repository interface for borrow record-specific data operations
    /// Provides methods for borrow record queries and return operations
    /// </summary>
    public interface IBorrowRepoistory
    {
        /// <summary>
        /// Retrieves all borrow records for a specific book
        /// </summary>
        /// <param name="bookId">ID of the book</param>
        /// <returns>Collection of borrow records for the specified book</returns>
        Task<IEnumerable<BorrowRecord>> GetByBookIdAsync(int bookId);

        /// <summary>
        /// Retrieves all borrow records for a specific member
        /// </summary>
        /// <param name="memberId">ID of the member</param>
        /// <returns>Collection of borrow records for the specified member</returns>
        Task<IEnumerable<BorrowRecord>> GetByMemberIdAsync(int memberId);

        /// <summary>
        /// Marks a borrow record as returned and sets the return date
        /// </summary>
        /// <param name="borrowRecordId">ID of the borrow record to return</param>
        /// <param name="returnedAt">Date and time when the book was returned</param>
        /// <returns>True if return was successful, false otherwise</returns>
        Task<bool> ReturnAsync(int borrowRecordId, DateTime returnedAt);
    }
}
