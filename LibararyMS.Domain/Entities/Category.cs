using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LibraryMS.Domain.Entities
{
    /// <summary>
    /// Represents a book category entity for organizing books
    /// Contains category information for book classification
    /// </summary>
    public class Category
    {
        #region Properties

        /// <summary>
        /// Unique identifier for the category
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Name of the category
        /// </summary>
        [Required(ErrorMessage = "Category name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Category name must be between 2 and 50 characters")]
        [Display(Name = "Category Name")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Category name can only contain letters and spaces")]
        public string Name { get; set; }

        #endregion
    }
}
