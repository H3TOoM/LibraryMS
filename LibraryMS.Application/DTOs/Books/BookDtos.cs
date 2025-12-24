namespace LibraryMS.Application.DTOs.Books
{
    public sealed record BookCreateDto(
        string Title,
        string Author,
        string ISBN,
        DateTime PublishedDate,
        int CopiesAvailable,
        int CategoryId);

    public sealed record BookUpdateDto(
        string Title,
        string Author,
        string ISBN,
        DateTime PublishedDate,
        int CopiesAvailable,
        int CategoryId);

    public sealed record BookReadDto(
        int Id,
        string Title,
        string Author,
        string ISBN,
        DateTime PublishedDate,
        int CopiesAvailable,
        int CategoryId,
        string? CategoryName);
}
