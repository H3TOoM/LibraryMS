using LibraryMS.Application.DTOs.Fines;

namespace LibraryMS.Application.Interfaces
{
    /// <summary>
    /// Interface for fine management services
    /// Defines methods for fine creation, payment, and fine-related queries
    /// </summary>
    public interface IFineService
    {
        #region CRUD Operations

        /// <summary>
        /// Retrieves all fines in the system
        /// </summary>
        /// <returns>Collection of fine read DTOs</returns>
        Task<IEnumerable<FineReadDto>> GetAllAsync();

        /// <summary>
        /// Retrieves a specific fine by ID
        /// </summary>
        /// <param name="id">Fine ID</param>
        /// <returns>Fine read DTO or null if not found</returns>
        Task<FineReadDto?> GetByIdAsync(int id);

        /// <summary>
        /// Creates a new fine record
        /// </summary>
        /// <param name="dto">Fine creation data</param>
        /// <returns>ID of the created fine</returns>
        Task<int> CreateAsync(FineCreateDto dto);

        /// <summary>
        /// Updates an existing fine's information
        /// </summary>
        /// <param name="id">Fine ID to update</param>
        /// <param name="dto">Updated fine information</param>
        /// <returns>True if update was successful, false if fine not found</returns>
        Task<bool> UpdateAsync(int id, FineUpdateDto dto);

        /// <summary>
        /// Deletes a fine from the system
        /// </summary>
        /// <param name="id">Fine ID to delete</param>
        /// <returns>True if deletion was successful, false if fine not found</returns>
        Task<bool> DeleteAsync(int id);

        #endregion

        #region Fine-Specific Operations

        /// <summary>
        /// Marks a fine as paid and sets the payment date
        /// </summary>
        /// <param name="fineId">Fine ID to pay</param>
        /// <param name="paidAt">Date and time when the fine was paid</param>
        /// <returns>True if payment was successful, false if fine not found</returns>
        Task<bool> PayAsync(int fineId, DateTime paidAt);

        /// <summary>
        /// Retrieves fines associated with a specific borrow record
        /// </summary>
        /// <param name="borrowRecordId">Borrow record ID</param>
        /// <returns>Collection of fines for the specified borrow record</returns>
        Task<IEnumerable<FineReadDto>> GetByBorrowRecordIdAsync(int borrowRecordId);

        #endregion
    }
}
