using AutoMapper;
using LibraryMS.Application.DTOs.BorrowRecords;
using LibraryMS.Application.Features.Borrows.Queries.GetById;
using LibraryMS.Domain.Entities;
using LibraryMS.Domain.Repoistries;
using MediatR;

namespace LibraryMS.Application.Features.Borrows.Queries.GetById;

public class GetBorrowRecordByIdQueryHandler : IRequestHandler<GetBorrowRecordByIdQuery, BorrowRecordReadDto?>
{
    private readonly IMapper _mapper;
    private readonly IMainRepoistery<BorrowRecord> _mainRepoistory;

    public GetBorrowRecordByIdQueryHandler(IMapper mapper, IMainRepoistery<BorrowRecord> mainRepoistory)
    {
        _mapper = mapper;
        _mainRepoistory = mainRepoistory;
    }

    public async Task<BorrowRecordReadDto?> Handle(GetBorrowRecordByIdQuery request, CancellationToken cancellationToken)
    {
        var borrowRecord = await _mainRepoistory.GetByIdAsync(request.Id);
        return _mapper.Map<BorrowRecordReadDto?>(borrowRecord);
    }
}
