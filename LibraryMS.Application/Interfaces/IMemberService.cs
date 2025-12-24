using LibraryMS.Application.DTOs.Members;

namespace LibraryMS.Application.Interfaces
{
    public interface IMemberService
    {
        Task<IEnumerable<MemberReadDto>> GetAllAsync();
        Task<MemberReadDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(MemberCreateDto dto);
        Task<bool> UpdateAsync(int id, MemberUpdateDto dto);
        Task<bool> DeleteAsync(int id);

        Task<int> GetActiveBorrowCountAsync(int memberId);
    }
}
