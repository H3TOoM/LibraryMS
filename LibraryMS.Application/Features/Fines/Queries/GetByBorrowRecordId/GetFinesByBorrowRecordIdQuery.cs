using LibraryMS.Application.DTOs.Fines;
using MediatR;

namespace LibraryMS.Application.Features.Fines.Queries.GetByBorrowRecordId;

public record GetFinesByBorrowRecordIdQuery(int BorrowRecordId) : IRequest<IEnumerable<FineReadDto>>;
