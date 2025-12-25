using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LibraryMS.Domain.Entities
{
    /// <summary>
    /// Represents a borrow record entity tracking book borrowing transactions
    /// Contains information about borrowed books, borrowers, and return status
    /// </summary>
    public class BorrowRecord
    {
        #region Properties

        /// <summary>
        /// Unique identifier for the borrow record
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Foreign key to the borrowed book
        /// </summary>
        [Required(ErrorMessage = "Book is required")]
        [Display(Name = "Book")]
        public int BookId { get; set; }

        /// <summary>
        /// Foreign key to the member who borrowed the book
        /// </summary>
        [Required(ErrorMessage = "Member is required")]
        [Display(Name = "Member")]
        public int MemberId { get; set; }

        /// <summary>
        /// Date and time when the book was borrowed
        /// </summary>
        [Required(ErrorMessage = "Borrow date is required")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Borrow Date")]
        [DisplayFormat(DataFormatString = "{0:g}", ApplyFormatInEditMode = true)]
        public DateTime BorrowedAt { get; set; }

        /// <summary>
        /// Due date for returning the book
        /// </summary>
        [Required(ErrorMessage = "Due date is required")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Due Date")]
        [DisplayFormat(DataFormatString = "{0:g}", ApplyFormatInEditMode = true)]
        public DateTime DueDate { get; set; }

        /// <summary>
        /// Date and time when the book was returned (null if not returned yet)
        /// </summary>
        [DataType(DataType.DateTime)]
        [Display(Name = "Return Date")]
        [DisplayFormat(DataFormatString = "{0:g}", ApplyFormatInEditMode = true)]
        public DateTime? ReturnedAt { get; set; }

        /// <summary>
        /// Calculated fine amount for late returns
        /// </summary>
        [Required(ErrorMessage = "Fine amount is required")]
        [Range(0, 10000, ErrorMessage = "Fine amount must be between 0 and 10,000")]
        [DataType(DataType.Currency)]
        [Display(Name = "Fine Amount")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]
        public double FineAmount { get; set; }

        #endregion

        #region Navigation Properties

        /// <summary>
        /// Navigation property to the borrowed book
        /// </summary>
        [Display(Name = "Borrowed Book")]
        public Book Book { get; set; }

        /// <summary>
        /// Navigation property to the borrowing member
        /// </summary>
        [Display(Name = "Borrowing Member")]
        public Member Member { get; set; }

        #endregion
    }
}
