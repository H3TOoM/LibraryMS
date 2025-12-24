namespace LibraryMS.Application.DTOs.Members
{
    public sealed record MemberCreateDto(
        string Name,
        string Email,
        string Phone,
        string Password,
        int MaxBorrowLimit);

    public sealed record MemberUpdateDto(
        string Name,
        string Email,
        string Phone,
        int MaxBorrowLimit,
        string Role);

    public sealed record MemberReadDto(
        int Id,
        string Name,
        string Email,
        string Phone,
        string Role,
        DateTime CreatedAt,
        int MaxBorrowLimit);
}
