using LibraryMS.Application.DTOs.Categories;
using LibraryMS.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibarayMS.Presentation.Controllers
{
    /// <summary>
    /// Controller for managing book categories
    /// Provides CRUD operations for library book categories
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        #region Fields

        private readonly ICategoryService _categoryService;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the CategoriesController
        /// </summary>
        /// <param name="categoryService">Service for category operations</param>
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        #endregion

        #region Read Operations

        /// <summary>
        /// Retrieves all book categories
        /// </summary>
        /// <returns>List of all categories</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        /// <summary>
        /// Retrieves a specific category by ID
        /// </summary>
        /// <param name="id">Category ID</param>
        /// <returns>Category details or NotFound if category doesn't exist</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
                return NotFound("Category not found");

            return Ok(category);
        }

        #endregion

        #region CRUD Operations

        /// <summary>
        /// Creates a new book category
        /// </summary>
        /// <param name="dto">Category creation data</param>
        /// <returns>Created category ID</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CategoryCreateDto dto)
        {
            var categoryId = await _categoryService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = categoryId }, new { Id = categoryId });
        }

        /// <summary>
        /// Updates an existing category's information
        /// </summary>
        /// <param name="id">Category ID to update</param>
        /// <param name="dto">Updated category information</param>
        /// <returns>Success or NotFound response</returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryUpdateDto dto)
        {
            var result = await _categoryService.UpdateAsync(id, dto);
            if (!result)
                return NotFound("Category not found");

            return Ok("Category updated successfully");
        }

        /// <summary>
        /// Deletes a category from the system
        /// </summary>
        /// <param name="id">Category ID to delete</param>
        /// <returns>Success or NotFound response</returns>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _categoryService.DeleteAsync(id);
            if (!result)
                return NotFound("Category not found");

            return Ok("Category deleted successfully");
        }

        #endregion
    }
}
