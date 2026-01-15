using AutoMapper;
using LibraryMS.Domain.Entities;
using LibraryMS.Domain.Repoistries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Application.Features.Categories.Commands.Delete;

public class DeleteCommandHandler : IRequestHandler<DeleteCommand, bool>
{
    private readonly IMainRepoistery<Category> _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteCommandHandler(IMainRepoistery<Category> categoryRepository, IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<bool> Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        var result = await _categoryRepository.DeleteAsync(request.Id);
        await _unitOfWork.SaveChangesAsync();
        return result;
    }
}
