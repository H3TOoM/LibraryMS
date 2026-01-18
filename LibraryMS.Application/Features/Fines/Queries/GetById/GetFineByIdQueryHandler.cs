using AutoMapper;
using LibraryMS.Application.DTOs.Fines;
using LibraryMS.Application.Features.Fines.Queries.GetById;
using LibraryMS.Domain.Entities;
using LibraryMS.Domain.Repoistries;
using MediatR;

namespace LibraryMS.Application.Features.Fines.Queries.GetById;

public class GetFineByIdQueryHandler : IRequestHandler<GetFineByIdQuery, FineReadDto?>
{
    private readonly IMapper _mapper;
    private readonly IMainRepoistery<Fine> _mainRepoistory;

    public GetFineByIdQueryHandler(IMapper mapper, IMainRepoistery<Fine> mainRepoistory)
    {
        _mapper = mapper;
        _mainRepoistory = mainRepoistory;
    }

    public async Task<FineReadDto?> Handle(GetFineByIdQuery request, CancellationToken cancellationToken)
    {
        var fine = await _mainRepoistory.GetByIdAsync(request.Id);
        return _mapper.Map<FineReadDto?>(fine);
    }
}
