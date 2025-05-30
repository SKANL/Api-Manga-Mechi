using MangaMechiApi.Models.Entities;

namespace MangaMechiApi.Data.Repositories;

public interface IMangaRepository
{
    Task<IEnumerable<Manga>> GetAllAsync();
    Task<Manga?> GetByIdAsync(int id);
    Task<IEnumerable<Manga>> GetByGenreAsync(string genre);
    Task<Manga> CreateAsync(Manga manga);
    Task UpdateAsync(Manga manga);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
