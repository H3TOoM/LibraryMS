using LibraryMS.Application.DTOs.Categories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Application.Features.Categories.Queries.GetById;

public record GetByIdQuery(int Id) : IRequest<CategoryReadDto>;
