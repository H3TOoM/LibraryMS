using AutoMapper;
using LibraryMS.Application.DTOs.Books;
using LibraryMS.Domain.Repoistries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Application.Features.Books.Queries.SearchBook;

internal class GetQueryHandler : IRequestHandler<SearchByTitle, IEnumerable<BookReadDto>>
{
    private readonly IBookRepoistory _bookRepoistory;
    private readonly IMapper _mapper;
    public GetQueryHandler(IBookRepoistory bookRepoistory, IMapper mapper)
    {
        _bookRepoistory = bookRepoistory;
        _mapper = mapper;
    }
    public async Task<IEnumerable<BookReadDto>> Handle(SearchByTitle request, CancellationToken cancellationToken)
    {
        var books = await _bookRepoistory.SearchAsync(request.Title);
        return _mapper.Map<IEnumerable<BookReadDto>>(books);
    }
}
