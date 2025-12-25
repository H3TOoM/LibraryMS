using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Domain.Repoistries
{
    /// <summary>
    /// Unit of Work pattern interface for managing transactions and repositories
    /// Provides centralized access to repositories and transaction management
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Saves all changes made in the current transaction to the database
        /// </summary>
        /// <returns>Number of affected records</returns>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// Gets a repository instance for the specified entity type
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <returns>Repository instance for the specified type</returns>
        IMainRepoistery<T> GetRepository<T>() where T : class;
    }
}
