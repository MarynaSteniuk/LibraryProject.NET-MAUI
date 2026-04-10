using System.ComponentModel.DataAnnotations;

namespace LibraryProject.BLL.DTOs;

public class BookDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Isbn { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int AuthorId { get; set; }
    public string AuthorName { get; set; } = string.Empty;
}

public class CreateBookDto
{
    [Required]
    [MaxLength(300)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string Isbn { get; set; } = string.Empty;

    [Required]
    [Range(0.01, 100000)]
    public decimal Price { get; set; }

    [Required]
    public int AuthorId { get; set; }
}

public class UpdateBookDto
{
    [Required]
    [MaxLength(300)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string Isbn { get; set; } = string.Empty;

    [Required]
    [Range(0.01, 100000)]
    public decimal Price { get; set; }

    [Required]
    public int AuthorId { get; set; }
}