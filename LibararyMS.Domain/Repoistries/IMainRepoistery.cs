using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Domain.Repoistries
{
    /// <summary>
    /// Generic repository interface providing basic CRUD operations
    /// Defines standard data access methods for entities
    /// </summary>
    /// <typeparam name="T">The entity type this repository handles</typeparam>
    public interface IMainRepoistery<T> where T : class
    {
        #region Read Operations

        /// <summary>
        /// Retrieves all entities of type T
        /// </summary>
        /// <returns>Collection of all entities</returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Retrieves a specific entity by its ID
        /// </summary>
        /// <param name="id">Unique identifier of the entity</param>
        /// <returns>The entity if found, otherwise null</returns>
        Task<T> GetByIdAsync(int id);

        #endregion

        #region Write Operations

        /// <summary>
        /// Adds a new entity to the data store
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <returns>The added entity</returns>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// Updates an existing entity in the data store
        /// </summary>
        /// <param name="id">ID of the entity to update</param>
        /// <param name="entity">Updated entity data</param>
        /// <returns>True if update was successful, false otherwise</returns>
        Task<bool> UpdateAsync(int id, T entity);

        /// <summary>
        /// Deletes an entity from the data store
        /// </summary>
        /// <param name="id">ID of the entity to delete</param>
        /// <returns>True if deletion was successful, false otherwise</returns>
        Task<bool> DeleteAsync(int id);

        #endregion
    }
}
