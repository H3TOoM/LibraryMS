using AutoMapper;
using LibraryMS.Application.DTOs.Categories;
using LibraryMS.Domain.Entities;
using LibraryMS.Domain.Repoistries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Application.Features.Categories.Queries.GetById;

public class GetByIdHandler : IRequestHandler<GetByIdQuery, CategoryReadDto>
{
    private readonly IMainRepoistery<Category> _categoryRepoistory;
    private readonly IMapper _mapper;
    public GetByIdHandler(IMainRepoistery<Category> categoryRepoistory, IMapper mapper)
    {
        _categoryRepoistory = categoryRepoistory;
        _mapper = mapper;
    }
    public async Task<CategoryReadDto> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepoistory.GetByIdAsync(request.Id);
        return _mapper.Map<CategoryReadDto>(category);
    }
}
