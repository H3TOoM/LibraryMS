using AutoMapper;
using LibraryMS.Application.DTOs.Fines;
using LibraryMS.Application.Interfaces;
using LibraryMS.Domain.Entities;
using LibraryMS.Domain.Repoistries;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Application.Services
{
    /// <summary>
    /// Service responsible for fine management operations
    /// Handles fine creation, payment, and fine-related queries
    /// </summary>
    public class FineService : IFineService
    {
        #region Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMainRepoistery<Fine> _mainRepoistory;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the FineService
        /// </summary>
        /// <param name="unitOfWork">Unit of work for transaction management</param>
        /// <param name="mapper">AutoMapper instance for object mapping</param>
        /// <param name="mainRepoistory">Main repository for fine entities</param>
        public FineService(IUnitOfWork unitOfWork, IMapper mapper, IMainRepoistery<Fine> mainRepoistory)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mainRepoistory = mainRepoistory;
        }

        #endregion

        #region Read Operations

        /// <summary>
        /// Retrieves all fines in the system
        /// </summary>
        /// <returns>Collection of fine read DTOs</returns>
        public async Task<IEnumerable<FineReadDto>> GetAllAsync()
        {
            var fines = await _mainRepoistory.GetAllAsync();
            return _mapper.Map<IEnumerable<FineReadDto>>(fines);
        }

        /// <summary>
        /// Retrieves a specific fine by ID
        /// </summary>
        /// <param name="id">Fine ID</param>
        /// <returns>Fine read DTO or null if not found</returns>
        public async Task<FineReadDto?> GetByIdAsync(int id)
        {
            var fine = await _mainRepoistory.GetByIdAsync(id);
            return _mapper.Map<FineReadDto?>(fine);
        }

        /// <summary>
        /// Retrieves fines associated with a specific borrow record
        /// </summary>
        /// <param name="borrowRecordId">Borrow record ID</param>
        /// <returns>Collection of fines for the specified borrow record</returns>
        public async Task<IEnumerable<FineReadDto>> GetByBorrowRecordIdAsync(int borrowRecordId)
        {
            var fines = await _mainRepoistory.GetByIdAsync(borrowRecordId);
            return _mapper.Map<IEnumerable<FineReadDto>>(fines);
        }

        #endregion

        #region CRUD Operations

        /// <summary>
        /// Creates a new fine record
        /// </summary>
        /// <param name="dto">Fine creation data</param>
        /// <returns>ID of the created fine</returns>
        public async Task<int> CreateAsync(FineCreateDto dto)
        {
            var fine = _mapper.Map<Fine>(dto);
            await _mainRepoistory.AddAsync(fine);
            await _unitOfWork.SaveChangesAsync();
            return fine.Id;
        }

        /// <summary>
        /// Updates an existing fine's information
        /// </summary>
        /// <param name="id">Fine ID to update</param>
        /// <param name="dto">Updated fine information</param>
        /// <returns>True if update was successful, false if fine not found</returns>
        public async Task<bool> UpdateAsync(int id, FineUpdateDto dto)
        {
            var fine = await _mainRepoistory.GetByIdAsync(id);
            if (fine == null)
                return false;

            _mapper.Map(dto, fine);
            await _mainRepoistory.UpdateAsync(id, fine);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Deletes a fine from the system
        /// </summary>
        /// <param name="id">Fine ID to delete</param>
        /// <returns>True if deletion was successful, false if fine not found</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var fine = await _mainRepoistory.GetByIdAsync(id);
            if (fine == null)
                return false;

            await _mainRepoistory.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        #endregion

        #region Fine-Specific Operations

        /// <summary>
        /// Marks a fine as paid and sets the payment date
        /// </summary>
        /// <param name="fineId">Fine ID to pay</param>
        /// <param name="paidAt">Date and time when the fine was paid</param>
        /// <returns>True if payment was successful, false if fine not found</returns>
        public async Task<bool> PayAsync(int fineId, DateTime paidAt)
        {
            var fine = await _mainRepoistory.GetByIdAsync(fineId);
            if (fine == null)
                return false;

            fine.IsPaid = true;
            fine.PaidAt = paidAt;

            await _mainRepoistory.UpdateAsync(fineId, fine);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        #endregion
    }
}
