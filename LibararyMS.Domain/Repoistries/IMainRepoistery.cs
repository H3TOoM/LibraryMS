using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Domain.Repoistries
{
    public interface IMainRepoistery<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);

        Task<T> AddAsync(T entity);

        Task<bool> UpdateAsync(int id,T entity);

        Task<bool> DeleteAsync(int id);
    }
}
