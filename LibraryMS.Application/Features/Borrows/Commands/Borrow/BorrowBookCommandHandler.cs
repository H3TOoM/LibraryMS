using AutoMapper;
using LibraryMS.Application.DTOs.BorrowRecords;
using LibraryMS.Application.Features.Borrows.Commands.Borrow;
using LibraryMS.Domain.Entities;
using LibraryMS.Domain.Repoistries;
using MediatR;

namespace LibraryMS.Application.Features.Borrows.Commands.Borrow;

public class BorrowBookCommandHandler : IRequestHandler<BorrowBookCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMainRepoistery<BorrowRecord> _mainRepoistory;

    public BorrowBookCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IMainRepoistery<BorrowRecord> mainRepoistory)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _mainRepoistory = mainRepoistory;
    }

    public async Task<int> Handle(BorrowBookCommand request, CancellationToken cancellationToken)
    {
        if (request.BorrowRecord == null)
            throw new ArgumentNullException(nameof(request.BorrowRecord), "Borrow record data cannot be null.");

        var borrowRecord = _mapper.Map<BorrowRecord>(request.BorrowRecord);
        await _mainRepoistory.AddAsync(borrowRecord);
        await _unitOfWork.SaveChangesAsync();
        return borrowRecord.Id;
    }
}
