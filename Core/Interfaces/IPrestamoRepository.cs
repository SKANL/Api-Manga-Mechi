// MangaMechiApi.Core.Interfaces/IPrestamoRepository.cs
using MangaMechiApi.Core.Entities;
using MangaMechiApi.Application.DTOs; // Necesario para PaginationRequestDto

namespace MangaMechiApi.Core.Interfaces;

public interface IPrestamoRepository
{
    // Método existente
    Task<IEnumerable<Prestamo>> GetAllAsync(); // Mantener por si hay otros usos no paginados

    // Nuevo método para paginación general
    Task<(IEnumerable<Prestamo> Items, int TotalCount)> GetAllPagedAsync(PaginationRequestDto pagination);

    Task<Prestamo?> GetByIdAsync(int id);
    
    // Modificar GetByMangaIdAsync para aceptar paginación
    Task<(IEnumerable<Prestamo> Items, int TotalCount)> GetByMangaIdAsync(int mangaId, PaginationRequestDto pagination);

    Task<Prestamo> CreateAsync(Prestamo prestamo);
    Task UpdateAsync(Prestamo prestamo);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}