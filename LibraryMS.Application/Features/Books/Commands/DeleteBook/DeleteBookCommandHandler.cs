using LibraryMS.Domain.Repoistries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Application.Features.Books.Commands.DeleteBook;

public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, bool>
{
    private readonly IMainRepoistery<LibraryMS.Domain.Entities.Book> _mainRepoistory;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteBookCommandHandler(IMainRepoistery<LibraryMS.Domain.Entities.Book> mainRepoistory, IUnitOfWork unitOfWork)
    {
        _mainRepoistory = mainRepoistory;
        _unitOfWork = unitOfWork;
    }
    public async Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var result = await _mainRepoistory.DeleteAsync(request.Id);
        await _unitOfWork.SaveChangesAsync();
        return result;
    }
}
