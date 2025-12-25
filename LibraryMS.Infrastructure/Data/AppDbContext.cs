using LibraryMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Infrastructure.Data
{
    /// <summary>
    /// Entity Framework Core database context for the Library Management System
    /// Defines DbSets for all domain entities and handles database operations
    /// </summary>
    public class AppDbContext : DbContext
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the AppDbContext
        /// </summary>
        /// <param name="options">The options to be used by the DbContext</param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        #endregion

        #region DbSets

        /// <summary>
        /// DbSet for Book entities
        /// </summary>
        public DbSet<Book> Books { get; set; }

        /// <summary>
        /// DbSet for Member entities
        /// </summary>
        public DbSet<Member> Members { get; set; }

        /// <summary>
        /// DbSet for BorrowRecord entities
        /// </summary>
        public DbSet<BorrowRecord> BorrowRecords { get; set; }

        /// <summary>
        /// DbSet for Fine entities
        /// </summary>
        public DbSet<Fine> Fines { get; set; }

        /// <summary>
        /// DbSet for Category entities
        /// </summary>
        public DbSet<Category> Categories { get; set; }

        #endregion
    }
}
