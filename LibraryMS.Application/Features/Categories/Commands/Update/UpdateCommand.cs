using LibraryMS.Application.DTOs.Categories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Application.Features.Categories.Commands.Update;

public record UpdateCommand(int Id , CategoryUpdateDto Request) : IRequest<bool>;
