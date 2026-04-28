using AutoMapper;
using LibraryProject.BLL.DTOs;
using LibraryProject.DAL.Entities;
using LibraryProject.DAL.Repositories;

namespace LibraryProject.BLL.Services;

public class AuthorService : IAuthorService
{
    private readonly IGenericRepository<Author> _repository;
    private readonly IMapper _mapper;

    public AuthorService(IGenericRepository<Author> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AuthorDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<AuthorDto>>(entities);
    }

    public async Task<AuthorDto?> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity is null ? null : _mapper.Map<AuthorDto>(entity);
    }

    public async Task<AuthorDto> CreateAsync(CreateAuthorDto dto)
    {
        var entity = _mapper.Map<Author>(dto);
        await _repository.AddAsync(entity);
        return _mapper.Map<AuthorDto>(entity);
    }

    public async Task UpdateAsync(int id, UpdateAuthorDto dto)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing is null)
            throw new KeyNotFoundException($"Author with id {id} not found");

        _mapper.Map(dto, existing);
        existing.Id = id; // Зберігаємо оригінальний ID
        await _repository.UpdateAsync(existing);
    }

    public async Task DeleteAsync(int id)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing is null)
            throw new KeyNotFoundException($"Author with id {id} not found");

        await _repository.DeleteAsync(id);
    }
}
