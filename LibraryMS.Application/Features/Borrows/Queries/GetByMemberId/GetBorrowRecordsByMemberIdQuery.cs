using LibraryMS.Application.DTOs.BorrowRecords;
using MediatR;

namespace LibraryMS.Application.Features.Borrows.Queries.GetByMemberId;

public record GetBorrowRecordsByMemberIdQuery(int MemberId) : IRequest<IEnumerable<BorrowRecordReadDto>>;
