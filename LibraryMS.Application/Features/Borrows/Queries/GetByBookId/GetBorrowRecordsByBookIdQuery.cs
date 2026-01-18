using LibraryMS.Application.DTOs.BorrowRecords;
using MediatR;

namespace LibraryMS.Application.Features.Borrows.Queries.GetByBookId;

public record GetBorrowRecordsByBookIdQuery(int BookId) : IRequest<IEnumerable<BorrowRecordReadDto>>;
