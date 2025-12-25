using System.ComponentModel.DataAnnotations;

namespace LibraryMS.Application.DTOs.Categories
{
    #region Category Data Transfer Objects

    /// <summary>
    /// Data Transfer Object for creating new categories
    /// </summary>
    public sealed record CategoryCreateDto
    {
        /// <summary>
        /// Category name
        /// </summary>
        [Required(ErrorMessage = "Category name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Category name must be between 2 and 50 characters")]
        [Display(Name = "Category Name")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Category name can only contain letters and spaces")]
        public string Name { get; init; }
    }

    /// <summary>
    /// Data Transfer Object for updating existing categories
    /// </summary>
    public sealed record CategoryUpdateDto
    {
        /// <summary>
        /// Updated category name
        /// </summary>
        [Required(ErrorMessage = "Category name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Category name must be between 2 and 50 characters")]
        [Display(Name = "Category Name")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Category name can only contain letters and spaces")]
        public string Name { get; init; }
    }

    /// <summary>
    /// Data Transfer Object for reading category information
    /// </summary>
    public sealed record CategoryReadDto(
        [property: Display(Name = "Category ID")] int Id,
        [property: Required][property: Display(Name = "Category Name")] string Name);

    #endregion
}
