using AutoMapper;
using LibraryMS.Application.DTOs.Books;
using LibraryMS.Domain.Entities;
using LibraryMS.Domain.Repoistries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Application.Features.Books.Queries.GetBooks;

public class GetAllBookQueryHandler : IRequestHandler<GetAllBookQuery, IEnumerable<BookReadDto>>
{
    private readonly IMainRepoistery<LibraryMS.Domain.Entities.Book> _bookRepoistery;
    private readonly IMapper _mapper;
    public GetAllBookQueryHandler(IMainRepoistery<LibraryMS.Domain.Entities.Book> bookRepoistery, IMapper mapper)
    {
        _bookRepoistery = bookRepoistery;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BookReadDto>> Handle(GetAllBookQuery request, CancellationToken cancellationToken)
    {

        var books = await _bookRepoistery.GetAllAsync();
        return _mapper.Map<IEnumerable<BookReadDto>>(books);
    }
}
