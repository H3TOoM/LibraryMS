using LibraryMS.Application.DTOs.BorrowRecords;
using MediatR;

namespace LibraryMS.Application.Features.Borrows.Queries.GetById;

public record GetBorrowRecordByIdQuery(int Id) : IRequest<BorrowRecordReadDto?>;
