using LibraryMS.Application.DTOs.Categories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Application.Features.Categories.Queries.GetAll;

public record GetAllQuery : IRequest<IEnumerable<CategoryReadDto>>;
