using AutoMapper;
using LibraryMS.Domain.Entities;
using LibraryMS.Domain.Repoistries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Application.Features.Categories.Commands.Create;

public class CreateCommandHandler : IRequestHandler<CreateCommand, int>
{
    private readonly IMainRepoistery<Category> _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CreateCommandHandler(IMainRepoistery<Category> categoryRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<int> Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        var category = _mapper.Map<Category>(request.Request);
        await _categoryRepository.AddAsync(category);
        await _unitOfWork.SaveChangesAsync();
        return category.Id;
    }
}
