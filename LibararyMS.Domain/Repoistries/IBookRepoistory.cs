using LibraryMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Domain.Repoistries
{
    public interface IBookRepoistory
    {
        Task<IEnumerable<Book>> GetByCategoryIdAsync(int categoryId);
        Task<IEnumerable<Book>> SearchAsync(string query);
    }
}
