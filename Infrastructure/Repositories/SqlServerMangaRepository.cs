// MangaMechiApi.Infrastructure.Repositories/SqlServerMangaRepository.cs
using Microsoft.EntityFrameworkCore;
using MangaMechiApi.Core.Entities;
using MangaMechiApi.Core.Interfaces;
using MangaMechiApi.Infrastructure.Data;
using MangaMechiApi.Application.DTOs; // Necesario para PaginationRequestDto

namespace MangaMechiApi.Infrastructure.Repositories;

public class SqlServerMangaRepository : BaseRepository, IMangaRepository
{
    public SqlServerMangaRepository(ApplicationDbContext context, DatabaseSettings settings)
        : base(context, settings)
    {
    }

    public async Task<IEnumerable<Manga>> GetAllAsync()
    {
        return await _context.Mangas.ToListAsync();
    }

    // Implementación del nuevo método paginado
    public async Task<(IEnumerable<Manga> Items, int TotalCount)> GetAllPagedAsync(PaginationRequestDto pagination)
    {
        var query = _context.Mangas.AsQueryable();
        var totalCount = await query.CountAsync(); // Obtener el total antes de paginar

        var items = await query
            .Skip((pagination.PageNumber - 1) * pagination.PageSize) // Saltar los elementos de páginas anteriores
            .Take(pagination.PageSize) // Tomar solo los elementos de la página actual
            .ToListAsync();

        return (items, totalCount);
    }

    public async Task<Manga?> GetByIdAsync(int id)
    {
        return await _context.Mangas.FindAsync(id);
    }

    // Modificación de GetByGenreAsync para paginación
    public async Task<(IEnumerable<Manga> Items, int TotalCount)> GetByGenreAsync(string genre, PaginationRequestDto pagination)
    {
        var query = _context.Mangas
            .Where(m => m.Genres != null && m.Genres.Contains(genre))
            .AsQueryable(); // Convertir a IQueryable para encadenar operaciones

        var totalCount = await query.CountAsync(); // Obtener el total antes de paginar

        var items = await query
            .Skip((pagination.PageNumber - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();

        return (items, totalCount);
    }

    public async Task<Manga> CreateAsync(Manga manga)
    {
        _context.Mangas.Add(manga);
        await SaveChangesAsync();
        return manga;
    }

    public async Task UpdateAsync(Manga manga)
    {
        _context.Entry(manga).State = EntityState.Modified;
        await SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var manga = await _context.Mangas.FindAsync(id);
        if (manga != null)
        {
            _context.Mangas.Remove(manga);
            await SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Mangas.AnyAsync(m => m.Id == id);
    }
}