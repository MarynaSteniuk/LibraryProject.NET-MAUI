namespace LibraryProject.MAUI.Models;
public class BookModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Isbn { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int AuthorId { get; set; }
}