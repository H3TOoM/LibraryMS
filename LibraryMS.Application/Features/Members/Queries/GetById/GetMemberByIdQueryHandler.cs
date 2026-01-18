using AutoMapper;
using LibraryMS.Application.DTOs.Members;
using LibraryMS.Application.Features.Members.Queries.GetById;
using LibraryMS.Domain.Entities;
using LibraryMS.Domain.Repoistries;
using MediatR;

namespace LibraryMS.Application.Features.Members.Queries.GetById;

public class GetMemberByIdQueryHandler : IRequestHandler<GetMemberByIdQuery, MemberReadDto?>
{
    private readonly IMapper _mapper;
    private readonly IMainRepoistery<Member> _mainRepoistory;

    public GetMemberByIdQueryHandler(IMapper mapper, IMainRepoistery<Member> mainRepoistory)
    {
        _mapper = mapper;
        _mainRepoistory = mainRepoistory;
    }

    public async Task<MemberReadDto?> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
    {
        var member = await _mainRepoistory.GetByIdAsync(request.Id);
        return _mapper.Map<MemberReadDto?>(member);
    }
}
