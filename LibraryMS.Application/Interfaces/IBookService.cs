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

       
        Task<IEnumerable<BookReadDto>> GetAllAsync();

        
        Task<BookReadDto?> GetByIdAsync(int id);

       
        Task<int> CreateAsync(BookCreateDto dto);

       
        Task<bool> UpdateAsync(int id, BookUpdateDto dto);

        
        Task<bool> DeleteAsync(int id);

        #endregion

        #region Book-Specific Queries

       
        Task<IEnumerable<BookReadDto>> GetByCategoryIdAsync(int categoryId);

        Task<IEnumerable<BookReadDto>> SearchAsync(string query);

        #endregion
    }
}
