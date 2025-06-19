// MangaMechiApi.Infrastructure.Repositories/SqlServerPrestamoRepository.cs
using Microsoft.EntityFrameworkCore;
using MangaMechiApi.Core.Entities;
using MangaMechiApi.Core.Interfaces;
using MangaMechiApi.Infrastructure.Data;
using MangaMechiApi.Application.DTOs; // Necesario para PaginationRequestDto

namespace MangaMechiApi.Infrastructure.Repositories;

public class SqlServerPrestamoRepository : BaseRepository, IPrestamoRepository
{
    public SqlServerPrestamoRepository(ApplicationDbContext context, DatabaseSettings settings)
        : base(context, settings)
    {
    }

    public async Task<IEnumerable<Prestamo>> GetAllAsync()
    {
        return await _context.Prestamos
            .Include(p => p.Manga)
            .ToListAsync();
    }

    // Implementación del nuevo método paginado
    public async Task<(IEnumerable<Prestamo> Items, int TotalCount)> GetAllPagedAsync(PaginationRequestDto pagination)
    {
        var query = _context.Prestamos
            .Include(p => p.Manga) // Asegurarse de incluir el Manga para el DTO
            .AsQueryable();

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((pagination.PageNumber - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();

        return (items, totalCount);
    }

    public async Task<Prestamo?> GetByIdAsync(int id)
    {
        return await _context.Prestamos
            .Include(p => p.Manga)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    // Modificación de GetByMangaIdAsync para paginación
    public async Task<(IEnumerable<Prestamo> Items, int TotalCount)> GetByMangaIdAsync(int mangaId, PaginationRequestDto pagination)
    {
        var query = _context.Prestamos
            .Include(p => p.Manga)
            .Where(p => p.MangaId == mangaId)
            .AsQueryable();

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((pagination.PageNumber - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();

        return (items, totalCount);
    }

    public async Task<Prestamo> CreateAsync(Prestamo prestamo)
    {
        _context.Prestamos.Add(prestamo);
        await SaveChangesAsync();
        return prestamo;
    }

    public async Task UpdateAsync(Prestamo prestamo)
    {
        _context.Entry(prestamo).State = EntityState.Modified;
        await SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var prestamo = await _context.Prestamos.FindAsync(id);
        if (prestamo != null)
        {
            _context.Prestamos.Remove(prestamo);
            await SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Prestamos.AnyAsync(p => p.Id == id);
    }

    public async Task<(IEnumerable<Prestamo> Items, int TotalCount)> GetAllPagedByEstadoAsync(PrestamoPagedRequestDto request)
    {
        var query = _context.Prestamos
            .Include(p => p.Manga)
            .AsQueryable();

        if (!string.IsNullOrEmpty(request.Estado))
        {
            query = query.Where(p => p.Estado == request.Estado);
        }

        var totalCount = await query.CountAsync();
        var items = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        return (items, totalCount);
    }
}