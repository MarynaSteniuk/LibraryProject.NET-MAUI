namespace LibraryProject.DAL.Entities; 
public class FavoriteBook
{
    public int Id { get; set; }
    public string UserEmail { get; set; } = string.Empty;
    public int BookId { get; set; }
    public Book? Book { get; set; }
}
