using Microsoft.AspNetCore.Mvc;
using MangaMechiApi.Application.DTOs;
using MangaMechiApi.Application.Services;
using MangaMechiApi.Core.Interfaces;

namespace MangaMechiApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MangasController : ControllerBase
{
    private readonly IMangaService _mangaService;
    private readonly ILogger<MangasController> _logger;

    public MangasController(IMangaService mangaService, ILogger<MangasController> logger)
    {
        _mangaService = mangaService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MangaDto>>> GetAll([FromQuery] string? genero = null)
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(genero))
            {
                var mangasByGenre = await _mangaService.GetByGenreAsync(genero);
                return Ok(mangasByGenre);
            }

            var mangas = await _mangaService.GetAllAsync();
            return Ok(mangas);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting mangas");
            return StatusCode(500, "An error occurred while processing your request");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MangaDto>> GetById(int id)
    {
        try
        {
            var manga = await _mangaService.GetByIdAsync(id);
            if (manga == null)
            {
                return NotFound($"Manga with ID {id} not found");
            }

            return Ok(manga);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting manga with ID {Id}", id);
            return StatusCode(500, "An error occurred while processing your request");
        }
    }

    [HttpPost]
    public async Task<ActionResult<MangaDto>> Create(MangaCreateDto mangaDto)
    {
        try
        {
            var createdManga = await _mangaService.CreateAsync(mangaDto);
            return CreatedAtAction(nameof(GetById), new { id = createdManga.Id }, createdManga);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating manga");
            return StatusCode(500, "An error occurred while processing your request");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, MangaUpdateDto mangaDto)
    {
        try
        {
            await _mangaService.UpdateAsync(id, mangaDto);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating manga with ID {Id}", id);
            return StatusCode(500, "An error occurred while processing your request");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _mangaService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting manga with ID {Id}", id);
            return StatusCode(500, "An error occurred while processing your request");
        }
    }
}
