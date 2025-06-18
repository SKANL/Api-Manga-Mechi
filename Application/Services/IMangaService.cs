using MangaMechiApi.Application.DTOs;

namespace MangaMechiApi.Application.Services;

public interface IMangaService
{
    Task<IEnumerable<MangaDto>> GetAllAsync();
    Task<MangaDto?> GetByIdAsync(int id);
    Task<IEnumerable<MangaDto>> GetByGenreAsync(string genre);
    Task<MangaDto> CreateAsync(MangaCreateDto mangaDto);
    Task UpdateAsync(int id, MangaUpdateDto mangaDto);
    Task DeleteAsync(int id);
}
