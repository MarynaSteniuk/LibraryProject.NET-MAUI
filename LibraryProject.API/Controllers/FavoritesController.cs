using LibraryProject.DAL;
using LibraryProject.DAL.Entities;
using LibraryProject.BLL.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FavoritesController : ControllerBase
{
    private readonly AppDbContext _context;

    public FavoritesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("toggle")]
    public async Task<IActionResult> ToggleFavorite([FromBody] FavoriteRequest request)
    {
        var existingFavorite = await _context.FavoriteBooks
            .FirstOrDefaultAsync(f => f.UserEmail == request.Email && f.BookId == request.BookId);

        if (existingFavorite != null)
        {
            _context.FavoriteBooks.Remove(existingFavorite);
            await _context.SaveChangesAsync();
            return Ok(new { isFavorite = false });
        }
        else
        {
            var newFavorite = new FavoriteBook
            {
                UserEmail = request.Email,
                BookId = request.BookId
            };
            _context.FavoriteBooks.Add(newFavorite);
            await _context.SaveChangesAsync();
            return Ok(new { isFavorite = true });
        }
    }

    [HttpGet("{email}")]
    public async Task<IActionResult> GetFavorites(string email)
    {
        var favoriteBooks = await _context.FavoriteBooks
            .Include(f => f.Book)
            .Where(f => f.UserEmail == email)
            .Select(f => new BookDto
            {
                Id = f.Book!.Id,
                Title = f.Book.Title,
                Isbn = f.Book.Isbn,
                Price = f.Book.Price,
                AuthorId = f.Book.AuthorId,
                ImageUrl = f.Book.ImageUrl
            })
            .ToListAsync();

        return Ok(favoriteBooks);
    }
}

public class FavoriteRequest
{
    public string Email { get; set; } = string.Empty;
    public int BookId { get; set; }
}