using LibraryMS.Application.DTOs.Books;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Application.Features.Books.Queries.GetBookById;

public record GetBookByIdQuery(int Id) : IRequest<BookReadDto>;
