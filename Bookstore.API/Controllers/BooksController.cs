using Microsoft.AspNetCore.Mvc;
using Bookstore.Application;
using Bookstore.Domain;

namespace Bookstore.API.controllers;

[ApiController]
[Route("books")]
public class BooksController : ControllerBase
{
	private readonly BookService _service;
	
	public BooksController(BookService service)
	{
		_service = service;
	}
	
	[HttpGet]
	public async Task<ActionResult<List<Book>>> GetAll()
		=> Ok(await _service.GetAllAsync());
		
	[HttpGet("{id}")]
	public async Task<ActionResult<Book>> GetById(long id)
		=> Ok(await _service.GetByIdAsync(id));
		
	[HttpPost]
	public async Task<ActionResult<Book>> Create(CreateBookRequest request)
	{
		var book = await _service.CreateAsync(request.Title, request.Author);
		return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
	}
	
	[HttpPut("{id}/author")]
	public async Task<ActionResult<Book>> UpdateAuthor(long id, UpdateAuthorRequest request)
		=> Ok (await _service.UpdateAuthor(id, request.Author));
		
	[HttpPut("{id}/title")]
	public async Task<ActionResult<Book>> UpdateTitle(long id, UpdateTitleRequest request)
		=> Ok (await _service.UpdateTitle(id, request.Title));
		
	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(long id)
	{
		await _service.DeleteAsync(id);
		return NoContent();
	}
}