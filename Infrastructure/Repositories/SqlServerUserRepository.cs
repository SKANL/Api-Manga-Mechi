using MangaMechiApi.Core.Entities;
using MangaMechiApi.Core.Interfaces;
using MangaMechiApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MangaMechiApi.Infrastructure.Repositories;

public class SqlServerUserRepository : BaseRepository, IUserRepository
{
    public SqlServerUserRepository(ApplicationDbContext context, DatabaseSettings settings)
        : base(context, settings) { }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _context.Set<User>().FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<User> CreateAsync(User user)
    {
        _context.Set<User>().Add(user);
        await SaveChangesAsync();
        return user;
    }
}
