namespace LibraryMS.Application.DTOs.Categories
{
    public sealed record CategoryCreateDto(string Name);

    public sealed record CategoryUpdateDto(string Name);

    public sealed record CategoryReadDto(int Id, string Name);
}
