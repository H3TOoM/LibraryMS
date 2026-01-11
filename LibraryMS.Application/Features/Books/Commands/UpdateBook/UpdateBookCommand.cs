using LibraryMS.Application.DTOs.Books;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Application.Features.Books.Commands.UpdateBook;

public record UpdateBookCommand(int Id, BookUpdateDto Book) : IRequest<int>;
