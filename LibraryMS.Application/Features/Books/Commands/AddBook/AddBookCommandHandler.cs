using AutoMapper;
using LibraryMS.Application.Features.Book.Commands.AddBook;
using LibraryMS.Domain.Entities;
using LibraryMS.Domain.Repoistries;
using MediatR;
using System.Collections.Generic;

namespace LibraryMS.Application.Features.Books.Commands.AddBook;

public class AddBookCommandHandler : IRequestHandler<AddBookCommand, int>
{
    private readonly IMainRepoistery<LibraryMS.Domain.Entities.Book> _mainRepoistory;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public AddBookCommandHandler(IMainRepoistery<LibraryMS.Domain.Entities.Book> mainRepoistory, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mainRepoistory = mainRepoistory;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<int> Handle(AddBookCommand request, CancellationToken cancellationToken)
    {
        if(request.Book == null)
            throw new ArgumentNullException(nameof(request.Book), "Book data cannot be null.");

        var bookEntity = _mapper.Map<LibraryMS.Domain.Entities.Book>(request.Book);
        await _mainRepoistory.AddAsync(bookEntity);
        await _unitOfWork.SaveChangesAsync();

        return bookEntity.Id;
    }
}
