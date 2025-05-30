using MangaMechiApi.Models.DTOs;

namespace MangaMechiApi.Services.Interfaces;

public interface IMangaService
{
    Task<IEnumerable<MangaDto>> GetAllAsync();
    Task<MangaDto?> GetByIdAsync(int id);
    Task<IEnumerable<MangaDto>> GetByGenreAsync(string genre);
    Task<MangaDto> CreateAsync(MangaCreateDto mangaDto);
    Task UpdateAsync(int id, MangaUpdateDto mangaDto);
    Task DeleteAsync(int id);
}
