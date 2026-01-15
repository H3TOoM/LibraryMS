using LibraryMS.Domain.Repoistries;
using LibraryMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Infrastructure.Repoistries
{
    /// <summary>
    /// Generic repository implementation providing basic CRUD operations
    /// Implements the IMainRepoistery interface using Entity Framework Core
    /// </summary>
    /// <typeparam name="T">The entity type this repository handles</typeparam>
    public class MainRepoistery<T> : IMainRepoistery<T> where T : class
    {
        #region Fields

        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the MainRepoistery
        /// </summary>
        /// <param name="context">The database context</param>
        public MainRepoistery(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        #endregion

        #region Read Operations

        /// <summary>
        /// Retrieves all entities of type T from the database
        /// </summary>
        /// <returns>Collection of all entities</returns>
        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        /// <summary>
        /// Retrieves a specific entity by its ID
        /// </summary>
        /// <param name="id">Unique identifier of the entity</param>
        /// <returns>The entity if found, otherwise null</returns>
        public async Task<T> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        #endregion

        #region Write Operations

        /// <summary>
        /// Adds a new entity to the database
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <returns>The added entity</returns>
        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        /// <summary>
        /// Updates an existing entity in the database
        /// </summary>
        /// <param name="id">ID of the entity to update</param>
        /// <param name="entity">Updated entity data</param>
        /// <returns>True if update was successful, false otherwise</returns>
        public async Task<bool> UpdateAsync(int id, T entity)
        {
            var existingEntity = await _dbSet.FindAsync(id);
            if (existingEntity == null) return false;

            _context.Entry(existingEntity).Property("Id").IsModified = false;
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            return true;
        }

        /// <summary>
        /// Deletes an entity from the database
        /// </summary>
        /// <param name="id">ID of the entity to delete</param>
        /// <returns>True if deletion was successful, false otherwise</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) return false;

            _dbSet.Remove(entity);
            return true;
        }

        #endregion
    }
}
