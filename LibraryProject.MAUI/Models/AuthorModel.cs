namespace LibraryProject.MAUI.Models;
public class AuthorModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Nationality { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
}