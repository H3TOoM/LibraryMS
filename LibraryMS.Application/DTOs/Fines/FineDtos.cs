namespace LibraryMS.Application.DTOs.Fines
{
    public sealed record FineCreateDto(
        int BorrowRecordId,
        double Amount);

    public sealed record FineUpdateDto(
        bool IsPaid,
        DateTime? PaidAt);

    public sealed record FineReadDto(
        int Id,
        int BorrowRecordId,
        double Amount,
        bool IsPaid,
        DateTime? PaidAt);
}
