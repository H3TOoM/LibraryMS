using LibraryMS.Application.DTOs.BorrowRecords;
using LibraryMS.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibarayMS.Presentation.Controllers
{
    /// <summary>
    /// Controller for managing book borrowing records
    /// Handles book borrowing, returning, and borrow record queries
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BorrowRecordsController : ControllerBase
    {
        #region Fields

        private readonly IBorrowService _borrowService;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the BorrowRecordsController
        /// </summary>
        /// <param name="borrowService">Service for borrow record operations</param>
        public BorrowRecordsController(IBorrowService borrowService)
        {
            _borrowService = borrowService;
        }

        #endregion

        #region Read Operations

        /// <summary>
        /// Retrieves all borrow records in the system
        /// </summary>
        /// <returns>List of all borrow records</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var borrowRecords = await _borrowService.GetAllAsync();
            return Ok(borrowRecords);
        }

        /// <summary>
        /// Retrieves a specific borrow record by ID
        /// </summary>
        /// <param name="id">Borrow record ID</param>
        /// <returns>Borrow record details or NotFound if record doesn't exist</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var borrowRecord = await _borrowService.GetByIdAsync(id);
            if (borrowRecord == null)
                return NotFound("Borrow record not found");

            return Ok(borrowRecord);
        }

        /// <summary>
        /// Retrieves all borrow records for a specific book
        /// </summary>
        /// <param name="bookId">Book ID</param>
        /// <returns>List of borrow records for the specified book</returns>
        [HttpGet("book/{bookId}")]
        public async Task<IActionResult> GetByBookId(int bookId)
        {
            var borrowRecords = await _borrowService.GetByBookIdAsync(bookId);
            return Ok(borrowRecords);
        }

        /// <summary>
        /// Retrieves all borrow records for a specific member
        /// </summary>
        /// <param name="memberId">Member ID</param>
        /// <returns>List of borrow records for the specified member</returns>
        [HttpGet("member/{memberId}")]
        public async Task<IActionResult> GetByMemberId(int memberId)
        {
            var borrowRecords = await _borrowService.GetByMemberIdAsync(memberId);
            return Ok(borrowRecords);
        }

        #endregion

        #region Borrow Operations

        /// <summary>
        /// Creates a new borrow record (borrows a book)
        /// </summary>
        /// <param name="dto">Borrow record creation data</param>
        /// <returns>Created borrow record ID</returns>
        [HttpPost]
        public async Task<IActionResult> BorrowBook([FromBody] BorrowRecordCreateDto dto)
        {
            var borrowRecordId = await _borrowService.BorrowAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = borrowRecordId }, new { Id = borrowRecordId });
        }

        /// <summary>
        /// Marks a borrow record as returned
        /// </summary>
        /// <param name="borrowRecordId">Borrow record ID to return</param>
        /// <param name="request">Return date information</param>
        /// <returns>Success or NotFound response</returns>
        [HttpPut("{borrowRecordId}/return")]
        public async Task<IActionResult> ReturnBook(int borrowRecordId, [FromBody] ReturnBookRequest request)
        {
            var result = await _borrowService.ReturnAsync(borrowRecordId, request.ReturnedAt);
            if (!result)
                return NotFound("Borrow record not found or already returned");

            return Ok("Book returned successfully");
        }

        #endregion
    }

    #region Request/Response Models

    /// <summary>
    /// Return book request data transfer object
    /// </summary>
    /// <param name="ReturnedAt">Date and time when the book was returned</param>
    public sealed record ReturnBookRequest(DateTime ReturnedAt);

    #endregion
}
