using LibraryMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Domain.Repoistries
{
    /// <summary>
    /// Repository interface for book-specific data operations
    /// Provides methods for category-based queries and search functionality
    /// </summary>
    public interface IBookRepoistory
    {
        /// <summary>
        /// Retrieves all books belonging to a specific category
        /// </summary>
        /// <param name="categoryId">ID of the category</param>
        /// <returns>Collection of books in the specified category</returns>
        Task<IEnumerable<Book>> GetByCategoryIdAsync(int categoryId);

        /// <summary>
        /// Searches for books by title, author, or ISBN
        /// </summary>
        /// <param name="query">Search query string</param>
        /// <returns>Collection of matching books</returns>
        Task<IEnumerable<Book>> SearchAsync(string query);
    }
}
