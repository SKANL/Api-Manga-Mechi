// MangaMechiApi.Core.Interfaces/IPrestamoService.cs
using MangaMechiApi.Application.DTOs; // Necesario para PagedResultDto y PaginationRequestDto

namespace MangaMechiApi.Application.Services;

public interface IPrestamoService
{
    // Método existente
    Task<IEnumerable<PrestamoDto>> GetAllAsync(); // Mantener por si hay otros usos no paginados

    // Nuevo método para paginación general
    Task<PagedResultDto<PrestamoDto>> GetAllPagedAsync(PaginationRequestDto pagination);

    Task<PrestamoDto?> GetByIdAsync(int id);
    
    // Modificar GetByMangaIdAsync para aceptar paginación
    Task<PagedResultDto<PrestamoDto>> GetByMangaIdAsync(int mangaId, PaginationRequestDto pagination);

    Task<PrestamoDto> CreateAsync(PrestamoCreateDto prestamoDto);
    Task UpdateAsync(int id, PrestamoUpdateDto prestamoDto);
    Task DeleteAsync(int id);

    // Nuevo método para filtrar préstamos por estado
    Task<PagedResultDto<PrestamoDto>> GetAllPagedByEstadoAsync(PrestamoPagedRequestDto request);
}