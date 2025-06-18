using System.ComponentModel.DataAnnotations;

namespace MangaMechiApi.Core.Entities;

public class Prestamo
{
    public int Id { get; set; }
    public int MangaId { get; set; }
    public Manga? Manga { get; set; }
    public DateTime FechaPrestamo { get; set; }
    public DateTime? FechaDevolucion { get; set; }
    public string Prestatario { get; set; } = string.Empty;
    public string Estado { get; set; } = "Prestado";
}
