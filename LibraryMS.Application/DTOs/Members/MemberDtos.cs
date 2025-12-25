using System.ComponentModel.DataAnnotations;

namespace LibraryMS.Application.DTOs.Members
{
    #region Member Data Transfer Objects

    /// <summary>
    /// Data Transfer Object for creating new members
    /// </summary>
    public sealed record MemberCreateDto
    {
        /// <summary>
        /// Member full name
        /// </summary>
        [Required(ErrorMessage = "Member name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
        [Display(Name = "Full Name")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name can only contain letters and spaces")]
        public string Name { get; init; }

        /// <summary>
        /// Member email address
        /// </summary>
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [StringLength(255, ErrorMessage = "Email address cannot exceed 255 characters")]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; init; }

        /// <summary>
        /// Member phone number
        /// </summary>
        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Please enter a valid phone number")]
        [StringLength(20, MinimumLength = 10, ErrorMessage = "Phone number must be between 10 and 20 characters")]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; init; }

        /// <summary>
        /// Member password (will be hashed)
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; init; }

        /// <summary>
        /// Maximum number of books the member can borrow
        /// </summary>
        [Required(ErrorMessage = "Borrow limit is required")]
        [Range(1, 20, ErrorMessage = "Borrow limit must be between 1 and 20")]
        [Display(Name = "Maximum Borrow Limit")]
        public int MaxBorrowLimit { get; init; }
    }

    /// <summary>
    /// Data Transfer Object for updating existing members
    /// </summary>
    public sealed record MemberUpdateDto
    {
        /// <summary>
        /// Member full name
        /// </summary>
        [Required(ErrorMessage = "Member name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
        [Display(Name = "Full Name")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name can only contain letters and spaces")]
        public string Name { get; init; }

        /// <summary>
        /// Member email address
        /// </summary>
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [StringLength(255, ErrorMessage = "Email address cannot exceed 255 characters")]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; init; }

        /// <summary>
        /// Member phone number
        /// </summary>
        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Please enter a valid phone number")]
        [StringLength(20, MinimumLength = 10, ErrorMessage = "Phone number must be between 10 and 20 characters")]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; init; }

        /// <summary>
        /// Maximum number of books the member can borrow
        /// </summary>
        [Required(ErrorMessage = "Borrow limit is required")]
        [Range(1, 20, ErrorMessage = "Borrow limit must be between 1 and 20")]
        [Display(Name = "Maximum Borrow Limit")]
        public int MaxBorrowLimit { get; init; }

        /// <summary>
        /// Member role (e.g., Admin, Member)
        /// </summary>
        [Required(ErrorMessage = "Role is required")]
        [StringLength(20, ErrorMessage = "Role cannot exceed 20 characters")]
        [Display(Name = "User Role")]
        public string Role { get; init; }
    }

    /// <summary>
    /// Data Transfer Object for reading member information
    /// </summary>
    public sealed record MemberReadDto(
        [property: Display(Name = "Member ID")] int Id,
        [property: Required][property: Display(Name = "Full Name")] string Name,
        [property: Required][property: Display(Name = "Email Address")][property: DataType(DataType.EmailAddress)] string Email,
        [property: Required][property: Display(Name = "Phone Number")][property: DataType(DataType.PhoneNumber)] string Phone,
        [property: Required][property: Display(Name = "User Role")] string Role,
        [property: Required][property: Display(Name = "Registration Date")][property: DataType(DataType.DateTime)] DateTime CreatedAt,
        [property: Required][property: Display(Name = "Maximum Borrow Limit")] int MaxBorrowLimit);

    #endregion
}
