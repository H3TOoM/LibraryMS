using AutoMapper;
using LibraryMS.Application.DTOs.Members;
using LibraryMS.Application.Features.Members.Queries.GetAll;
using LibraryMS.Domain.Entities;
using LibraryMS.Domain.Repoistries;
using MediatR;

namespace LibraryMS.Application.Features.Members.Queries.GetAll;

public class GetAllMembersQueryHandler : IRequestHandler<GetAllMembersQuery, IEnumerable<MemberReadDto>>
{
    private readonly IMapper _mapper;
    private readonly IMainRepoistery<Member> _mainRepoistory;

    public GetAllMembersQueryHandler(IMapper mapper, IMainRepoistery<Member> mainRepoistory)
    {
        _mapper = mapper;
        _mainRepoistory = mainRepoistory;
    }

    public async Task<IEnumerable<MemberReadDto>> Handle(GetAllMembersQuery request, CancellationToken cancellationToken)
    {
        var members = await _mainRepoistory.GetAllAsync();
        return _mapper.Map<IEnumerable<MemberReadDto>>(members);
    }
}
