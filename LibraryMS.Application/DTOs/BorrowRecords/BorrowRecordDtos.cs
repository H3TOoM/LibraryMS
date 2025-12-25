using LibraryMS.Application.DTOs.Books;
using LibraryMS.Application.DTOs.Members;
using System.ComponentModel.DataAnnotations;

namespace LibraryMS.Application.DTOs.BorrowRecords
{
    #region Borrow Record Data Transfer Objects

    /// <summary>
    /// Data Transfer Object for creating new borrow records
    /// </summary>
    public sealed record BorrowRecordCreateDto
    {
        /// <summary>
        /// ID of the book being borrowed
        /// </summary>
        [Required(ErrorMessage = "Book selection is required")]
        [Display(Name = "Book")]
        public int BookId { get; init; }

        /// <summary>
        /// ID of the member borrowing the book
        /// </summary>
        [Required(ErrorMessage = "Member selection is required")]
        [Display(Name = "Member")]
        public int MemberId { get; init; }

        /// <summary>
        /// Due date for returning the book
        /// </summary>
        [Required(ErrorMessage = "Due date is required")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Due Date")]
        public DateTime DueDate { get; init; }
    }

    /// <summary>
    /// Data Transfer Object for updating borrow records
    /// </summary>
    public sealed record BorrowRecordUpdateDto
    {
        /// <summary>
        /// Updated due date
        /// </summary>
        [Required(ErrorMessage = "Due date is required")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Due Date")]
        public DateTime DueDate { get; init; }

        /// <summary>
        /// Return date (null if not returned yet)
        /// </summary>
        [DataType(DataType.DateTime)]
        [Display(Name = "Return Date")]
        public DateTime? ReturnedAt { get; init; }
    }

    /// <summary>
    /// Data Transfer Object for reading borrow record information
    /// </summary>
    public sealed record BorrowRecordReadDto(
        [property: Display(Name = "Record ID")] int Id,
        [property: Required][property: Display(Name = "Book ID")] int BookId,
        [property: Required][property: Display(Name = "Member ID")] int MemberId,
        [property: Required][property: Display(Name = "Borrow Date")][property: DataType(DataType.DateTime)] DateTime BorrowedAt,
        [property: Required][property: Display(Name = "Due Date")][property: DataType(DataType.DateTime)] DateTime DueDate,
        [property: Display(Name = "Return Date")][property: DataType(DataType.DateTime)] DateTime? ReturnedAt,
        [property: Required][property: Display(Name = "Fine Amount")][property: DataType(DataType.Currency)] double FineAmount,
        [property: Display(Name = "Book Details")] BookReadDto? Book,
        [property: Display(Name = "Member Details")] MemberReadDto? Member);

    #endregion
}
