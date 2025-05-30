using MangaMechiApi.Models.Entities;

namespace MangaMechiApi.Data.Repositories;

public interface IPrestamoRepository
{    Task<IEnumerable<Prestamo>> GetAllAsync();
    Task<Prestamo?> GetByIdAsync(int id);
    Task<IEnumerable<Prestamo>> GetByMangaIdAsync(int mangaId);
    Task<Prestamo> CreateAsync(Prestamo prestamo);
    Task UpdateAsync(Prestamo prestamo);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
