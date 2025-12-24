using LibraryMS.Application.DTOs.Fines;

namespace LibraryMS.Application.Interfaces
{
    public interface IFineService
    {
        Task<IEnumerable<FineReadDto>> GetAllAsync();
        Task<FineReadDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(FineCreateDto dto);
        Task<bool> UpdateAsync(int id, FineUpdateDto dto);
        Task<bool> DeleteAsync(int id);

        Task<bool> PayAsync(int fineId, DateTime paidAt);
        Task<IEnumerable<FineReadDto>> GetByBorrowRecordIdAsync(int borrowRecordId);
    }
}
