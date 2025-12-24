using LibraryMS.Application.DTOs.Books;

namespace LibraryMS.Application.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookReadDto>> GetAllAsync();
        Task<BookReadDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(BookCreateDto dto);
        Task<bool> UpdateAsync(int id, BookUpdateDto dto);
        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<BookReadDto>> GetByCategoryIdAsync(int categoryId);
        Task<IEnumerable<BookReadDto>> SearchAsync(string query);
    }
}
