using LibraryMS.Domain.Entities;
using LibraryMS.Domain.Repoistries;
using LibraryMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryMS.Infrastructure.Repoistries
{
    /// <summary>
    /// Repository implementation for borrow record-specific data operations
    /// Provides methods for borrow record queries and return operations
    /// </summary>
    public class BorrowRepoistory : IBorrowRepoistory
    {
        #region Fields

        private readonly AppDbContext _context;
        private readonly DbSet<BorrowRecord> _dbSet;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the BorrowRepoistory
        /// </summary>
        /// <param name="context">The database context</param>
        public BorrowRepoistory(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<BorrowRecord>();
        }

        #endregion

        #region Query Methods

        /// <summary>
        /// Retrieves all borrow records for a specific book
        /// </summary>
        /// <param name="bookId">ID of the book</param>
        /// <returns>Collection of borrow records for the specified book</returns>
        public async Task<IEnumerable<BorrowRecord>> GetByBookIdAsync(int bookId)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(b => b.BookId == bookId)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves all borrow records for a specific member
        /// </summary>
        /// <param name="memberId">ID of the member</param>
        /// <returns>Collection of borrow records for the specified member</returns>
        public async Task<IEnumerable<BorrowRecord>> GetByMemberIdAsync(int memberId)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(b => b.MemberId == memberId)
                .ToListAsync();
        }

        #endregion

        #region Borrow Operations

        /// <summary>
        /// Marks a borrow record as returned and sets the return date
        /// </summary>
        /// <param name="borrowRecordId">ID of the borrow record to return</param>
        /// <param name="returnedAt">Date and time when the book was returned</param>
        /// <returns>True if return was successful, false otherwise</returns>
        public async Task<bool> ReturnAsync(int borrowRecordId, DateTime returnedAt)
        {
            var borrow = await _dbSet.FindAsync(borrowRecordId);
            if (borrow == null) return false;

            borrow.ReturnedAt = returnedAt;
            _dbSet.Update(borrow);
            return true;
        }

        #endregion
    }
}
