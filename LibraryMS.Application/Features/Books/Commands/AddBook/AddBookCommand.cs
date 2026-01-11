using LibraryMS.Application.DTOs.Books;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Application.Features.Book.Commands.AddBook;

public record AddBookCommand(BookCreateDto Book) : IRequest<int>;
