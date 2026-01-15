using LibraryMS.Application.DTOs.Categories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Application.Features.Categories.Commands.Create;

public record CreateCommand(CategoryCreateDto Request) : IRequest<int>;
