using AutoMapper;
using LibraryMS.Application.DTOs.Categories;
using LibraryMS.Domain.Entities;
using LibraryMS.Domain.Repoistries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Application.Features.Categories.Queries.GetAll;

public class GetAllQueryHandler : IRequestHandler<GetAllQuery, IEnumerable<CategoryReadDto>>
{
    private readonly IMainRepoistery<Category> _categoryRepository;
    private readonly IMapper _mapper;
    public GetAllQueryHandler(IMainRepoistery<Category> categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<CategoryReadDto>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        var categories = await _categoryRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CategoryReadDto>>(categories);
    }
}
