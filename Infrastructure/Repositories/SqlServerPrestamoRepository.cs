using Microsoft.EntityFrameworkCore;
using MangaMechiApi.Core.Entities;
using MangaMechiApi.Core.Interfaces;
using MangaMechiApi.Infrastructure.Data;

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

    public async Task<Prestamo?> GetByIdAsync(int id)
    {
        return await _context.Prestamos
            .Include(p => p.Manga)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Prestamo>> GetByMangaIdAsync(int mangaId)
    {
        return await _context.Prestamos
            .Include(p => p.Manga)
            .Where(p => p.MangaId == mangaId)
            .ToListAsync();
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
}
