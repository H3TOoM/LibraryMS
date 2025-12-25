using LibraryMS.Application.DTOs.Fines;
using LibraryMS.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibarayMS.Presentation.Controllers
{
    /// <summary>
    /// Controller for managing library fines
    /// Handles fine creation, payment, and fine-related queries
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FinesController : ControllerBase
    {
        #region Fields

        private readonly IFineService _fineService;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the FinesController
        /// </summary>
        /// <param name="fineService">Service for fine operations</param>
        public FinesController(IFineService fineService)
        {
            _fineService = fineService;
        }

        #endregion

        #region Read Operations

        /// <summary>
        /// Retrieves all fines in the system
        /// </summary>
        /// <returns>List of all fines</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var fines = await _fineService.GetAllAsync();
            return Ok(fines);
        }

        /// <summary>
        /// Retrieves a specific fine by ID
        /// </summary>
        /// <param name="id">Fine ID</param>
        /// <returns>Fine details or NotFound if fine doesn't exist</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var fine = await _fineService.GetByIdAsync(id);
            if (fine == null)
                return NotFound("Fine not found");

            return Ok(fine);
        }

        /// <summary>
        /// Retrieves fines associated with a specific borrow record
        /// </summary>
        /// <param name="borrowRecordId">Borrow record ID</param>
        /// <returns>List of fines for the specified borrow record</returns>
        [HttpGet("borrowrecord/{borrowRecordId}")]
        public async Task<IActionResult> GetByBorrowRecordId(int borrowRecordId)
        {
            var fines = await _fineService.GetByBorrowRecordIdAsync(borrowRecordId);
            return Ok(fines);
        }

        #endregion

        #region CRUD Operations

        /// <summary>
        /// Creates a new fine record
        /// </summary>
        /// <param name="dto">Fine creation data</param>
        /// <returns>Created fine ID</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FineCreateDto dto)
        {
            var fineId = await _fineService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = fineId }, new { Id = fineId });
        }

        /// <summary>
        /// Updates an existing fine's information
        /// </summary>
        /// <param name="id">Fine ID to update</param>
        /// <param name="dto">Updated fine information</param>
        /// <returns>Success or NotFound response</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FineUpdateDto dto)
        {
            var result = await _fineService.UpdateAsync(id, dto);
            if (!result)
                return NotFound("Fine not found");

            return Ok("Fine updated successfully");
        }

        /// <summary>
        /// Deletes a fine from the system
        /// </summary>
        /// <param name="id">Fine ID to delete</param>
        /// <returns>Success or NotFound response</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _fineService.DeleteAsync(id);
            if (!result)
                return NotFound("Fine not found");

            return Ok("Fine deleted successfully");
        }

        #endregion

        #region Fine-Specific Operations

        /// <summary>
        /// Marks a fine as paid and sets the payment date
        /// </summary>
        /// <param name="id">Fine ID to pay</param>
        /// <param name="request">Payment date information</param>
        /// <returns>Success or NotFound response</returns>
        [HttpPut("{id}/pay")]
        public async Task<IActionResult> PayFine(int id, [FromBody] PayFineRequest request)
        {
            var result = await _fineService.PayAsync(id, request.PaidAt);
            if (!result)
                return NotFound("Fine not found");

            return Ok("Fine paid successfully");
        }

        #endregion
    }

    #region Request/Response Models

    /// <summary>
    /// Pay fine request data transfer object
    /// </summary>
    /// <param name="PaidAt">Date and time when the fine was paid</param>
    public sealed record PayFineRequest(DateTime PaidAt);

    #endregion
}
