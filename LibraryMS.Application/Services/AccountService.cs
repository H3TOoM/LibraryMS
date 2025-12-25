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
    public class AccountService : IAccountService
    {
        private readonly IMainRepoistery<Member> _mainRepoistory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAccountRepoistory _accountRepoistory;

        public AccountService(IMainRepoistery<Member> mainRepoistory, IUnitOfWork unitOfWork, IMapper mapper, IAccountRepoistory accountRepoistory)
        {
            _mainRepoistory = mainRepoistory;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _accountRepoistory = accountRepoistory;
        }

        public async Task<bool> IsEmailTakenAsync(string email)
        {
            return await _accountRepoistory.IsEmailTakenAsync(email);
        }

        public async Task<int> CreateAccount(MemberCreateDto dto)
        {
            if (await IsEmailTakenAsync(dto.Email))
                throw new ArgumentException("Email is already taken");

            var member = _mapper.Map<Member>(dto);
            member.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            await _mainRepoistory.AddAsync(member);
            await _unitOfWork.SaveChangesAsync();
            return member.Id;
        }



        public async Task<int> LoginAsync(string email, string password)
        {
            var member = await _accountRepoistory.GetUserByEmail(email);
            if (member == null || !BCrypt.Net.BCrypt.Verify(password, member.PasswordHash))
                throw new ArgumentException("Invalid email or password");

            return member.Id;
        }

        public async Task<bool> UpdateAccount(int id, MemberUpdateDto dto)
        {
            var member = await _mainRepoistory.GetByIdAsync(id);
            if (member == null) return false;

            _mapper.Map(dto, member);
            await _mainRepoistory.UpdateAsync(id, member);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
