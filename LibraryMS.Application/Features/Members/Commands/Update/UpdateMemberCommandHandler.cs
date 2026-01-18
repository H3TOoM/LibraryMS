using AutoMapper;
using LibraryMS.Application.Features.Members.Commands.Update;
using LibraryMS.Domain.Entities;
using LibraryMS.Domain.Repoistries;
using MediatR;

namespace LibraryMS.Application.Features.Members.Commands.Update;

public class UpdateMemberCommandHandler : IRequestHandler<UpdateMemberCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMainRepoistery<Member> _mainRepoistory;

    public UpdateMemberCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IMainRepoistery<Member> mainRepoistory)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _mainRepoistory = mainRepoistory;
    }

    public async Task<bool> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
    {
        var member = await _mainRepoistory.GetByIdAsync(request.Id);
        if (member == null)
            return false;

        _mapper.Map(request.Member, member);
        await _mainRepoistory.UpdateAsync(request.Id, member);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
