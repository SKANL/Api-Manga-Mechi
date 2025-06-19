using MangaMechiApi.Application.DTOs;

namespace MangaMechiApi.Application.DTOs;

public class PrestamoPagedRequestDto : PaginationRequestDto
{
    public string? Estado { get; set; }
}
