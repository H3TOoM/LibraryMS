using System.ComponentModel.DataAnnotations;

namespace LibraryMS.Application.DTOs.Books
{
    #region Book Data Transfer Objects

    /// <summary>
    /// Data Transfer Object for creating new books
    /// </summary>
    public sealed record BookCreateDto
    {
        /// <summary>
        /// Book title
        /// </summary>
        [Required(ErrorMessage = "Book title is required")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 200 characters")]
        [Display(Name = "Book Title")]
        public string Title { get; init; }

        /// <summary>
        /// Book author
        /// </summary>
        [Required(ErrorMessage = "Author name is required")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Author name must be between 1 and 100 characters")]
        [Display(Name = "Author Name")]
        public string Author { get; init; }

        /// <summary>
        /// Book ISBN number
        /// </summary>
        [Required(ErrorMessage = "ISBN is required")]
        [StringLength(20, MinimumLength = 10, ErrorMessage = "ISBN must be between 10 and 20 characters")]
        [RegularExpression(@"^(?=(?:\D*\d){10}(?:(?:\D*\d){3})?$)[\d-]+$", ErrorMessage = "Invalid ISBN format")]
        [Display(Name = "ISBN")]
        public string ISBN { get; init; }

        /// <summary>
        /// Book publication date
        /// </summary>
        [Required(ErrorMessage = "Published date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Published Date")]
        public DateTime PublishedDate { get; init; }

        /// <summary>
        /// Number of available copies
        /// </summary>
        [Required(ErrorMessage = "Copies available is required")]
        [Range(0, 1000, ErrorMessage = "Copies available must be between 0 and 1000")]
        [Display(Name = "Available Copies")]
        public int CopiesAvailable { get; init; }

        /// <summary>
        /// ID of the book's category
        /// </summary>
        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Category")]
        public int CategoryId { get; init; }
    }

    /// <summary>
    /// Data Transfer Object for updating existing books
    /// </summary>
    public sealed record BookUpdateDto
    {
        /// <summary>
        /// Book title
        /// </summary>
        [Required(ErrorMessage = "Book title is required")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 200 characters")]
        [Display(Name = "Book Title")]
        public string Title { get; init; }

        /// <summary>
        /// Book author
        /// </summary>
        [Required(ErrorMessage = "Author name is required")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Author name must be between 1 and 100 characters")]
        [Display(Name = "Author Name")]
        public string Author { get; init; }

        /// <summary>
        /// Book ISBN number
        /// </summary>
        [Required(ErrorMessage = "ISBN is required")]
        [StringLength(20, MinimumLength = 10, ErrorMessage = "ISBN must be between 10 and 20 characters")]
        [RegularExpression(@"^(?=(?:\D*\d){10}(?:(?:\D*\d){3})?$)[\d-]+$", ErrorMessage = "Invalid ISBN format")]
        [Display(Name = "ISBN")]
        public string ISBN { get; init; }

        /// <summary>
        /// Book publication date
        /// </summary>
        [Required(ErrorMessage = "Published date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Published Date")]
        public DateTime PublishedDate { get; init; }

        /// <summary>
        /// Number of available copies
        /// </summary>
        [Required(ErrorMessage = "Copies available is required")]
        [Range(0, 1000, ErrorMessage = "Copies available must be between 0 and 1000")]
        [Display(Name = "Available Copies")]
        public int CopiesAvailable { get; init; }

        /// <summary>
        /// ID of the book's category
        /// </summary>
        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Category")]
        public int CategoryId { get; init; }
    }

    /// <summary>
    /// Data Transfer Object for reading book information
    /// </summary>
    public sealed record BookReadDto(
        [property: Display(Name = "Book ID")] int Id,
        [property: Required][property: Display(Name = "Book Title")] string Title,
        [property: Required][property: Display(Name = "Author Name")] string Author,
        [property: Required][property: Display(Name = "ISBN")] string ISBN,
        [property: Required][property: Display(Name = "Published Date")][property: DataType(DataType.Date)] DateTime PublishedDate,
        [property: Required][property: Display(Name = "Available Copies")] int CopiesAvailable,
        [property: Required][property: Display(Name = "Category ID")] int CategoryId,
        [property: Display(Name = "Category Name")] string? CategoryName);

    #endregion
}
