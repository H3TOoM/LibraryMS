using LibraryMS.Application.DTOs.BorrowRecords;
using MediatR;

namespace LibraryMS.Application.Features.Borrows.Queries.GetAll;

public record GetAllBorrowRecordsQuery : IRequest<IEnumerable<BorrowRecordReadDto>>;
