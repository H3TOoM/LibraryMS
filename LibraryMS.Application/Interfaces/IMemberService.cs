using LibraryMS.Application.DTOs.Members;
using LibraryMS.Domain.Entities;

namespace LibraryMS.Application.Interfaces
{
    /// <summary>
    /// Interface for member management services
    /// Defines methods for CRUD operations and member-specific queries
    /// </summary>
    public interface IMemberService
    {
        #region CRUD Operations

        /// <summary>
        /// Retrieves all members in the system
        /// </summary>
        /// <returns>Collection of member read DTOs</returns>
        Task<IEnumerable<MemberReadDto>> GetAllAsync();

        /// <summary>
        /// Retrieves a specific member by ID
        /// </summary>
        /// <param name="id">Member ID</param>
        /// <returns>Member read DTO or null if not found</returns>
        Task<MemberReadDto?> GetByIdAsync(int id);

        /// <summary>
        /// Creates a new member in the system
        /// </summary>
        /// <param name="dto">Member creation data</param>
        /// <returns>ID of the created member</returns>
        Task<int> CreateAsync(MemberCreateDto dto);

        /// <summary>
        /// Updates an existing member's information
        /// </summary>
        /// <param name="id">Member ID to update</param>
        /// <param name="dto">Updated member information</param>
        /// <returns>True if update was successful, false if member not found</returns>
        Task<bool> UpdateAsync(int id, MemberUpdateDto dto);

        /// <summary>
        /// Deletes a member from the system
        /// </summary>
        /// <param name="id">Member ID to delete</param>
        /// <returns>True if deletion was successful, false if member not found</returns>
        Task<bool> DeleteAsync(int id);

        #endregion

        #region Member-Specific Queries

        /// <summary>
        /// Gets the count of active (unreturned) borrow records for a member
        /// </summary>
        /// <param name="memberId">Member ID</param>
        /// <returns>Count of active borrows</returns>
        Task<int> GetActiveBorrowCountAsync(int memberId);

        /// <summary>
        /// Retrieves the member entity by ID (for internal use by other services)
        /// </summary>
        /// <param name="id">Member ID</param>
        /// <returns>Member entity or null if not found</returns>
        Task<Member?> GetMemberEntityByIdAsync(int id);

        #endregion
    }
}
