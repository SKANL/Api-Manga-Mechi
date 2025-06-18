using MangaMechiApi.Application.DTOs;

namespace MangaMechiApi.Application.Services;

public interface IPrestamoService
{
    Task<IEnumerable<PrestamoDto>> GetAllAsync();
    Task<PrestamoDto?> GetByIdAsync(int id);
    Task<IEnumerable<PrestamoDto>> GetByMangaIdAsync(int mangaId);
    Task<PrestamoDto> CreateAsync(PrestamoCreateDto prestamoDto);
    Task UpdateAsync(int id, PrestamoUpdateDto prestamoDto);
    Task DeleteAsync(int id);
}
