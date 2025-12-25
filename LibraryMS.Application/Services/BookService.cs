using AutoMapper;
using LibraryMS.Application.DTOs.Books;
using LibraryMS.Application.Interfaces;
using LibraryMS.Domain.Entities;
using LibraryMS.Domain.Repoistries;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Application.Services
{
    /// <summary>
    /// Service responsible for book management operations
    /// Handles CRUD operations, search functionality, and category-based queries
    /// </summary>
    public class BookService : IBookService
    {
        #region Fields

        private readonly IMainRepoistery<Book> _mainRepoistory;
        private readonly IBookRepoistory _bookRepoistory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the BookService
        /// </summary>
        /// <param name="mainRepoistory">Main repository for book entities</param>
        /// <param name="bookRepoistory">Book-specific repository operations</param>
        /// <param name="unitOfWork">Unit of work for transaction management</param>
        /// <param name="mapper">AutoMapper instance for object mapping</param>
        public BookService(IMainRepoistery<Book> mainRepoistory, IBookRepoistory bookRepoistory, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mainRepoistory = mainRepoistory;
            _bookRepoistory = bookRepoistory;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #endregion


        #region CRUD Operations

        /// <summary>
        /// Creates a new book in the library
        /// </summary>
        /// <param name="dto">Book creation data</param>
        /// <returns>ID of the created book</returns>
        public async Task<int> CreateAsync(BookCreateDto dto)
        {
            var book = _mapper.Map<Book>(dto);
            await _mainRepoistory.AddAsync(book);
            await _unitOfWork.SaveChangesAsync();
            return book.Id;
        }

        /// <summary>
        /// Deletes a book from the library
        /// </summary>
        /// <param name="id">Book ID to delete</param>
        /// <returns>True if deletion was successful, false if book not found</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var book = await _mainRepoistory.GetByIdAsync(id);
            if (book == null) return false;

            await _mainRepoistory.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Retrieves all books in the library
        /// </summary>
        /// <returns>Collection of book read DTOs</returns>
        public async Task<IEnumerable<BookReadDto>> GetAllAsync()
        {
            var books = await _mainRepoistory.GetAllAsync();
            return _mapper.Map<IEnumerable<BookReadDto>>(books);
        }

        /// <summary>
        /// Retrieves a specific book by ID
        /// </summary>
        /// <param name="id">Book ID</param>
        /// <returns>Book read DTO or null if not found</returns>
        public async Task<BookReadDto?> GetByIdAsync(int id)
        {
            var book = await _mainRepoistory.GetByIdAsync(id);
            return _mapper.Map<BookReadDto?>(book);
        }

        /// <summary>
        /// Updates an existing book's information
        /// </summary>
        /// <param name="id">Book ID to update</param>
        /// <param name="dto">Updated book information</param>
        /// <returns>True if update was successful, false if book not found</returns>
        public async Task<bool> UpdateAsync(int id, BookUpdateDto dto)
        {
            var book = await _mainRepoistory.GetByIdAsync(id);
            if (book == null) return false;

            _mapper.Map(dto, book);
            await _mainRepoistory.UpdateAsync(id, book);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        #endregion

        #region Book-Specific Queries

        /// <summary>
        /// Retrieves all books belonging to a specific category
        /// </summary>
        /// <param name="categoryId">Category ID</param>
        /// <returns>Collection of books in the specified category</returns>
        public async Task<IEnumerable<BookReadDto>> GetByCategoryIdAsync(int categoryId)
        {
            var books = await _bookRepoistory.GetByCategoryIdAsync(categoryId);
            return _mapper.Map<IEnumerable<BookReadDto>>(books);
        }

        /// <summary>
        /// Searches for books by title, author, or ISBN
        /// </summary>
        /// <param name="query">Search query string</param>
        /// <returns>Collection of matching books</returns>
        public async Task<IEnumerable<BookReadDto>> SearchAsync(string query)
        {
            var books = await _bookRepoistory.SearchAsync(query);
            return _mapper.Map<IEnumerable<BookReadDto>>(books);
        }

        #endregion
    }
}
