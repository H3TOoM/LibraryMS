using LibraryMS.Application.DTOs.Auth;
using LibraryMS.Application.DTOs.Members;
using LibraryMS.Application.Interfaces;
using LibraryMS.Domain.Entities;
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

        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;
        private readonly IMemberService _memberService;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the AccountController
        /// </summary>
        /// <param name="accountService">Service for account-related operations</param>
        /// <param name="tokenService">Service for JWT token generation</param>
        /// <param name="memberService">Service for member operations</param>
        public AccountController(
            IAccountService accountService,
            ITokenService tokenService,
            IMemberService memberService)
        {
            _accountService = accountService;
            _tokenService = tokenService;
            _memberService = memberService;
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
                var memberId = await _accountService.CreateAccount(dto);

                // Retrieve the created member entity
                var member = await _memberService.GetMemberEntityByIdAsync(memberId);

                if (member == null)
                    return BadRequest("Failed to create account");

                // Generate JWT token for the new member
                var tokenResult = _tokenService.GenerateToken(member);

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
                var memberId = await _accountService.LoginAsync(dto.Email, dto.Password);

                // Retrieve the authenticated member
                var member = await _memberService.GetMemberEntityByIdAsync(memberId);

                if (member == null)
                    return Unauthorized("Invalid credentials");

                // Generate JWT token for authentication
                var tokenResult = _tokenService.GenerateToken(member);

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
            var result = await _accountService.UpdateAccount(id, dto);
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
