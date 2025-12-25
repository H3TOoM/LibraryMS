using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LibraryMS.Domain.Entities
{
    /// <summary>
    /// Represents a library member entity
    /// Contains member personal information, authentication data, and borrowing limits
    /// </summary>
    public class Member
    {
        #region Properties

        /// <summary>
        /// Unique identifier for the member
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Full name of the member
        /// </summary>
        [Required(ErrorMessage = "Member name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
        [Display(Name = "Full Name")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name can only contain letters and spaces")]
        public string Name { get; set; }

        /// <summary>
        /// Email address of the member (used for authentication)
        /// </summary>
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [StringLength(255, ErrorMessage = "Email address cannot exceed 255 characters")]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// Phone number of the member
        /// </summary>
        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Please enter a valid phone number")]
        [StringLength(20, MinimumLength = 10, ErrorMessage = "Phone number must be between 10 and 20 characters")]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        /// <summary>
        /// Hashed password for authentication
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; }

        /// <summary>
        /// Role of the member in the system (Admin, Member, Guest)
        /// </summary>
        [Required(ErrorMessage = "Role is required")]
        [StringLength(20, ErrorMessage = "Role cannot exceed 20 characters")]
        [Display(Name = "User Role")]
        public string Role { get; set; } = "Member";

        /// <summary>
        /// Date and time when the member account was created
        /// </summary>
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Registration Date")]
        [DisplayFormat(DataFormatString = "{0:g}", ApplyFormatInEditMode = true)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        /// <summary>
        /// Maximum number of books the member can borrow simultaneously
        /// </summary>
        [Required(ErrorMessage = "Borrow limit is required")]
        [Range(1, 20, ErrorMessage = "Borrow limit must be between 1 and 20")]
        [Display(Name = "Maximum Borrow Limit")]
        public int MaxBorrowLimit { get; set; }

        #endregion
    }
}
