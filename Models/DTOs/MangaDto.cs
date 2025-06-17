namespace MangaMechiApi.Models.DTOs;

public class MangaDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Authors { get; set; }
    public int? Volumes { get; set; }
    public int? Chapters { get; set; }
    public string? Status { get; set; }
    public string? Published { get; set; }
    public string? Genres { get; set; }
    public string? Synopsis { get; set; }
    public string? ImageUrl { get; set; }
}

public class MangaCreateDto
{
    public string? Title { get; set; }
    public string? Authors { get; set; }
    public int? Volumes { get; set; }
    public int? Chapters { get; set; }
    public string? Status { get; set; }
    public string? Published { get; set; }
    public string? Genres { get; set; }
    public string? Synopsis { get; set; }
    public string? ImageUrl { get; set; }
}

public class MangaUpdateDto
{
    public string? Title { get; set; }
    public string? Authors { get; set; }
    public int? Volumes { get; set; }
    public int? Chapters { get; set; }
    public string? Status { get; set; }
    public string? Published { get; set; }
    public string? Genres { get; set; }
    public string? Synopsis { get; set; }
    public string? ImageUrl { get; set; }
}
