using LibraryProject.MAUI.Models;

namespace LibraryProject.MAUI.Services;

public interface ILibraryApiService
{
    Task<bool> LoginAsync(string email, string password);
    Task<bool> RegisterAsync(string email, string password);
    Task InitializeAuthAsync();
    Task<IEnumerable<BookModel>> GetAllAsync();
    Task<BookModel?> GetByIdAsync(int id);
    Task<BookModel?> CreateAsync(BookModel item);
    Task UpdateAsync(BookModel item);
    Task DeleteAsync(int id);
    Task<IEnumerable<AuthorModel>> GetAuthorsAsync();
    Task<AuthorModel?> GetAuthorByIdAsync(int id);
    Task<AuthorModel?> CreateAuthorAsync(AuthorModel item);
    Task UpdateAuthorAsync(AuthorModel item);
    Task DeleteAuthorAsync(int id);
}
