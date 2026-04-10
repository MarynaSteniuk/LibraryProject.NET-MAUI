using AutoMapper;
using LibraryProject.BLL.DTOs;
using LibraryProject.DAL.Entities;
using LibraryProject.DAL.Repositories;

namespace LibraryProject.BLL.Services;

public class BookService : IBookService
{
    private readonly IGenericRepository<Book> _bookRepository;
    private readonly IGenericRepository<Author> _authorRepository;
    private readonly IMapper _mapper;

    public BookService(
        IGenericRepository<Book> bookRepository,
        IGenericRepository<Author> authorRepository,
        IMapper mapper)
    {
        _bookRepository = bookRepository;
        _authorRepository = authorRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BookDto>> GetAllAsync()
    {
        var entities = await _bookRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<BookDto>>(entities);
    }

    public async Task<BookDto?> GetByIdAsync(int id)
    {
        var entity = await _bookRepository.GetByIdAsync(id);
        return entity is null ? null : _mapper.Map<BookDto>(entity);
    }

    public async Task<BookDto> CreateAsync(CreateBookDto dto)
    {
        var author = await _authorRepository.GetByIdAsync(dto.AuthorId);
        if (author is null)
            throw new KeyNotFoundException($"Author with id {dto.AuthorId} not found");

        var entity = _mapper.Map<Book>(dto);
        await _bookRepository.AddAsync(entity);
        return _mapper.Map<BookDto>(entity);
    }

    public async Task UpdateAsync(int id, UpdateBookDto dto)
    {
        var existing = await _bookRepository.GetByIdAsync(id);
        if (existing is null)
            throw new KeyNotFoundException($"Book with id {id} not found");

        var author = await _authorRepository.GetByIdAsync(dto.AuthorId);
        if (author is null)
            throw new KeyNotFoundException($"Author with id {dto.AuthorId} not found");

        _mapper.Map(dto, existing);
        existing.Id = id;
        await _bookRepository.UpdateAsync(existing);
    }

    public async Task DeleteAsync(int id)
    {
        var existing = await _bookRepository.GetByIdAsync(id);
        if (existing is null)
            throw new KeyNotFoundException($"Book with id {id} not found");

        await _bookRepository.DeleteAsync(id);
    }
}