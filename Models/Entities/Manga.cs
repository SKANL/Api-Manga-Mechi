using System.ComponentModel.DataAnnotations;

namespace MangaMechiApi.Models.Entities;

public class Manga
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Author { get; set; } = string.Empty;

    [Required]
    public string Genre { get; set; } = string.Empty;

    public int Year { get; set; }

    [Required]
    public string Status { get; set; } = string.Empty;

    [StringLength(500)]
    public string Description { get; set; } = string.Empty;

    public List<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}
