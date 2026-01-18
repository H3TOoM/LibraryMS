using LibraryMS.Application.Features.Fines.Commands.Delete;
using LibraryMS.Domain.Entities;
using LibraryMS.Domain.Repoistries;
using MediatR;

namespace LibraryMS.Application.Features.Fines.Commands.Delete;

public class DeleteFineCommandHandler : IRequestHandler<DeleteFineCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMainRepoistery<Fine> _mainRepoistory;

    public DeleteFineCommandHandler(IUnitOfWork unitOfWork, IMainRepoistery<Fine> mainRepoistory)
    {
        _unitOfWork = unitOfWork;
        _mainRepoistory = mainRepoistory;
    }

    public async Task<bool> Handle(DeleteFineCommand request, CancellationToken cancellationToken)
    {
        var fine = await _mainRepoistory.GetByIdAsync(request.Id);
        if (fine == null)
            return false;

        await _mainRepoistory.DeleteAsync(request.Id);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
