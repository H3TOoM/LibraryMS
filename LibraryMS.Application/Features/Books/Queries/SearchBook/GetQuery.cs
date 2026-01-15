using LibraryMS.Application.DTOs.Books;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Application.Features.Books.Queries.SearchBook;

public record SearchByTitle(string? Title) : IRequest<IEnumerable<BookReadDto>>;
