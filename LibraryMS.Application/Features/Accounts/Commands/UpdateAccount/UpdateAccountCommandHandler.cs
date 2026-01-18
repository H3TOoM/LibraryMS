using AutoMapper;
using LibraryMS.Application.DTOs.Members;
using LibraryMS.Application.Features.Accounts.Commands.UpdateAccount;
using LibraryMS.Domain.Entities;
using LibraryMS.Domain.Repoistries;
using MediatR;

namespace LibraryMS.Application.Features.Accounts.Commands.UpdateAccount;

public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand, bool>
{
    private readonly IMainRepoistery<Member> _mainRepoistory;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateAccountCommandHandler(IMainRepoistery<Member> mainRepoistory, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mainRepoistory = mainRepoistory;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        // Retrieve existing member
        var member = await _mainRepoistory.GetByIdAsync(request.Id);
        if (member == null) return false;

        // Update member with new data
        _mapper.Map(request.Member, member);
        await _mainRepoistory.UpdateAsync(request.Id, member);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
