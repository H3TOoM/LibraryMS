using LibraryMS.Domain.Entities;
using LibraryMS.Domain.Repoistries;
using LibraryMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Infrastructure.Repoistries
{
    public class BorrowRepoistory : IBorrowRepoistory
    {
        private readonly AppDbContext _context;
        private readonly DbSet<BorrowRecord> _dbSet;
        public BorrowRepoistory(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<BorrowRecord>();
        }
        public async Task<BorrowRecord> GetByBookIdAsync(int bookId)
        {
            var borrow = await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.BookId == bookId && b.ReturnedAt == null);
            return borrow;
        }

        public async Task<BorrowRecord> GetByMemberIdAsync(int memberId)
        {
            var borrow = await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.MemberId == memberId && b.ReturnedAt == null);
            return borrow;
        }

        public async Task<bool> ReturnAsync(int borrowRecordId, DateTime returnedAt)
        {
            var borrow = await _dbSet.FindAsync(borrowRecordId);
            if (borrow == null) return false;

            borrow.ReturnedAt = returnedAt;
            _dbSet.Update(borrow);
            return true;
        }
    }
}
