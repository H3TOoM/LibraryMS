using LibraryMS.Application.Features.Members.Commands.Delete;
using LibraryMS.Domain.Entities;
using LibraryMS.Domain.Repoistries;
using MediatR;

namespace LibraryMS.Application.Features.Members.Commands.Delete;

public class DeleteMemberCommandHandler : IRequestHandler<DeleteMemberCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMainRepoistery<Member> _mainRepoistory;

    public DeleteMemberCommandHandler(IUnitOfWork unitOfWork, IMainRepoistery<Member> mainRepoistory)
    {
        _unitOfWork = unitOfWork;
        _mainRepoistory = mainRepoistory;
    }

    public async Task<bool> Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
    {
        var member = await _mainRepoistory.GetByIdAsync(request.Id);
        if (member == null)
            return false;

        await _mainRepoistory.DeleteAsync(request.Id);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
