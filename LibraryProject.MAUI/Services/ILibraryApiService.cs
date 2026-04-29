using LibraryProject.MAUI.Models;

namespace LibraryProject.MAUI.Services;

public interface ILibraryApiService
{
    Task<IEnumerable<BookModel>> GetAllAsync();
    Task<BookModel?> GetByIdAsync(int id);
    Task<BookModel?> CreateAsync(BookModel item);
    Task UpdateAsync(BookModel item);
    Task DeleteAsync(int id);
  
}
