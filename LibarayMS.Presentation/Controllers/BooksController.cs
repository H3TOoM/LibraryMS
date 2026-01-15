using LibraryMS.Application.DTOs.Books;
using LibraryMS.Application.Features.Book.Commands.AddBook;
using LibraryMS.Application.Features.Books.Commands.DeleteBook;
using LibraryMS.Application.Features.Books.Commands.UpdateBook;
using LibraryMS.Application.Features.Books.Queries.GetBookById;
using LibraryMS.Application.Features.Books.Queries.GetBooks;
using LibraryMS.Application.Features.Books.Queries.GetByCategoryId;
using LibraryMS.Application.Features.Books.Queries.SearchBook;
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

        private readonly IMediator _mediator;

        #endregion

        #region Constructor
        public BooksController(IBookService bookService, IMediator mediator)
        {
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

       
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _mediator.Send(new GetBookByIdQuery(id));
            if (book == null)
                return NotFound("Book not found");

            return Ok(book);
        }

        
        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetByCategory(int categoryId)
        {
            var books = await _mediator.Send(new GetByCategoryIdQuery(categoryId));
            return Ok(books);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest("Search query cannot be empty");

            var books = await _mediator.Send(new SearchByTitle(query)); 
            return Ok(books);
        }

        #endregion

        #region CRUD Operations

     
        [HttpPost]
        public async Task<IActionResult> Create(AddBookCommand command)
        {
            var bookId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = bookId }, new { Id = bookId });
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id , UpdateBookCommand command)
        {
           var result = await _mediator.Send(command);
              if (result == 0)
                return NotFound("Book not found");

              return Ok("Book updated successfully");
        }

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
