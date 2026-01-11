using AutoMapper;
using LibraryMS.Domain.Repoistries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Application.Features.Books.Commands.UpdateBook;

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, int>
{
    private readonly IMainRepoistery<LibraryMS.Domain.Entities.Book> _mainRepoistory;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateBookCommandHandler(IMainRepoistery<LibraryMS.Domain.Entities.Book> mainRepoistory, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mainRepoistory = mainRepoistory;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<int> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var existingBook = await _mainRepoistory.GetByIdAsync(request.Id);
        if (existingBook == null) return 0;

        var bookEntity = _mapper.Map(request.Book, existingBook);
        await _mainRepoistory.UpdateAsync(request.Id, bookEntity);
        await _unitOfWork.SaveChangesAsync();
        return bookEntity.Id;
    }
}
