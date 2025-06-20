// MangaMechiApi.Application.Services/MangaService.cs
using AutoMapper;
using MangaMechiApi.Core.Entities;
using MangaMechiApi.Core.Interfaces;
using MangaMechiApi.Application.DTOs;

namespace MangaMechiApi.Application.Services;

public class MangaService : IMangaService
{
    private readonly IMangaRepository _mangaRepository;
    private readonly IMapper _mapper;

    public MangaService(IMangaRepository mangaRepository, IMapper mapper)
    {
        _mangaRepository = mangaRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MangaDto>> GetAllAsync()
    {
        var mangas = await _mangaRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<MangaDto>>(mangas);
    }

    // Implementación del nuevo método paginado
    public async Task<PagedResultDto<MangaDto>> GetAllPagedAsync(PaginationRequestDto pagination)
    {
        var (items, totalCount) = await _mangaRepository.GetAllPagedAsync(pagination);
        var mangaDtos = _mapper.Map<IEnumerable<MangaDto>>(items);
        return new PagedResultDto<MangaDto>(mangaDtos, totalCount, pagination.PageNumber, pagination.PageSize);
    }

    public async Task<MangaDto?> GetByIdAsync(int id)
    {
        var manga = await _mangaRepository.GetByIdAsync(id);
        return manga != null ? _mapper.Map<MangaDto>(manga) : null;
    }

    // Modificación de GetByGenreAsync para paginación
    public async Task<PagedResultDto<MangaDto>> GetByGenreAsync(string genre, PaginationRequestDto pagination)
    {
        var (items, totalCount) = await _mangaRepository.GetByGenreAsync(genre, pagination);
        var mangaDtos = _mapper.Map<IEnumerable<MangaDto>>(items);
        return new PagedResultDto<MangaDto>(mangaDtos, totalCount, pagination.PageNumber, pagination.PageSize);
    }

    public async Task<MangaDto> CreateAsync(MangaCreateDto mangaDto)
    {
        var manga = _mapper.Map<Manga>(mangaDto);
        var createdManga = await _mangaRepository.CreateAsync(manga);
        return _mapper.Map<MangaDto>(createdManga);
    }

    public async Task UpdateAsync(int id, MangaUpdateDto mangaDto)
    {
        var existingManga = await _mangaRepository.GetByIdAsync(id);
        if (existingManga == null)
        {
            throw new KeyNotFoundException($"Manga with ID {id} not found");
        }
        _mapper.Map(mangaDto, existingManga);
        await _mangaRepository.UpdateAsync(existingManga);
    }

    public async Task DeleteAsync(int id)
    {
        if (!await _mangaRepository.ExistsAsync(id))
        {
            throw new KeyNotFoundException($"Manga with ID {id} not found");
        }
        await _mangaRepository.DeleteAsync(id);
    }
}