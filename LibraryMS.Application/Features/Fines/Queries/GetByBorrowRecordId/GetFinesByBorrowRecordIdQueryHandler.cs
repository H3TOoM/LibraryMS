using AutoMapper;
using LibraryMS.Application.DTOs.Fines;
using LibraryMS.Application.Features.Fines.Queries.GetByBorrowRecordId;
using LibraryMS.Domain.Entities;
using LibraryMS.Domain.Repoistries;
using MediatR;

namespace LibraryMS.Application.Features.Fines.Queries.GetByBorrowRecordId;

public class GetFinesByBorrowRecordIdQueryHandler : IRequestHandler<GetFinesByBorrowRecordIdQuery, IEnumerable<FineReadDto>>
{
    private readonly IMapper _mapper;
    private readonly IMainRepoistery<Fine> _mainRepoistory;

    public GetFinesByBorrowRecordIdQueryHandler(IMapper mapper, IMainRepoistery<Fine> mainRepoistory)
    {
        _mapper = mapper;
        _mainRepoistory = mainRepoistory;
    }

    public async Task<IEnumerable<FineReadDto>> Handle(GetFinesByBorrowRecordIdQuery request, CancellationToken cancellationToken)
    {
        var fines = await _mainRepoistory.GetByIdAsync(request.BorrowRecordId);
        return _mapper.Map<IEnumerable<FineReadDto>>(fines);
    }
}
