using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Application.Features.Books.Commands.DeleteBook;

public record DeleteBookCommand(int Id) : IRequest<bool>;
