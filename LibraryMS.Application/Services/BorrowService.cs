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
    public class BorrowService : IBorrowService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMainRepoistery<BorrowRecord> _mainRepoistory;
        private readonly IBorrowRepoistory _borrowRepoistory;
        public BorrowService(IUnitOfWork unitOfWork, IMapper mapper, IMainRepoistery<BorrowRecord> mainRepoistory, IBorrowRepoistory borrowRepoistory)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mainRepoistory = mainRepoistory;
            _borrowRepoistory = borrowRepoistory;
        }
        public async Task<int> BorrowAsync(BorrowRecordCreateDto dto)
        {
            var borrowRecord = _mapper.Map<BorrowRecord>(dto);
            await _mainRepoistory.AddAsync(borrowRecord);
            await _unitOfWork.SaveChangesAsync();
            return borrowRecord.Id;
        }

        public async Task<IEnumerable<BorrowRecordReadDto>> GetAllAsync()
        {
            var borrowRecords = await _mainRepoistory.GetAllAsync();
            return _mapper.Map<IEnumerable<BorrowRecordReadDto>>(borrowRecords);
        }

        public async Task<IEnumerable<BorrowRecordReadDto>> GetByBookIdAsync(int bookId)
        {
            var borrowRecords = await _borrowRepoistory.GetByBookIdAsync(bookId);
            return _mapper.Map<IEnumerable<BorrowRecordReadDto>>(borrowRecords);

        }

        public async Task<BorrowRecordReadDto?> GetByIdAsync(int id)
        {
            var borrowRecord = await _mainRepoistory.GetByIdAsync(id);
            return _mapper.Map<BorrowRecordReadDto?>(borrowRecord);
        }

        public async Task<IEnumerable<BorrowRecordReadDto>> GetByMemberIdAsync(int memberId)
        {
            var borrowRecords = await _borrowRepoistory.GetByMemberIdAsync(memberId);
            return _mapper.Map<IEnumerable<BorrowRecordReadDto>>(borrowRecords);
        }

        public async Task<bool> ReturnAsync(int borrowRecordId, DateTime returnedAt)
        {
            var result =await _borrowRepoistory.ReturnAsync(borrowRecordId, returnedAt);
            if (!result) return false;
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
