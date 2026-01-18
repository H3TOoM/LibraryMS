using LibraryMS.Application.Features.Members.Queries.GetMemberEntity;
using LibraryMS.Domain.Entities;
using LibraryMS.Domain.Repoistries;
using MediatR;

namespace LibraryMS.Application.Features.Members.Queries.GetMemberEntity;

public class GetMemberEntityQueryHandler : IRequestHandler<GetMemberEntityQuery, Member?>
{
    private readonly IMainRepoistery<Member> _mainRepoistory;

    public GetMemberEntityQueryHandler(IMainRepoistery<Member> mainRepoistory)
    {
        _mainRepoistory = mainRepoistory;
    }

    public async Task<Member?> Handle(GetMemberEntityQuery request, CancellationToken cancellationToken)
    {
        return await _mainRepoistory.GetByIdAsync(request.Id);
    }
}
