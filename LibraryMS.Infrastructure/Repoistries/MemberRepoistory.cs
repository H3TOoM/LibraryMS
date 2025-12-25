using LibraryMS.Domain.Entities;
using LibraryMS.Domain.Repoistries;
using LibraryMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Infrastructure.Repoistries
{
    public class MemberRepoistory : IMemberRepoistory
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Member> _dbSet;
        public MemberRepoistory(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Member>();
        }
        public async Task<int> GetActiveBorrowCountAsync(int memberId)
        {
            var borrowCount = await _context.BorrowRecords
                .CountAsync(b => b.MemberId == memberId && b.ReturnedAt == null);
            return borrowCount;
        }
    }
}
