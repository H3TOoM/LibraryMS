using LibraryMS.Application.DTOs.Members;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Application.Interfaces
{
    /// <summary>
    /// Interface for account management and authentication services
    /// Defines methods for user registration, login, and account management
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Checks if an email address is already registered
        /// </summary>
        /// <param name="email">Email address to check</param>
        /// <returns>True if email is taken, false otherwise</returns>
        Task<bool> IsEmailTakenAsync(string email);

        /// <summary>
        /// Creates a new member account with encrypted password
        /// </summary>
        /// <param name="dto">Member creation data</param>
        /// <returns>ID of the created member</returns>
        Task<int> CreateAccount(MemberCreateDto dto);

        /// <summary>
        /// Authenticates a member using email and password
        /// </summary>
        /// <param name="email">Member's email address</param>
        /// <param name="password">Member's password</param>
        /// <returns>ID of the authenticated member</returns>
        Task<int> LoginAsync(string email, string password);

        /// <summary>
        /// Updates an existing member's account information
        /// </summary>
        /// <param name="id">Member ID to update</param>
        /// <param name="dto">Updated member information</param>
        /// <returns>True if update was successful, false if member not found</returns>
        Task<bool> UpdateAccount(int id, MemberUpdateDto dto);
    }
}
