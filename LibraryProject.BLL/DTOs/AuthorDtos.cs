using System.ComponentModel.DataAnnotations;

namespace LibraryProject.BLL.DTOs;

public class AuthorDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Nationality { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
}

public class CreateAuthorDto
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Nationality { get; set; } = string.Empty;

    [Required]
    public DateTime BirthDate { get; set; }
}

public class UpdateAuthorDto
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Nationality { get; set; } = string.Empty;

    [Required]
    public DateTime BirthDate { get; set; }
}