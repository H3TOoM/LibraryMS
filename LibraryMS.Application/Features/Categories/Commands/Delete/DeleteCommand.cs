using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Application.Features.Categories.Commands.Delete;

public record DeleteCommand(int Id) : IRequest<bool>;
