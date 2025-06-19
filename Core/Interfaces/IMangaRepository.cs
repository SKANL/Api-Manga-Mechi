// MangaMechiApi.Core.Interfaces/IMangaRepository.cs
using MangaMechiApi.Core.Entities;
using MangaMechiApi.Application.DTOs; // Necesario para PaginationRequestDto

namespace MangaMechiApi.Core.Interfaces;

public interface IMangaRepository
{
    // Método existente
    Task<IEnumerable<Manga>> GetAllAsync(); // Mantener por si hay otros usos no paginados

    // Nuevo método para paginación general
    Task<(IEnumerable<Manga> Items, int TotalCount)> GetAllPagedAsync(PaginationRequestDto pagination);

    Task<Manga?> GetByIdAsync(int id);
    
    // Modificar GetByGenreAsync para aceptar paginación
    Task<(IEnumerable<Manga> Items, int TotalCount)> GetByGenreAsync(string genre, PaginationRequestDto pagination);

    Task<Manga> CreateAsync(Manga manga);
    Task UpdateAsync(Manga manga);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}