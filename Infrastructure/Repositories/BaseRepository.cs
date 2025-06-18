using Microsoft.EntityFrameworkCore;
using MangaMechiApi.Infrastructure.Data;

namespace MangaMechiApi.Infrastructure.Repositories;

public abstract class BaseRepository
{
    protected readonly ApplicationDbContext _context;
    protected readonly DatabaseSettings _settings;

    protected BaseRepository(ApplicationDbContext context, DatabaseSettings settings)
    {
        _context = context;
        _settings = settings;
    }

    protected async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
