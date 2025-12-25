using AutoMapper;
using LibraryMS.Application.DTOs.Members;
using LibraryMS.Application.Interfaces;
using LibraryMS.Domain.Entities;
using LibraryMS.Domain.Repoistries;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Application.Services
{
    /// <summary>
    /// Service responsible for account management and authentication
    /// Handles user registration, login, email validation, and account updates
    /// </summary>
    public class AccountService : IAccountService
    {
        #region Fields

        private readonly IMainRepoistery<Member> _mainRepoistory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAccountRepoistory _accountRepoistory;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the AccountService
        /// </summary>
        /// <param name="mainRepoistory">Main repository for member entities</param>
        /// <param name="unitOfWork">Unit of work for transaction management</param>
        /// <param name="mapper">AutoMapper instance for object mapping</param>
        /// <param name="accountRepoistory">Account-specific repository operations</param>
        public AccountService(IMainRepoistery<Member> mainRepoistory, IUnitOfWork unitOfWork, IMapper mapper, IAccountRepoistory accountRepoistory)
        {
            _mainRepoistory = mainRepoistory;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _accountRepoistory = accountRepoistory;
        }

        #endregion

        #region Authentication Methods

        /// <summary>
        /// Checks if an email address is already taken by another member
        /// </summary>
        /// <param name="email">Email address to check</param>
        /// <returns>True if email is taken, false otherwise</returns>
        public async Task<bool> IsEmailTakenAsync(string email)
        {
            return await _accountRepoistory.IsEmailTakenAsync(email);
        }

        /// <summary>
        /// Creates a new member account with encrypted password
        /// </summary>
        /// <param name="dto">Member creation data</param>
        /// <returns>ID of the created member</returns>
        /// <exception cref="ArgumentException">Thrown when email is already taken</exception>
        public async Task<int> CreateAccount(MemberCreateDto dto)
        {
            // Check if email is already registered
            if (await IsEmailTakenAsync(dto.Email))
                throw new ArgumentException("Email is already taken");

            // Map DTO to entity and hash password
            var member = _mapper.Map<Member>(dto);
            member.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            // Save member to database
            await _mainRepoistory.AddAsync(member);
            await _unitOfWork.SaveChangesAsync();
            return member.Id;
        }

        /// <summary>
        /// Authenticates a member using email and password
        /// </summary>
        /// <param name="email">Member's email address</param>
        /// <param name="password">Member's password</param>
        /// <returns>ID of the authenticated member</returns>
        /// <exception cref="ArgumentException">Thrown when credentials are invalid</exception>
        public async Task<int> LoginAsync(string email, string password)
        {
            // Retrieve member by email
            var member = await _accountRepoistory.GetUserByEmail(email);

            // Verify credentials
            if (member == null || !BCrypt.Net.BCrypt.Verify(password, member.PasswordHash))
                throw new ArgumentException("Invalid email or password");

            return member.Id;
        }

        #endregion

        #region Account Management Methods

        /// <summary>
        /// Updates an existing member's account information
        /// </summary>
        /// <param name="id">Member ID to update</param>
        /// <param name="dto">Updated member information</param>
        /// <returns>True if update was successful, false if member not found</returns>
        public async Task<bool> UpdateAccount(int id, MemberUpdateDto dto)
        {
            // Retrieve existing member
            var member = await _mainRepoistory.GetByIdAsync(id);
            if (member == null) return false;

            // Update member with new data
            _mapper.Map(dto, member);
            await _mainRepoistory.UpdateAsync(id, member);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        #endregion
    }
}
