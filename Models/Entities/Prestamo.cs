using System.ComponentModel.DataAnnotations;

namespace MangaMechiApi.Models.Entities;

public class Prestamo
{
    public int Id { get; set; }

    public int MangaId { get; set; }
    public Manga? Manga { get; set; }

    [Required]
    public DateTime FechaPrestamo { get; set; }

    public DateTime? FechaDevolucion { get; set; }

    [Required]
    [StringLength(100)]
    public string Prestatario { get; set; } = string.Empty;

    [Required]
    public string Estado { get; set; } = "Prestado";
}
