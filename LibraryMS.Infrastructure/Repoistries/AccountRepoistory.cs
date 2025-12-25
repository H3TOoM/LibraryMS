using LibraryMS.Domain.Entities;
using LibraryMS.Domain.Repoistries;
using LibraryMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Infrastructure.Repoistries
{
    public class AccountRepoistory : IAccountRepoistory
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Member> _dbSet;

        public AccountRepoistory(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Member>();
        }

        public async Task<Member> GetUserByEmail(string email)
        {
            var member = await _dbSet.FirstOrDefaultAsync(m => m.Email == email);
            return member;
        }

        public async Task<bool> IsEmailTakenAsync(string email)
        {
            var result = await _dbSet.AnyAsync(m => m.Email == email);
            return result;
        }
    }
}
