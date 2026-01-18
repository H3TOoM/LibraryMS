using MediatR;

namespace LibraryMS.Application.Features.Members.Queries.GetActiveBorrowCount;

public record GetActiveBorrowCountQuery(int MemberId) : IRequest<int>;
