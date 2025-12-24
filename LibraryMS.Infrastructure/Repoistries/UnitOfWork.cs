using LibraryMS.Domain.Repoistries;
using LibraryMS.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Infrastructure.Repoistries
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly Dictionary<Type, object> _repositories = new();

        private readonly AppDbContext _context;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }



        // Commit changes to the database
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }


        // Repositories for different entities
        public IMainRepoistery<T> GetRepository<T>() where T : class
        {
            var type = typeof(T);

            // If repository for this entity doesn't exist, create and store it
            if (!_repositories.ContainsKey(type))
            {
                var repoInstance = new MainRepoistery<T>(_context);
                _repositories[type] = repoInstance;
            }

            return (IMainRepoistery<T>)_repositories[type];
        }
        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
