using AutoMapper;
using LibraryMS.Application.DTOs.Fines;
using LibraryMS.Application.Features.Fines.Commands.Create;
using LibraryMS.Domain.Entities;
using LibraryMS.Domain.Repoistries;
using MediatR;

namespace LibraryMS.Application.Features.Fines.Commands.Create;

public class CreateFineCommandHandler : IRequestHandler<CreateFineCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMainRepoistery<Fine> _mainRepoistory;

    public CreateFineCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IMainRepoistery<Fine> mainRepoistory)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _mainRepoistory = mainRepoistory;
    }

    public async Task<int> Handle(CreateFineCommand request, CancellationToken cancellationToken)
    {
        if (request.Fine == null)
            throw new ArgumentNullException(nameof(request.Fine), "Fine data cannot be null.");

        var fine = _mapper.Map<Fine>(request.Fine);
        await _mainRepoistory.AddAsync(fine);
        await _unitOfWork.SaveChangesAsync();
        return fine.Id;
    }
}
