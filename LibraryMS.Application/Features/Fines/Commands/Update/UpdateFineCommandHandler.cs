using AutoMapper;
using LibraryMS.Application.DTOs.Fines;
using LibraryMS.Application.Features.Fines.Commands.Update;
using LibraryMS.Domain.Entities;
using LibraryMS.Domain.Repoistries;
using MediatR;

namespace LibraryMS.Application.Features.Fines.Commands.Update;

public class UpdateFineCommandHandler : IRequestHandler<UpdateFineCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMainRepoistery<Fine> _mainRepoistory;

    public UpdateFineCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IMainRepoistery<Fine> mainRepoistory)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _mainRepoistory = mainRepoistory;
    }

    public async Task<bool> Handle(UpdateFineCommand request, CancellationToken cancellationToken)
    {
        var fine = await _mainRepoistory.GetByIdAsync(request.Id);
        if (fine == null)
            return false;

        _mapper.Map(request.Fine, fine);
        await _mainRepoistory.UpdateAsync(request.Id, fine);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
