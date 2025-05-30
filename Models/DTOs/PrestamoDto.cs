namespace MangaMechiApi.Models.DTOs;

public class PrestamoDto
{
    public int Id { get; set; }
    public int MangaId { get; set; }
    public string MangaTitle { get; set; } = string.Empty;
    public DateTime FechaPrestamo { get; set; }
    public DateTime? FechaDevolucion { get; set; }
    public string Prestatario { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
}

public class PrestamoCreateDto
{
    public int MangaId { get; set; }
    public string Prestatario { get; set; } = string.Empty;
}

public class PrestamoUpdateDto
{
    public DateTime? FechaDevolucion { get; set; }
    public string Estado { get; set; } = string.Empty;
}
