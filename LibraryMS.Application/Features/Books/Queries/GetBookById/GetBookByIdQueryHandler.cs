using AutoMapper;
using LibraryMS.Application.DTOs.Books;
using LibraryMS.Domain.Repoistries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Application.Features.Books.Queries.GetBookById;

internal class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookReadDto>
{
    private readonly IBookRepoistory _bookRepoistory;
    private readonly IMapper _mapper;
    public GetBookByIdQueryHandler(IBookRepoistory bookRepoistory, IMapper mapper)
    {
        _bookRepoistory = bookRepoistory;
        _mapper = mapper;
    }
    public async Task<BookReadDto> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        if (request.Id <= 0)
            throw new ArgumentNullException(nameof(request));

        var book = await _bookRepoistory.GetBookByIdAsync(request.Id);
        return _mapper.Map<BookReadDto>(book);
    }
}
