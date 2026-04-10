using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryProject.BLL.DTOs;

namespace LibraryProject.BLL.Services;

public interface IBookService
{
    Task<IEnumerable<BookDto>> GetAllAsync();
    Task<BookDto?> GetByIdAsync(int id);
    Task<BookDto> CreateAsync(CreateBookDto dto);
    Task UpdateAsync(int id, UpdateBookDto dto);
    Task DeleteAsync(int id);
}
