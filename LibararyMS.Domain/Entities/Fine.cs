using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LibraryMS.Domain.Entities
{
    /// <summary>
    /// Represents a fine entity for tracking fines on borrow records
    /// Contains fine amount, payment status, and payment date
    /// </summary>
    public class Fine
    {
        #region Properties

        /// <summary>
        /// Unique identifier for the fine
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Foreign key to the borrow record this fine is associated with
        /// </summary>
        [Required(ErrorMessage = "Borrow record is required")]
        [Display(Name = "Borrow Record")]
        public int BorrowRecordId { get; set; }

        /// <summary>
        /// Amount of the fine
        /// </summary>
        [Required(ErrorMessage = "Fine amount is required")]
        [Range(0.01, 10000, ErrorMessage = "Fine amount must be between 0.01 and 10,000")]
        [DataType(DataType.Currency)]
        [Display(Name = "Fine Amount")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]
        public double Amount { get; set; }

        /// <summary>
        /// Indicates whether the fine has been paid
        /// </summary>
        [Required]
        [Display(Name = "Is Paid")]
        public bool IsPaid { get; set; } = false;

        /// <summary>
        /// Date and time when the fine was paid (null if not paid yet)
        /// </summary>
        [DataType(DataType.DateTime)]
        [Display(Name = "Payment Date")]
        [DisplayFormat(DataFormatString = "{0:g}", ApplyFormatInEditMode = true)]
        public DateTime? PaidAt { get; set; }

        #endregion

        #region Navigation Properties

        /// <summary>
        /// Navigation property to the associated borrow record
        /// </summary>
        [Display(Name = "Associated Borrow Record")]
        public BorrowRecord BorrowRecord { get; set; }

        #endregion
    }
}
