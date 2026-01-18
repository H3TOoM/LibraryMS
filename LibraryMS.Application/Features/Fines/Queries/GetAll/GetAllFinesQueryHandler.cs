using AutoMapper;
using LibraryMS.Application.DTOs.Fines;
using LibraryMS.Application.Features.Fines.Queries.GetAll;
using LibraryMS.Domain.Entities;
using LibraryMS.Domain.Repoistries;
using MediatR;

namespace LibraryMS.Application.Features.Fines.Queries.GetAll;

public class GetAllFinesQueryHandler : IRequestHandler<GetAllFinesQuery, IEnumerable<FineReadDto>>
{
    private readonly IMapper _mapper;
    private readonly IMainRepoistery<Fine> _mainRepoistory;

    public GetAllFinesQueryHandler(IMapper mapper, IMainRepoistery<Fine> mainRepoistory)
    {
        _mapper = mapper;
        _mainRepoistory = mainRepoistory;
    }

    public async Task<IEnumerable<FineReadDto>> Handle(GetAllFinesQuery request, CancellationToken cancellationToken)
    {
        var fines = await _mainRepoistory.GetAllAsync();
        return _mapper.Map<IEnumerable<FineReadDto>>(fines);
    }
}
