using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LibraryProject.BLL.DTOs;
using LibraryProject.DAL.Entities;
using LibraryProject.DAL.Repositories;

namespace LibraryProject.BLL.Services;
public class BookService : IBookService
{
    private readonly IRepository<Book> _repository;
    private readonly IMapper _mapper;

    public BookService(IRepository<Book> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<BookDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<BookDto>>(entities);
    }
    public async Task<BookDto?> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return _mapper.Map<BookDto>(entity);
    }
    public async Task<BookDto> CreateAsync(CreateBookDto dto)
    {
        var entity = _mapper.Map<Book>(dto);
        await _repository.AddAsync(entity);
        return _mapper.Map<BookDto>(entity);
    }
    public async Task UpdateAsync(int id, UpdateBookDto dto)
    {
        var entity = _mapper.Map<Book>(dto);
        entity.Id = id; 
        await _repository.UpdateAsync(entity);
    }
    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}
