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
    /// Service responsible for member management operations
    /// Handles CRUD operations and member-specific queries
    /// </summary>
    public class MemberService : IMemberService
    {
        #region Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMainRepoistery<Member> _mainRepoistory;
        private readonly IMemberRepoistory _memberRepoistory;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the MemberService
        /// </summary>
        /// <param name="unitOfWork">Unit of work for transaction management</param>
        /// <param name="mapper">AutoMapper instance for object mapping</param>
        /// <param name="mainRepoistory">Main repository for member entities</param>
        /// <param name="memberRepoistory">Member-specific repository operations</param>
        public MemberService(IUnitOfWork unitOfWork, IMapper mapper, IMainRepoistery<Member> mainRepoistory, IMemberRepoistory memberRepoistory)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mainRepoistory = mainRepoistory;
            _memberRepoistory = memberRepoistory;
        }

        #endregion


        #region CRUD Operations

        /// <summary>
        /// Creates a new member in the system
        /// </summary>
        /// <param name="dto">Member creation data</param>
        /// <returns>ID of the created member</returns>
        public async Task<int> CreateAsync(MemberCreateDto dto)
        {
            var member = _mapper.Map<Member>(dto);
            await _mainRepoistory.AddAsync(member);
            await _unitOfWork.SaveChangesAsync();
            return member.Id;
        }

        /// <summary>
        /// Deletes a member from the system
        /// </summary>
        /// <param name="id">Member ID to delete</param>
        /// <returns>True if deletion was successful, false if member not found</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var member = await _mainRepoistory.GetByIdAsync(id);
            if (member == null)
                return false;

            await _mainRepoistory.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Retrieves all members in the system
        /// </summary>
        /// <returns>Collection of member read DTOs</returns>
        public async Task<IEnumerable<MemberReadDto>> GetAllAsync()
        {
            var members = await _mainRepoistory.GetAllAsync();
            return _mapper.Map<IEnumerable<MemberReadDto>>(members);
        }

        /// <summary>
        /// Retrieves a specific member by ID
        /// </summary>
        /// <param name="id">Member ID</param>
        /// <returns>Member read DTO or null if not found</returns>
        public async Task<MemberReadDto?> GetByIdAsync(int id)
        {
            var member = await _mainRepoistory.GetByIdAsync(id);
            return _mapper.Map<MemberReadDto?>(member);
        }

        /// <summary>
        /// Updates an existing member's information
        /// </summary>
        /// <param name="id">Member ID to update</param>
        /// <param name="dto">Updated member information</param>
        /// <returns>True if update was successful, false if member not found</returns>
        public async Task<bool> UpdateAsync(int id, MemberUpdateDto dto)
        {
            var member = await _mainRepoistory.GetByIdAsync(id);
            if (member == null)
                return false;

            _mapper.Map(dto, member);
            await _mainRepoistory.UpdateAsync(id, member);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        #endregion

        #region Member-Specific Queries

        /// <summary>
        /// Gets the count of active (unreturned) borrow records for a member
        /// </summary>
        /// <param name="memberId">Member ID</param>
        /// <returns>Count of active borrows</returns>
        public async Task<int> GetActiveBorrowCountAsync(int memberId)
        {
            var borrowCount = await _memberRepoistory.GetActiveBorrowCountAsync(memberId);
            return borrowCount;
        }

        /// <summary>
        /// Retrieves the member entity by ID (for internal use by other services)
        /// </summary>
        /// <param name="id">Member ID</param>
        /// <returns>Member entity or null if not found</returns>
        public async Task<Member?> GetMemberEntityByIdAsync(int id)
        {
            return await _mainRepoistory.GetByIdAsync(id);
        }

        #endregion
    }
}
