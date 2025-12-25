using LibraryMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Domain.Repoistries
{
    /// <summary>
    /// Repository interface for account-related data operations
    /// Provides methods for email validation and user retrieval by email
    /// </summary>
    public interface IAccountRepoistory
    {
        /// <summary>
        /// Checks if an email address is already registered by another member
        /// </summary>
        /// <param name="email">Email address to check</param>
        /// <returns>True if email is taken, false otherwise</returns>
        Task<bool> IsEmailTakenAsync(string email);

        /// <summary>
        /// Retrieves a member entity by their email address
        /// </summary>
        /// <param name="email">Email address of the member</param>
        /// <returns>Member entity if found, otherwise null</returns>
        Task<Member> GetUserByEmail(string email);
    }
}
