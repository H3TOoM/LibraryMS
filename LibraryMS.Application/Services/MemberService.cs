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
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMainRepoistery<Member> _mainRepoistory;
        private readonly IMemberRepoistory _memberRepoistory;

        public MemberService(IUnitOfWork unitOfWork, IMapper mapper, IMainRepoistery<Member> mainRepoistory, IMemberRepoistory memberRepoistory)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mainRepoistory = mainRepoistory;
            _memberRepoistory = memberRepoistory;
        }



        public async Task<int> CreateAsync(MemberCreateDto dto)
        {
            var member = _mapper.Map<Member>(dto);
            await _mainRepoistory.AddAsync(member);
            await _unitOfWork.SaveChangesAsync();
            return member.Id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var member = await _mainRepoistory.GetByIdAsync(id);    
            if (member == null)
                return false;
            await _mainRepoistory.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetActiveBorrowCountAsync(int memberId)
        {
            var borrowCount = await _memberRepoistory.GetActiveBorrowCountAsync(memberId);
            return borrowCount;
        }

        public async Task<IEnumerable<MemberReadDto>> GetAllAsync()
        {
            var members = await _mainRepoistory.GetAllAsync();
            return _mapper.Map<IEnumerable<MemberReadDto>>(members);
        }

        public async Task<MemberReadDto?> GetByIdAsync(int id)
        {
            var member = await _mainRepoistory.GetByIdAsync(id);
            return _mapper.Map<MemberReadDto?>(member);

        }

        public async Task<bool> UpdateAsync(int id, MemberUpdateDto dto)
        {
            var member = await _mainRepoistory.GetByIdAsync(id);
            if (member == null)
                return false;

            _mapper.Map(dto, member);
            await _mainRepoistory.UpdateAsync(id,member);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
