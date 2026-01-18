using LibraryMS.Application.DTOs.Auth;
using LibraryMS.Application.DTOs.Members;
using LibraryMS.Application.Features.Accounts.Commands.CreateAccount;
using LibraryMS.Application.Features.Accounts.Commands.UpdateAccount;
using LibraryMS.Application.Features.Accounts.Commands.Login;
using LibraryMS.Application.Features.Accounts.Queries.IsEmailTaken;
using LibraryMS.Application.Features.Members.Queries.GetMemberEntity;
using LibraryMS.Application.Features.Tokens.Commands.GenerateToken;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibarayMS.Presentation.Controllers
{
    /// <summary>
    /// Controller responsible for authentication and account management operations
    /// Handles user registration, login, and account updates
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the AccountController
        /// </summary>
        /// <param name="mediator">Mediator for handling requests</param>
        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region Authentication Methods

        /// <summary>
        /// Registers a new member account and returns authentication token
        /// </summary>
        /// <param name="dto">Member registration data</param>
        /// <returns>Authentication response with token and member details</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] MemberCreateDto dto)
        {
            try
            {
                // Create the account and get member ID
                var memberId = await _mediator.Send(new CreateAccountCommand(dto));

                // Retrieve the created member entity
                var member = await _mediator.Send(new GetMemberEntityQuery(memberId));

                if (member == null)
                    return BadRequest("Failed to create account");

                // Generate JWT token for the new member
                var tokenResult = await _mediator.Send(new GenerateTokenCommand(member));

                // Prepare authentication response
                var response = new AuthResponseDto
                {
                    Token = tokenResult.Token,
                    Expiration = tokenResult.ExpiresAt,
                    Member = new MemberReadDto(
                        member.Id,
                        member.Name,
                        member.Email,
                        member.Phone,
                        member.Role,
                        member.CreatedAt,
                        member.MaxBorrowLimit)
                };

                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Authenticates a member and returns JWT token
        /// </summary>
        /// <param name="dto">Login credentials (email and password)</param>
        /// <returns>Authentication response with token and member details</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            try
            {
                // Authenticate user and get member ID
                var memberId = await _mediator.Send(new LoginCommand(dto.Email, dto.Password));

                // Retrieve the authenticated member
                var member = await _mediator.Send(new GetMemberEntityQuery(memberId));

                if (member == null)
                    return Unauthorized("Invalid credentials");

                // Generate JWT token for authentication
                var tokenResult = await _mediator.Send(new GenerateTokenCommand(member));

                // Prepare authentication response
                var response = new AuthResponseDto
                {
                    Token = tokenResult.Token,
                    Expiration = tokenResult.ExpiresAt,
                    Member = new MemberReadDto(
                        member.Id,
                        member.Name,
                        member.Email,
                        member.Phone,
                        member.Role,
                        member.CreatedAt,
                        member.MaxBorrowLimit)
                };

                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region Account Management Methods

        /// <summary>
        /// Updates an existing member account information
        /// </summary>
        /// <param name="id">Member ID to update</param>
        /// <param name="dto">Updated member information</param>
        /// <returns>Success or not found response</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(int id, [FromBody] MemberUpdateDto dto)
        {
            var result = await _mediator.Send(new UpdateAccountCommand(id, dto));
            if (!result)
                return NotFound("Member not found");

            return Ok("Account updated successfully");
        }

        #endregion
    }

    #region Request/Response Models

    /// <summary>
    /// Login request data transfer object
    /// </summary>
    /// <param name="Email">Member's email address</param>
    /// <param name="Password">Member's password</param>
    public sealed record LoginRequestDto(string Email, string Password);

    #endregion
}
