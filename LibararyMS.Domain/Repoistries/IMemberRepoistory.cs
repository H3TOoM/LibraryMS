using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Domain.Repoistries
{
    /// <summary>
    /// Repository interface for member-specific data operations
    /// Provides methods for member-related queries and statistics
    /// </summary>
    public interface IMemberRepoistory
    {
        /// <summary>
        /// Gets the count of active (unreturned) borrow records for a member
        /// </summary>
        /// <param name="memberId">ID of the member</param>
        /// <returns>Count of active borrows</returns>
        Task<int> GetActiveBorrowCountAsync(int memberId);
    }
}
