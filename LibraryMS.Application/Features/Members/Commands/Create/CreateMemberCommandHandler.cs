using AutoMapper;
using LibraryMS.Application.Features.Members.Commands.Create;
using LibraryMS.Domain.Entities;
using LibraryMS.Domain.Repoistries;
using MediatR;

namespace LibraryMS.Application.Features.Members.Commands.Create;

public class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMainRepoistery<Member> _mainRepoistory;

    public CreateMemberCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IMainRepoistery<Member> mainRepoistory)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _mainRepoistory = mainRepoistory;
    }

    public async Task<int> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
    {
        if (request.Member == null)
            throw new ArgumentNullException(nameof(request.Member), "Member data cannot be null.");

        var member = _mapper.Map<Member>(request.Member);
        await _mainRepoistory.AddAsync(member);
        await _unitOfWork.SaveChangesAsync();
        return member.Id;
    }
}
