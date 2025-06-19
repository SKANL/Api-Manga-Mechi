// MangaMechiApi.Core.Interfaces/IMangaService.cs
using MangaMechiApi.Application.DTOs; // Necesario para PagedResultDto y PaginationRequestDto

namespace MangaMechiApi.Application.Services;

public interface IMangaService
{
    // Método existente
    Task<IEnumerable<MangaDto>> GetAllAsync(); // Mantener por si hay otros usos no paginados

    // Nuevo método para paginación general
    Task<PagedResultDto<MangaDto>> GetAllPagedAsync(PaginationRequestDto pagination);

    Task<MangaDto?> GetByIdAsync(int id);
    
    // Modificar GetByGenreAsync para aceptar paginación
    Task<PagedResultDto<MangaDto>> GetByGenreAsync(string genre, PaginationRequestDto pagination);

    Task<MangaDto> CreateAsync(MangaCreateDto mangaDto);
    Task UpdateAsync(int id, MangaUpdateDto mangaDto);
    Task DeleteAsync(int id);
}