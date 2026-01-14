using LibraryMS.Domain.Entities;
using LibraryMS.Domain.Repoistries;
using LibraryMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Infrastructure.Repoistries
{
    public class BookRepoistory : IBookRepoistory
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Book> _dbSet;
        public BookRepoistory(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Book>();
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _context.Books
                .Include(b => b.Category)
                .FirstOrDefaultAsync(b => b.Id == id);
        }
        public async Task<IEnumerable<Book>> GetByCategoryIdAsync(int categoryId)
        {
            var books = await _dbSet.Where(b => b.CategoryId == categoryId).ToListAsync();
            return books;
        }

        public async Task<IEnumerable<Book>> SearchAsync(string query)
        {
            var books = await _dbSet.Where(b => b.Title.Contains(query) || b.Author.Contains(query)).ToListAsync();
            return books;
        }
    }
}
