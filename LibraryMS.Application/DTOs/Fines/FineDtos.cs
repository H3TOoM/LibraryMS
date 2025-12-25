using System.ComponentModel.DataAnnotations;

namespace LibraryMS.Application.DTOs.Fines
{
    #region Fine Data Transfer Objects

    /// <summary>
    /// Data Transfer Object for creating new fines
    /// </summary>
    public sealed record FineCreateDto
    {
        /// <summary>
        /// ID of the borrow record this fine is associated with
        /// </summary>
        [Required(ErrorMessage = "Borrow record is required")]
        [Display(Name = "Borrow Record")]
        public int BorrowRecordId { get; init; }

        /// <summary>
        /// Fine amount
        /// </summary>
        [Required(ErrorMessage = "Fine amount is required")]
        [Range(0.01, 10000, ErrorMessage = "Fine amount must be between 0.01 and 10,000")]
        [DataType(DataType.Currency)]
        [Display(Name = "Fine Amount")]
        public double Amount { get; init; }
    }

    /// <summary>
    /// Data Transfer Object for updating existing fines
    /// </summary>
    public sealed record FineUpdateDto
    {
        /// <summary>
        /// Whether the fine has been paid
        /// </summary>
        [Required]
        [Display(Name = "Is Paid")]
        public bool IsPaid { get; init; }

        /// <summary>
        /// Date when the fine was paid (null if not paid)
        /// </summary>
        [DataType(DataType.DateTime)]
        [Display(Name = "Payment Date")]
        public DateTime? PaidAt { get; init; }
    }

    /// <summary>
    /// Data Transfer Object for reading fine information
    /// </summary>
    public sealed record FineReadDto(
        [property: Display(Name = "Fine ID")] int Id,
        [property: Required][property: Display(Name = "Borrow Record ID")] int BorrowRecordId,
        [property: Required][property: Display(Name = "Fine Amount")][property: DataType(DataType.Currency)] double Amount,
        [property: Required][property: Display(Name = "Is Paid")] bool IsPaid,
        [property: Display(Name = "Payment Date")][property: DataType(DataType.DateTime)] DateTime? PaidAt);

    #endregion
}
