using AutoMapper;
using LibraryMS.Application.DTOs.Categories;
using LibraryMS.Application.Interfaces;
using LibraryMS.Domain.Entities;
using LibraryMS.Domain.Repoistries;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMainRepoistery<Category> _mainRepoistory;
        private readonly IMapper _mapper;
        public CategoryService(IUnitOfWork unitOfWork, IMainRepoistery<Category> mainRepoistory, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mainRepoistory = mainRepoistory;
            _mapper = mapper;
        }
        public async Task<int> CreateAsync(CategoryCreateDto dto)
        {
            var category = _mapper.Map<Category>(dto);
            await _mainRepoistory.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();
            return category.Id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _mainRepoistory.GetByIdAsync(id);
            if (category == null)
                return false;
            await _mainRepoistory.DeleteAsync(category);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CategoryReadDto>> GetAllAsync()
        {
            var categories = await _mainRepoistory.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryReadDto>>(categories);
        }

        public async Task<CategoryReadDto?> GetByIdAsync(int id)
        {
            var category = await _mainRepoistory.GetByIdAsync(id);
            return _mapper.Map<CategoryReadDto?>(category);
        }

        public async Task<bool> UpdateAsync(int id, CategoryUpdateDto dto)
        {
            var category = await _mainRepoistory.GetByIdAsync(id);
            if (category == null)
                return false;
            _mapper.Map(dto, category);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
