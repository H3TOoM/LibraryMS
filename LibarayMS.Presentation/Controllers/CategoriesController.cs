using LibraryMS.Application.DTOs.Categories;
using LibraryMS.Application.Features.Categories.Commands.Create;
using LibraryMS.Application.Features.Categories.Commands.Delete;
using LibraryMS.Application.Features.Categories.Commands.Update;
using LibraryMS.Application.Features.Categories.Queries.GetAll;
using LibraryMS.Application.Features.Categories.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibarayMS.Presentation.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Constructor

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region Read Operations

       
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _mediator.Send(new GetAllQuery());
            return Ok(categories);
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _mediator.Send(new GetByIdQuery(id));
            if (category == null)
                return NotFound("Category not found");

            return Ok(category);
        }

        #endregion

        #region CRUD Operations

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryCreateDto dto)
        {
            var categoryId = await _mediator.Send(new CreateCommand(dto));
            return CreatedAtAction(nameof(GetById), new { id = categoryId }, new { Id = categoryId });
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryUpdateDto dto)
        {
            var result = await _mediator.Send(new UpdateCommand(id, dto));
            if (!result)
                return NotFound("Category not found");

            return Ok("Category updated successfully");
        }




        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteCommand(id));
            if (!result)
                return NotFound("Category not found");

            return Ok("Category deleted successfully");
        }

        #endregion
    }
}
