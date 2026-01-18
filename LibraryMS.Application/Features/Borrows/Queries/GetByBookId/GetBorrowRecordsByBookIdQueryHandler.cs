using AutoMapper;
using LibraryMS.Application.DTOs.BorrowRecords;
using LibraryMS.Application.Features.Borrows.Queries.GetByBookId;
using LibraryMS.Domain.Repoistries;
using MediatR;

namespace LibraryMS.Application.Features.Borrows.Queries.GetByBookId;

public class GetBorrowRecordsByBookIdQueryHandler : IRequestHandler<GetBorrowRecordsByBookIdQuery, IEnumerable<BorrowRecordReadDto>>
{
    private readonly IMapper _mapper;
    private readonly IBorrowRepoistory _borrowRepoistory;

    public GetBorrowRecordsByBookIdQueryHandler(IMapper mapper, IBorrowRepoistory borrowRepoistory)
    {
        _mapper = mapper;
        _borrowRepoistory = borrowRepoistory;
    }

    public async Task<IEnumerable<BorrowRecordReadDto>> Handle(GetBorrowRecordsByBookIdQuery request, CancellationToken cancellationToken)
    {
        var borrowRecords = await _borrowRepoistory.GetByBookIdAsync(request.BookId);
        return _mapper.Map<IEnumerable<BorrowRecordReadDto>>(borrowRecords);
    }
}
