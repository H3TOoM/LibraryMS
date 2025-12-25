using AutoMapper;
using LibraryMS.Application.DTOs.BorrowRecords;
using LibraryMS.Application.Interfaces;
using LibraryMS.Domain.Entities;
using LibraryMS.Domain.Repoistries;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Application.Services
{
    /// <summary>
    /// Service responsible for borrow record management operations
    /// Handles book borrowing, returning, and borrow record queries
    /// </summary>
    public class BorrowService : IBorrowService
    {
        #region Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMainRepoistery<BorrowRecord> _mainRepoistory;
        private readonly IBorrowRepoistory _borrowRepoistory;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the BorrowService
        /// </summary>
        /// <param name="unitOfWork">Unit of work for transaction management</param>
        /// <param name="mapper">AutoMapper instance for object mapping</param>
        /// <param name="mainRepoistory">Main repository for borrow record entities</param>
        /// <param name="borrowRepoistory">Borrow record-specific repository operations</param>
        public BorrowService(IUnitOfWork unitOfWork, IMapper mapper, IMainRepoistery<BorrowRecord> mainRepoistory, IBorrowRepoistory borrowRepoistory)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mainRepoistory = mainRepoistory;
            _borrowRepoistory = borrowRepoistory;
        }

        #endregion
        #region Read Operations

        /// <summary>
        /// Retrieves all borrow records in the system
        /// </summary>
        /// <returns>Collection of borrow record read DTOs</returns>
        public async Task<IEnumerable<BorrowRecordReadDto>> GetAllAsync()
        {
            var borrowRecords = await _mainRepoistory.GetAllAsync();
            return _mapper.Map<IEnumerable<BorrowRecordReadDto>>(borrowRecords);
        }

        /// <summary>
        /// Retrieves a specific borrow record by ID
        /// </summary>
        /// <param name="id">Borrow record ID</param>
        /// <returns>Borrow record read DTO or null if not found</returns>
        public async Task<BorrowRecordReadDto?> GetByIdAsync(int id)
        {
            var borrowRecord = await _mainRepoistory.GetByIdAsync(id);
            return _mapper.Map<BorrowRecordReadDto?>(borrowRecord);
        }

        /// <summary>
        /// Retrieves all borrow records for a specific book
        /// </summary>
        /// <param name="bookId">Book ID</param>
        /// <returns>Collection of borrow records for the specified book</returns>
        public async Task<IEnumerable<BorrowRecordReadDto>> GetByBookIdAsync(int bookId)
        {
            var borrowRecords = await _borrowRepoistory.GetByBookIdAsync(bookId);
            return _mapper.Map<IEnumerable<BorrowRecordReadDto>>(borrowRecords);
        }

        /// <summary>
        /// Retrieves all borrow records for a specific member
        /// </summary>
        /// <param name="memberId">Member ID</param>
        /// <returns>Collection of borrow records for the specified member</returns>
        public async Task<IEnumerable<BorrowRecordReadDto>> GetByMemberIdAsync(int memberId)
        {
            var borrowRecords = await _borrowRepoistory.GetByMemberIdAsync(memberId);
            return _mapper.Map<IEnumerable<BorrowRecordReadDto>>(borrowRecords);
        }

        #endregion

        #region Borrow Operations

        /// <summary>
        /// Creates a new borrow record (borrows a book)
        /// </summary>
        /// <param name="dto">Borrow record creation data</param>
        /// <returns>ID of the created borrow record</returns>
        public async Task<int> BorrowAsync(BorrowRecordCreateDto dto)
        {
            var borrowRecord = _mapper.Map<BorrowRecord>(dto);
            await _mainRepoistory.AddAsync(borrowRecord);
            await _unitOfWork.SaveChangesAsync();
            return borrowRecord.Id;
        }

        /// <summary>
        /// Marks a borrow record as returned and sets the return date
        /// </summary>
        /// <param name="borrowRecordId">Borrow record ID to return</param>
        /// <param name="returnedAt">Date and time when the book was returned</param>
        /// <returns>True if return was successful, false if borrow record not found</returns>
        public async Task<bool> ReturnAsync(int borrowRecordId, DateTime returnedAt)
        {
            var result = await _borrowRepoistory.ReturnAsync(borrowRecordId, returnedAt);
            if (!result) return false;

            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        #endregion
    }
}
