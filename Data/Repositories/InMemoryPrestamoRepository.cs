using MangaMechiApi.Models.Entities;

namespace MangaMechiApi.Data.Repositories;

public class InMemoryPrestamoRepository : IPrestamoRepository
{
    // Usar una lista estática para mantener los datos entre instancias
    private static readonly List<Prestamo> _prestamos = new();
    private static int _nextId = 1;    public InMemoryPrestamoRepository()
    {
        // Inicializar datos solo si la lista está vacía
        if (!_prestamos.Any())
        {
            // Datos de prueba iniciales
            _prestamos.AddRange(new[]
            {
            new Prestamo 
            { 
                Id = _nextId++, 
                MangaId = 1, // One Piece
                FechaPrestamo = DateTime.UtcNow.AddDays(-10),
                Prestatario = "Juan Pérez",
                Estado = "Prestado"
            },
            new Prestamo 
            { 
                Id = _nextId++, 
                MangaId = 3, // Your Lie in April
                FechaPrestamo = DateTime.UtcNow.AddDays(-5),
                FechaDevolucion = DateTime.UtcNow.AddDays(-2),
                Prestatario = "Ana García",
                Estado = "Devuelto"
            },
            new Prestamo 
            { 
                Id = _nextId++, 
                MangaId = 4, // Demon Slayer
                FechaPrestamo = DateTime.UtcNow.AddDays(-3),
                Prestatario = "Carlos Rodríguez",                Estado = "Prestado"
            }
            });
        }
    }

    public Task<IEnumerable<Prestamo>> GetAllAsync()
    {
        return Task.FromResult(_prestamos.AsEnumerable());
    }

    public Task<Prestamo?> GetByIdAsync(int id)
    {
        return Task.FromResult(_prestamos.FirstOrDefault(p => p.Id == id));
    }

    public Task<IEnumerable<Prestamo>> GetByMangaIdAsync(int mangaId)
    {
        return Task.FromResult(_prestamos.Where(p => p.MangaId == mangaId));
    }    public Task<Prestamo> CreateAsync(Prestamo prestamo)
    {
        var newPrestamo = new Prestamo
        {
            Id = _nextId++,
            MangaId = prestamo.MangaId,
            FechaPrestamo = prestamo.FechaPrestamo,
            Prestatario = prestamo.Prestatario,
            Estado = prestamo.Estado,
            Manga = prestamo.Manga // Mantener la referencia al manga si existe
        };
        
        _prestamos.Add(newPrestamo);
        return Task.FromResult(newPrestamo);
    }public Task UpdateAsync(Prestamo prestamo)
    {
        var index = _prestamos.FindIndex(p => p.Id == prestamo.Id);
        if (index != -1)
        {
            // Mantener los valores existentes que no se actualizan
            var existingPrestamo = _prestamos[index];
            if (prestamo.FechaPrestamo == default)
                prestamo.FechaPrestamo = existingPrestamo.FechaPrestamo;
            if (prestamo.MangaId == 0)
                prestamo.MangaId = existingPrestamo.MangaId;
            if (string.IsNullOrEmpty(prestamo.Prestatario))
                prestamo.Prestatario = existingPrestamo.Prestatario;
            
            _prestamos[index] = prestamo;
        }
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        var prestamo = _prestamos.FirstOrDefault(p => p.Id == id);
        if (prestamo != null)
        {
            _prestamos.Remove(prestamo);
        }
        return Task.CompletedTask;
    }

    public Task<bool> ExistsAsync(int id)
    {
        return Task.FromResult(_prestamos.Any(p => p.Id == id));
    }
}
