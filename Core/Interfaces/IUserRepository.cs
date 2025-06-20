using MangaMechiApi.Core.Entities;

namespace MangaMechiApi.Core.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username);
    Task<User> CreateAsync(User user);
    // Puedes agregar métodos para actualizar, eliminar, etc.
}
