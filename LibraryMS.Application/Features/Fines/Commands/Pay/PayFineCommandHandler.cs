using LibraryMS.Application.Features.Fines.Commands.Pay;
using LibraryMS.Domain.Entities;
using LibraryMS.Domain.Repoistries;
using MediatR;

namespace LibraryMS.Application.Features.Fines.Commands.Pay;

public class PayFineCommandHandler : IRequestHandler<PayFineCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMainRepoistery<Fine> _mainRepoistory;

    public PayFineCommandHandler(IUnitOfWork unitOfWork, IMainRepoistery<Fine> mainRepoistory)
    {
        _unitOfWork = unitOfWork;
        _mainRepoistory = mainRepoistory;
    }

    public async Task<bool> Handle(PayFineCommand request, CancellationToken cancellationToken)
    {
        var fine = await _mainRepoistory.GetByIdAsync(request.FineId);
        if (fine == null)
            return false;

        fine.IsPaid = true;
        fine.PaidAt = request.PaidAt;

        await _mainRepoistory.UpdateAsync(request.FineId, fine);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
