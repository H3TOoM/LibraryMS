using LibraryMS.Application.DTOs.Categories;

namespace LibraryMS.Application.Interfaces
{
    /// <summary>
    /// Interface for category management services
    /// Defines methods for CRUD operations on book categories
    /// </summary>
    public interface ICategoryService
    {
        #region CRUD Operations

        /// <summary>
        /// Retrieves all categories in the system
        /// </summary>
        /// <returns>Collection of category read DTOs</returns>
        Task<IEnumerable<CategoryReadDto>> GetAllAsync();

        /// <summary>
        /// Retrieves a specific category by ID
        /// </summary>
        /// <param name="id">Category ID</param>
        /// <returns>Category read DTO or null if not found</returns>
        Task<CategoryReadDto?> GetByIdAsync(int id);

        /// <summary>
        /// Creates a new book category
        /// </summary>
        /// <param name="dto">Category creation data</param>
        /// <returns>ID of the created category</returns>
        Task<int> CreateAsync(CategoryCreateDto dto);

        /// <summary>
        /// Updates an existing category's information
        /// </summary>
        /// <param name="id">Category ID to update</param>
        /// <param name="dto">Updated category information</param>
        /// <returns>True if update was successful, false if category not found</returns>
        Task<bool> UpdateAsync(int id, CategoryUpdateDto dto);

        /// <summary>
        /// Deletes a category from the system
        /// </summary>
        /// <param name="id">Category ID to delete</param>
        /// <returns>True if deletion was successful, false if category not found</returns>
        Task<bool> DeleteAsync(int id);

        #endregion
    }
}
