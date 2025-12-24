using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Domain.Repoistries
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync();
        IMainRepoistery<T> GetRepository<T>() where T : class;

    }
}
