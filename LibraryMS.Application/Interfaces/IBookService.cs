using LibraryMS.Application.DTOs.Books;

namespace LibraryMS.Application.Interfaces
{
    /// <summary>
    /// Interface for book management services
    /// Defines methods for CRUD operations, search, and category-based queries
    /// </summary>
    public interface IBookService
    {
        #region CRUD Operations

        /// <summary>
        /// Retrieves all books in the library
        /// </summary>
        /// <returns>Collection of book read DTOs</returns>
        Task<IEnumerable<BookReadDto>> GetAllAsync();

        /// <summary>
        /// Retrieves a specific book by ID
        /// </summary>
        /// <param name="id">Book ID</param>
        /// <returns>Book read DTO or null if not found</returns>
        Task<BookReadDto?> GetByIdAsync(int id);

        /// <summary>
        /// Creates a new book in the library
        /// </summary>
        /// <param name="dto">Book creation data</param>
        /// <returns>ID of the created book</returns>
        Task<int> CreateAsync(BookCreateDto dto);

        /// <summary>
        /// Updates an existing book's information
        /// </summary>
        /// <param name="id">Book ID to update</param>
        /// <param name="dto">Updated book information</param>
        /// <returns>True if update was successful, false if book not found</returns>
        Task<bool> UpdateAsync(int id, BookUpdateDto dto);

        /// <summary>
        /// Deletes a book from the library
        /// </summary>
        /// <param name="id">Book ID to delete</param>
        /// <returns>True if deletion was successful, false if book not found</returns>
        Task<bool> DeleteAsync(int id);

        #endregion

        #region Book-Specific Queries

        /// <summary>
        /// Retrieves all books belonging to a specific category
        /// </summary>
        /// <param name="categoryId">Category ID</param>
        /// <returns>Collection of books in the specified category</returns>
        Task<IEnumerable<BookReadDto>> GetByCategoryIdAsync(int categoryId);

        /// <summary>
        /// Searches for books by title, author, or ISBN
        /// </summary>
        /// <param name="query">Search query string</param>
        /// <returns>Collection of matching books</returns>
        Task<IEnumerable<BookReadDto>> SearchAsync(string query);

        #endregion
    }
}
