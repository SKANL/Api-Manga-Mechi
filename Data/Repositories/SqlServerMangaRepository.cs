using Microsoft.EntityFrameworkCore;
using MangaMechiApi.Models.Entities;
using MangaMechiApi.Data;

namespace MangaMechiApi.Data.Repositories;

public class SqlServerMangaRepository : IMangaRepository
{
    private readonly ApplicationDbContext _context;

    public SqlServerMangaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Manga>> GetAllAsync()
    {
        return await _context.Mangas.ToListAsync();
    }

    public async Task<Manga?> GetByIdAsync(int id)
    {
        return await _context.Mangas.FindAsync(id);
    }

    public async Task<IEnumerable<Manga>> GetByGenreAsync(string genre)
    {
        return await _context.Mangas
            .Where(m => m.Genres != null && m.Genres.Contains(genre))
            .ToListAsync();
    }

    public async Task<Manga> CreateAsync(Manga manga)
    {
        _context.Mangas.Add(manga);
        await _context.SaveChangesAsync();
        return manga;
    }

    public async Task UpdateAsync(Manga manga)
    {
        _context.Entry(manga).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var manga = await _context.Mangas.FindAsync(id);
        if (manga != null)
        {
            _context.Mangas.Remove(manga);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Mangas.AnyAsync(m => m.Id == id);
    }
}
