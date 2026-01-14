using LibraryMS.Application.DTOs.Books;
using LibraryMS.Application.Features.Book.Commands.AddBook;
using LibraryMS.Application.Features.Books.Commands.DeleteBook;
using LibraryMS.Application.Features.Books.Commands.UpdateBook;
using LibraryMS.Application.Features.Books.Queries.GetBookById;
using LibraryMS.Application.Features.Books.Queries.GetBooks;
using LibraryMS.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibarayMS.Presentation.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        #region Fields

        private readonly IBookService _bookService;
        private readonly IMediator _mediator;

        #endregion

        #region Constructor

        
        public BooksController(IBookService bookService, IMediator mediator)
        {
            _bookService = bookService;
            _mediator = mediator;
        }

        #endregion

        #region Read Operations

        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _mediator.Send(new GetAllBookQuery());
            return Ok(books);
        }

        /// <summary>
        /// Retrieves a specific book by ID
        /// </summary>
        /// <param name="id">Book ID</param>
        /// <returns>Book details or NotFound if book doesn't exist</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _mediator.Send(new GetBookByIdQuery(id));
            if (book == null)
                return NotFound("Book not found");

            return Ok(book);
        }

        /// <summary>
        /// Retrieves all books belonging to a specific category
        /// </summary>
        /// <param name="categoryId">Category ID</param>
        /// <returns>List of books in the specified category</returns>
        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetByCategory(int categoryId)
        {
            var books = await _bookService.GetByCategoryIdAsync(categoryId);
            return Ok(books);
        }

        /// <summary>
        /// Searches for books by title, author, or ISBN
        /// </summary>
        /// <param name="query">Search query string</param>
        /// <returns>List of matching books</returns>
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest("Search query cannot be empty");

            var books = await _bookService.SearchAsync(query);
            return Ok(books);
        }

        #endregion

        #region CRUD Operations

        /// <summary>
        /// Adds a new book to the library
        /// </summary>
        /// <param name="dto">Book creation data</param>
        /// <returns>Created book ID</returns>
        [HttpPost]
        public async Task<IActionResult> Create(AddBookCommand command)
        {
            var bookId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = bookId }, new { Id = bookId });
        }

        /// <summary>
        /// Updates an existing book's information
        /// </summary>
        /// <param name="id">Book ID to update</param>
        /// <param name="dto">Updated book information</param>
        /// <returns>Success or NotFound response</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id , UpdateBookCommand command)
        {
           var result = await _mediator.Send(command);
              if (result == 0)
                return NotFound("Book not found");

              return Ok("Book updated successfully");
        }

        /// <summary>
        /// Removes a book from the library
        /// </summary>
        /// <param name="id">Book ID to delete</param>
        /// <returns>Success or NotFound response</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteBookCommand(id));
            if (!result)
                return NotFound("Book not found");

            return Ok("Book deleted successfully");
        }

        #endregion
    }
}
