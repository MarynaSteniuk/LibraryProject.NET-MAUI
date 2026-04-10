using Microsoft.AspNetCore.Mvc;
using LibraryProject.BLL.Services;
using LibraryProject.BLL.DTOs;

namespace LibraryProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookDto>>> GetAll()
    {
        var result = await _bookService.GetAllAsync();
        return Ok(result);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<BookDto>> GetById(int id)
    {
        var result = await _bookService.GetByIdAsync(id);
        if (result == null) return NotFound(); 
        return Ok(result);
    }
    [HttpPost]
    public async Task<ActionResult<BookDto>> Create(CreateBookDto dto)
    {
        var result = await _bookService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateBookDto dto)
    {
        await _bookService.UpdateAsync(id, dto);
        return NoContent(); 
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _bookService.DeleteAsync(id);
        return NoContent(); 
    }
}
