using LibraryMS.Application.DTOs.Members;
using LibraryMS.Application.Features.Members.Commands.Create;
using LibraryMS.Application.Features.Members.Commands.Update;
using LibraryMS.Application.Features.Members.Commands.Delete;
using LibraryMS.Application.Features.Members.Queries.GetAll;
using LibraryMS.Application.Features.Members.Queries.GetById;
using LibraryMS.Application.Features.Members.Queries.GetActiveBorrowCount;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibarayMS.Presentation.Controllers
{
    /// <summary>
    /// Controller for managing library members
    /// Provides CRUD operations and member-specific queries
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MembersController : ControllerBase
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the MembersController
        /// </summary>
        /// <param name="mediator">Mediator for handling requests</param>
        public MembersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region CRUD Operations

        /// <summary>
        /// Retrieves all library members
        /// </summary>
        /// <returns>List of all members</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var members = await _mediator.Send(new GetAllMembersQuery());
            return Ok(members);
        }

        /// <summary>
        /// Retrieves a specific member by ID
        /// </summary>
        /// <param name="id">Member ID</param>
        /// <returns>Member details or NotFound if member doesn't exist</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var member = await _mediator.Send(new GetMemberByIdQuery(id));
            if (member == null)
                return NotFound("Member not found");

            return Ok(member);
        }

        /// <summary>
        /// Creates a new library member
        /// </summary>
        /// <param name="dto">Member creation data</param>
        /// <returns>Created member ID</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] MemberCreateDto dto)
        {
            var memberId = await _mediator.Send(new CreateMemberCommand(dto));
            return CreatedAtAction(nameof(GetById), new { id = memberId }, new { Id = memberId });
        }

        /// <summary>
        /// Updates an existing member's information
        /// </summary>
        /// <param name="id">Member ID to update</param>
        /// <param name="dto">Updated member information</param>
        /// <returns>Success or NotFound response</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MemberUpdateDto dto)
        {
            var result = await _mediator.Send(new UpdateMemberCommand(id, dto));
            if (!result)
                return NotFound("Member not found");

            return Ok("Member updated successfully");
        }

        /// <summary>
        /// Deletes a member from the library system
        /// </summary>
        /// <param name="id">Member ID to delete</param>
        /// <returns>Success or NotFound response</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteMemberCommand(id));
            if (!result)
                return NotFound("Member not found");

            return Ok("Member deleted successfully");
        }

        #endregion

        #region Member-Specific Queries

        /// <summary>
        /// Gets the count of active borrow records for a specific member
        /// </summary>
        /// <param name="id">Member ID</param>
        /// <returns>Count of active borrows</returns>
        [HttpGet("{id}/active-borrows")]
        public async Task<IActionResult> GetActiveBorrowCount(int id)
        {
            var count = await _mediator.Send(new GetActiveBorrowCountQuery(id));
            return Ok(new { ActiveBorrowCount = count });
        }

        #endregion
    }
}
