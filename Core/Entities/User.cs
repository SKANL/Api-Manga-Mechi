using System.ComponentModel.DataAnnotations;

namespace MangaMechiApi.Core.Entities;

public class User
{
    public int Id { get; set; }
    [Required]
    public string Username { get; set; } = string.Empty;
    [Required]
    public string PasswordHash { get; set; } = string.Empty;
    // Puedes agregar más propiedades como Email, Roles, etc.
}
