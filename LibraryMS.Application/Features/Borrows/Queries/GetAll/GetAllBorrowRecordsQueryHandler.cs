using AutoMapper;
using LibraryMS.Application.DTOs.BorrowRecords;
using LibraryMS.Application.Features.Borrows.Queries.GetAll;
using LibraryMS.Domain.Entities;
using LibraryMS.Domain.Repoistries;
using MediatR;

namespace LibraryMS.Application.Features.Borrows.Queries.GetAll;

public class GetAllBorrowRecordsQueryHandler : IRequestHandler<GetAllBorrowRecordsQuery, IEnumerable<BorrowRecordReadDto>>
{
    private readonly IMapper _mapper;
    private readonly IMainRepoistery<BorrowRecord> _mainRepoistory;

    public GetAllBorrowRecordsQueryHandler(IMapper mapper, IMainRepoistery<BorrowRecord> mainRepoistory)
    {
        _mapper = mapper;
        _mainRepoistory = mainRepoistory;
    }

    public async Task<IEnumerable<BorrowRecordReadDto>> Handle(GetAllBorrowRecordsQuery request, CancellationToken cancellationToken)
    {
        var borrowRecords = await _mainRepoistory.GetAllAsync();
        return _mapper.Map<IEnumerable<BorrowRecordReadDto>>(borrowRecords);
    }
}
