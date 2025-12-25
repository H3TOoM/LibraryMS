using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LibraryMS.Domain.Entities
{
    /// <summary>
    /// Represents a book entity in the library system
    /// Contains book information and relationships with categories
    /// </summary>
    public class Book
    {
        #region Properties

        /// <summary>
        /// Unique identifier for the book
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Title of the book
        /// </summary>
        [Required(ErrorMessage = "Book title is required")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 200 characters")]
        [Display(Name = "Book Title")]
        public string Title { get; set; }

        /// <summary>
        /// Author of the book
        /// </summary>
        [Required(ErrorMessage = "Author name is required")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Author name must be between 1 and 100 characters")]
        [Display(Name = "Author Name")]
        public string Author { get; set; }

        /// <summary>
        /// International Standard Book Number
        /// </summary>
        [Required(ErrorMessage = "ISBN is required")]
        [StringLength(20, MinimumLength = 10, ErrorMessage = "ISBN must be between 10 and 20 characters")]
        [RegularExpression(@"^(?=(?:\D*\d){10}(?:(?:\D*\d){3})?$)[\d-]+$", ErrorMessage = "Invalid ISBN format")]
        [Display(Name = "ISBN")]
        public string ISBN { get; set; }

        /// <summary>
        /// Date when the book was published
        /// </summary>
        [Required(ErrorMessage = "Published date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Published Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PublishedDate { get; set; }

        /// <summary>
        /// Number of available copies for borrowing
        /// </summary>
        [Required(ErrorMessage = "Copies available is required")]
        [Range(0, 1000, ErrorMessage = "Copies available must be between 0 and 1000")]
        [Display(Name = "Available Copies")]
        public int CopiesAvailable { get; set; }

        /// <summary>
        /// Foreign key to the book's category
        /// </summary>
        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        #endregion

        #region Navigation Properties

        /// <summary>
        /// Navigation property to the book's category
        /// </summary>
        [Display(Name = "Book Category")]
        public Category Category { get; set; }

        #endregion
    }
}
