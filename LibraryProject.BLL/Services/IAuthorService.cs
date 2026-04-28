using LibraryProject.BLL.DTOs;

namespace LibraryProject.BLL.Services;

public interface IAuthorService
{
    Task<IEnumerable<AuthorDto>> GetAllAsync();
    Task<AuthorDto?> GetByIdAsync(int id);
    Task<AuthorDto> CreateAsync(CreateAuthorDto dto);
    Task UpdateAsync(int id, UpdateAuthorDto dto);
    Task DeleteAsync(int id);
}
