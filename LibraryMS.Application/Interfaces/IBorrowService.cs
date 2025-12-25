using LibraryMS.Application.DTOs.BorrowRecords;

namespace LibraryMS.Application.Interfaces
{
    /// <summary>
    /// Interface for borrow record management services
    /// Defines methods for borrowing books, returning books, and borrow record queries
    /// </summary>
    public interface IBorrowService
    {
        #region Read Operations

        /// <summary>
        /// Retrieves all borrow records in the system
        /// </summary>
        /// <returns>Collection of borrow record read DTOs</returns>
        Task<IEnumerable<BorrowRecordReadDto>> GetAllAsync();

        /// <summary>
        /// Retrieves a specific borrow record by ID
        /// </summary>
        /// <param name="id">Borrow record ID</param>
        /// <returns>Borrow record read DTO or null if not found</returns>
        Task<BorrowRecordReadDto?> GetByIdAsync(int id);

        /// <summary>
        /// Retrieves all borrow records for a specific member
        /// </summary>
        /// <param name="memberId">Member ID</param>
        /// <returns>Collection of borrow records for the specified member</returns>
        Task<IEnumerable<BorrowRecordReadDto>> GetByMemberIdAsync(int memberId);

        /// <summary>
        /// Retrieves all borrow records for a specific book
        /// </summary>
        /// <param name="bookId">Book ID</param>
        /// <returns>Collection of borrow records for the specified book</returns>
        Task<IEnumerable<BorrowRecordReadDto>> GetByBookIdAsync(int bookId);

        #endregion

        #region Borrow Operations

        /// <summary>
        /// Creates a new borrow record (borrows a book)
        /// </summary>
        /// <param name="dto">Borrow record creation data</param>
        /// <returns>ID of the created borrow record</returns>
        Task<int> BorrowAsync(BorrowRecordCreateDto dto);

        /// <summary>
        /// Marks a borrow record as returned and sets the return date
        /// </summary>
        /// <param name="borrowRecordId">Borrow record ID to return</param>
        /// <param name="returnedAt">Date and time when the book was returned</param>
        /// <returns>True if return was successful, false if borrow record not found</returns>
        Task<bool> ReturnAsync(int borrowRecordId, DateTime returnedAt);

        #endregion
    }
}
