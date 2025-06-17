using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MangaMechiApi.Models.Entities;

[Table("mangas", Schema = "dbo")]
public class Manga
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("title")]
    [StringLength(255)]
    public string? Title { get; set; }

    [Column("authors")]
    public string? Authors { get; set; }

    [Column("volumes")]
    public int? Volumes { get; set; }

    [Column("chapters")]
    public int? Chapters { get; set; }

    [Column("status")]
    [StringLength(50)]
    public string? Status { get; set; }

    [Column("published")]
    [StringLength(100)]
    public string? Published { get; set; }

    [Column("genres")]
    public string? Genres { get; set; }

    [Column("synopsis")]
    public string? Synopsis { get; set; }

    [Column("image_url")]
    [StringLength(500)]
    public string? ImageUrl { get; set; }

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}
