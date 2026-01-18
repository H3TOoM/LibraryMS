using LibraryMS.Application.Features.Borrows.Commands.Return;
using LibraryMS.Domain.Repoistries;
using MediatR;

namespace LibraryMS.Application.Features.Borrows.Commands.Return;

public class ReturnBookCommandHandler : IRequestHandler<ReturnBookCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBorrowRepoistory _borrowRepoistory;

    public ReturnBookCommandHandler(IUnitOfWork unitOfWork, IBorrowRepoistory borrowRepoistory)
    {
        _unitOfWork = unitOfWork;
        _borrowRepoistory = borrowRepoistory;
    }

    public async Task<bool> Handle(ReturnBookCommand request, CancellationToken cancellationToken)
    {
        var result = await _borrowRepoistory.ReturnAsync(request.BorrowRecordId, request.ReturnedAt);
        if (!result) return false;

        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
