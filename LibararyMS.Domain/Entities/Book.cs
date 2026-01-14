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

       
        [Key]
        public int Id { get; set; }

        
        [Required(ErrorMessage = "Book title is required")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 200 characters")]
        [Display(Name = "Book Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author name is required")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Author name must be between 1 and 100 characters")]
        [Display(Name = "Author Name")]
        public string Author { get; set; }

       
        [Required(ErrorMessage = "ISBN is required")]
        [StringLength(20, MinimumLength = 10, ErrorMessage = "ISBN must be between 10 and 20 characters")]
        [RegularExpression(@"^(?=(?:\D*\d){10}(?:(?:\D*\d){3})?$)[\d-]+$", ErrorMessage = "Invalid ISBN format")]
        [Display(Name = "ISBN")]
        public string ISBN { get; set; }

        
        [Required(ErrorMessage = "Published date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Published Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PublishedDate { get; set; }

        
        [Required(ErrorMessage = "Copies available is required")]
        [Range(0, 1000, ErrorMessage = "Copies available must be between 0 and 1000")]
        [Display(Name = "Available Copies")]
        public int CopiesAvailable { get; set; }

        
        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        #endregion

        #region Navigation Properties

        
        [Display(Name = "Book Category")]
        public Category Category { get; set; }

        #endregion
    }
}
