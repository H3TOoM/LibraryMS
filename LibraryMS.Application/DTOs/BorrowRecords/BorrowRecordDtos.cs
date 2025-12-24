using LibraryMS.Application.DTOs.Books;
using LibraryMS.Application.DTOs.Members;

namespace LibraryMS.Application.DTOs.BorrowRecords
{
    public sealed record BorrowRecordCreateDto(
        int BookId,
        int MemberId,
        DateTime DueDate);

    public sealed record BorrowRecordUpdateDto(
        DateTime DueDate,
        DateTime? ReturnedAt);

    public sealed record BorrowRecordReadDto(
        int Id,
        int BookId,
        int MemberId,
        DateTime BorrowedAt,
        DateTime DueDate,
        DateTime? ReturnedAt,
        double FineAmount,
        BookReadDto? Book,
        MemberReadDto? Member);
}
