using AutoMapper;
using LibraryMS.Application.DTOs.BorrowRecords;
using LibraryMS.Application.Features.Borrows.Queries.GetByMemberId;
using LibraryMS.Domain.Repoistries;
using MediatR;

namespace LibraryMS.Application.Features.Borrows.Queries.GetByMemberId;

public class GetBorrowRecordsByMemberIdQueryHandler : IRequestHandler<GetBorrowRecordsByMemberIdQuery, IEnumerable<BorrowRecordReadDto>>
{
    private readonly IMapper _mapper;
    private readonly IBorrowRepoistory _borrowRepoistory;

    public GetBorrowRecordsByMemberIdQueryHandler(IMapper mapper, IBorrowRepoistory borrowRepoistory)
    {
        _mapper = mapper;
        _borrowRepoistory = borrowRepoistory;
    }

    public async Task<IEnumerable<BorrowRecordReadDto>> Handle(GetBorrowRecordsByMemberIdQuery request, CancellationToken cancellationToken)
    {
        var borrowRecords = await _borrowRepoistory.GetByMemberIdAsync(request.MemberId);
        return _mapper.Map<IEnumerable<BorrowRecordReadDto>>(borrowRecords);
    }
}
