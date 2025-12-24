using LibraryMS.Application.DTOs.BorrowRecords;

namespace LibraryMS.Application.Interfaces
{
    public interface IBorrowService
    {
        Task<IEnumerable<BorrowRecordReadDto>> GetAllAsync();
        Task<BorrowRecordReadDto?> GetByIdAsync(int id);

        Task<int> BorrowAsync(BorrowRecordCreateDto dto);
        Task<bool> ReturnAsync(int borrowRecordId, DateTime returnedAt);

        Task<IEnumerable<BorrowRecordReadDto>> GetByMemberIdAsync(int memberId);
        Task<IEnumerable<BorrowRecordReadDto>> GetByBookIdAsync(int bookId);
    }
}
