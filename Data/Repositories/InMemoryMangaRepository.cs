using MangaMechiApi.Models.Entities;

namespace MangaMechiApi.Data.Repositories;

public class InMemoryMangaRepository : IMangaRepository
{
    // Usar una lista estática para mantener los datos entre instancias
    private static readonly List<Manga> _mangas = new();
    private static int _nextId = 1;    public InMemoryMangaRepository()
    {
        // Inicializar datos solo si la lista está vacía
        if (!_mangas.Any())
        {
            // Seed initial data
            _mangas.AddRange(new[]
            {
            new Manga { Id = _nextId++, Title = "One Piece", Author = "Eiichiro Oda", Genre = "Shonen", Year = 1997, Status = "En publicación", Description = "Aventuras piratas en busca del One Piece" },
            new Manga { Id = _nextId++, Title = "Attack on Titan", Author = "Hajime Isayama", Genre = "Acción/Drama", Year = 2009, Status = "Finalizado", Description = "Humanidad luchando contra titanes" },
            new Manga { Id = _nextId++, Title = "Your Lie in April", Author = "Naoshi Arakawa", Genre = "Romance/Drama", Year = 2011, Status = "Finalizado", Description = "Historia de música y amor" },
            new Manga { Id = _nextId++, Title = "Demon Slayer", Author = "Koyoharu Gotouge", Genre = "Shonen", Year = 2016, Status = "Finalizado", Description = "Cazadores de demonios en la era Taisho" },            new Manga { Id = _nextId++, Title = "Jujutsu Kaisen", Author = "Gege Akutami", Genre = "Shonen", Year = 2018, Status = "En publicación", Description = "Estudiantes combatiendo maldiciones" }
            });
        }
    }

    public Task<IEnumerable<Manga>> GetAllAsync()
    {
        return Task.FromResult(_mangas.AsEnumerable());
    }

    public Task<Manga?> GetByIdAsync(int id)
    {
        return Task.FromResult(_mangas.FirstOrDefault(m => m.Id == id));
    }

    public Task<IEnumerable<Manga>> GetByGenreAsync(string genre)
    {
        return Task.FromResult(_mangas.Where(m => m.Genre.Contains(genre, StringComparison.OrdinalIgnoreCase)));
    }

    public Task<Manga> CreateAsync(Manga manga)
    {
        manga.Id = _nextId++;
        _mangas.Add(manga);
        return Task.FromResult(manga);
    }

    public Task UpdateAsync(Manga manga)
    {
        var index = _mangas.FindIndex(m => m.Id == manga.Id);
        if (index != -1)
        {
            _mangas[index] = manga;
        }
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        var manga = _mangas.FirstOrDefault(m => m.Id == id);
        if (manga != null)
        {
            _mangas.Remove(manga);
        }
        return Task.CompletedTask;
    }

    public Task<bool> ExistsAsync(int id)
    {
        return Task.FromResult(_mangas.Any(m => m.Id == id));
    }
}
