using LibraryMS.Application.Features.Members.Queries.GetActiveBorrowCount;
using LibraryMS.Domain.Repoistries;
using MediatR;

namespace LibraryMS.Application.Features.Members.Queries.GetActiveBorrowCount;

public class GetActiveBorrowCountQueryHandler : IRequestHandler<GetActiveBorrowCountQuery, int>
{
    private readonly IMemberRepoistory _memberRepoistory;

    public GetActiveBorrowCountQueryHandler(IMemberRepoistory memberRepoistory)
    {
        _memberRepoistory = memberRepoistory;
    }

    public async Task<int> Handle(GetActiveBorrowCountQuery request, CancellationToken cancellationToken)
    {
        var borrowCount = await _memberRepoistory.GetActiveBorrowCountAsync(request.MemberId);
        return borrowCount;
    }
}
