using LibraryMS.Application.DTOs.Books;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Application.Features.Books.Queries.GetByCategoryId;

public record GetByCategoryIdQuery(int CategoryId) : IRequest<IEnumerable<BookReadDto>>;
