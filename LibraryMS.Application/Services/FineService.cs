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
    public class FineService : IFineService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMainRepoistery<Fine> _mainRepoistory;
        public FineService(IUnitOfWork unitOfWork, IMapper mapper , IMainRepoistery<Fine> mainRepoistory)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mainRepoistory = mainRepoistory;
        }

        public async Task<int> CreateAsync(FineCreateDto dto)
        {
            var fine = _mapper.Map<Fine>(dto);
            await _mainRepoistory.AddAsync(fine);
            await _unitOfWork.SaveChangesAsync();
            return fine.Id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var fine = await _mainRepoistory.GetByIdAsync(id);
            if (fine == null)
                return false;
            await _mainRepoistory.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<FineReadDto>> GetAllAsync()
        {
            var fines = await _mainRepoistory.GetAllAsync();
            return _mapper.Map<IEnumerable<FineReadDto>>(fines);
        }

        public async Task<IEnumerable<FineReadDto>> GetByBorrowRecordIdAsync(int borrowRecordId)
        {
            var fines = await _mainRepoistory.GetByIdAsync(borrowRecordId);
            return _mapper.Map<IEnumerable<FineReadDto>>(fines);
        }


        public async Task<FineReadDto?> GetByIdAsync(int id)
        {
            var fine = await _mainRepoistory.GetByIdAsync(id);
            return _mapper.Map<FineReadDto?>(fine);
        }

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

        public async Task<bool> UpdateAsync(int id, FineUpdateDto dto)
        {
            var fine = await _mainRepoistory.GetByIdAsync(id);
            if (fine == null)
                return false;
            _mapper.Map(dto, fine);
            await _mainRepoistory.UpdateAsync(id,fine);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
