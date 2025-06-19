// MangaMechiApi.Application.DTOs/PaginationRequestDto.cs
namespace MangaMechiApi.Application.DTOs;

public class PaginationRequestDto
{
    private const int MaxPageSize = 50; // Define un tamaño máximo de página para evitar abusos
    public int PageNumber { get; set; } = 1; // Página por defecto es la 1

    private int _pageSize = 10; // Tamaño de página por defecto
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value; // Asegura que el tamaño de página no exceda el máximo
    }
}