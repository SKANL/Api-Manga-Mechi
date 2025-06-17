using Microsoft.EntityFrameworkCore;
using MangaMechiApi.Models.Entities;

namespace MangaMechiApi.Data.Repositories;

public class SqlServerPrestamoRepository : IPrestamoRepository
{
    private readonly ApplicationDbContext _context;

    public SqlServerPrestamoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Prestamo>> GetAllAsync()
    {
        return await _context.Prestamos
            .Include(p => p.Manga)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Prestamo?> GetByIdAsync(int id)
    {
        return await _context.Prestamos
            .Include(p => p.Manga)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Prestamo>> GetByMangaIdAsync(int mangaId)
    {
        return await _context.Prestamos
            .Include(p => p.Manga)
            .AsNoTracking()
            .Where(p => p.MangaId == mangaId)
            .ToListAsync();
    }

    public async Task<Prestamo> CreateAsync(Prestamo prestamo)
    {
        _context.Prestamos.Add(prestamo);
        await _context.SaveChangesAsync();
        return prestamo;
    }

    public async Task UpdateAsync(Prestamo prestamo)
    {
        _context.Entry(prestamo).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var prestamo = await _context.Prestamos.FindAsync(id);
        if (prestamo != null)
        {
            _context.Prestamos.Remove(prestamo);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Prestamos.AnyAsync(p => p.Id == id);
    }
}
