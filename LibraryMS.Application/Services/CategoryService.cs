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
    /// <summary>
    /// Service responsible for category management operations
    /// Handles CRUD operations for book categories
    /// </summary>
    public class CategoryService : ICategoryService
    {
        #region Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMainRepoistery<Category> _mainRepoistory;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the CategoryService
        /// </summary>
        /// <param name="unitOfWork">Unit of work for transaction management</param>
        /// <param name="mainRepoistory">Main repository for category entities</param>
        /// <param name="mapper">AutoMapper instance for object mapping</param>
        public CategoryService(IUnitOfWork unitOfWork, IMainRepoistery<Category> mainRepoistory, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mainRepoistory = mainRepoistory;
            _mapper = mapper;
        }

        #endregion
        #region CRUD Operations

        /// <summary>
        /// Creates a new book category
        /// </summary>
        /// <param name="dto">Category creation data</param>
        /// <returns>ID of the created category</returns>
        public async Task<int> CreateAsync(CategoryCreateDto dto)
        {
            var category = _mapper.Map<Category>(dto);
            await _mainRepoistory.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();
            return category.Id;
        }

        /// <summary>
        /// Deletes a category from the system
        /// </summary>
        /// <param name="id">Category ID to delete</param>
        /// <returns>True if deletion was successful, false if category not found</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _mainRepoistory.GetByIdAsync(id);
            if (category == null)
                return false;

            await _mainRepoistory.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Retrieves all categories in the system
        /// </summary>
        /// <returns>Collection of category read DTOs</returns>
        public async Task<IEnumerable<CategoryReadDto>> GetAllAsync()
        {
            var categories = await _mainRepoistory.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryReadDto>>(categories);
        }

        /// <summary>
        /// Retrieves a specific category by ID
        /// </summary>
        /// <param name="id">Category ID</param>
        /// <returns>Category read DTO or null if not found</returns>
        public async Task<CategoryReadDto?> GetByIdAsync(int id)
        {
            var category = await _mainRepoistory.GetByIdAsync(id);
            return _mapper.Map<CategoryReadDto?>(category);
        }

        /// <summary>
        /// Updates an existing category's information
        /// </summary>
        /// <param name="id">Category ID to update</param>
        /// <param name="dto">Updated category information</param>
        /// <returns>True if update was successful, false if category not found</returns>
        public async Task<bool> UpdateAsync(int id, CategoryUpdateDto dto)
        {
            var category = await _mainRepoistory.GetByIdAsync(id);
            if (category == null)
                return false;

            _mapper.Map(dto, category);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        #endregion
    }
}
