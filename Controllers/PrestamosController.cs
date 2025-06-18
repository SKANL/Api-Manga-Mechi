using Microsoft.AspNetCore.Mvc;
using MangaMechiApi.Application.DTOs;
using MangaMechiApi.Application.Services;

namespace MangaMechiApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrestamosController : ControllerBase
{
    private readonly IPrestamoService _prestamoService;
    private readonly ILogger<PrestamosController> _logger;

    public PrestamosController(IPrestamoService prestamoService, ILogger<PrestamosController> logger)
    {
        _prestamoService = prestamoService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PrestamoDto>>> GetAll()
    {
        try
        {
            var prestamos = await _prestamoService.GetAllAsync();
            return Ok(prestamos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting prestamos");
            return StatusCode(500, "An error occurred while processing your request");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PrestamoDto>> GetById(int id)
    {
        try
        {
            var prestamo = await _prestamoService.GetByIdAsync(id);
            if (prestamo == null)
            {
                return NotFound($"Préstamo with ID {id} not found");
            }

            return Ok(prestamo);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting prestamo with ID {Id}", id);
            return StatusCode(500, "An error occurred while processing your request");
        }
    }

    [HttpPost]
    public async Task<ActionResult<PrestamoDto>> Create(PrestamoCreateDto prestamoDto)
    {
        try
        {
            var createdPrestamo = await _prestamoService.CreateAsync(prestamoDto);
            return CreatedAtAction(nameof(GetById), new { id = createdPrestamo.Id }, createdPrestamo);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating prestamo");
            return StatusCode(500, "An error occurred while processing your request");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, PrestamoUpdateDto prestamoDto)
    {
        try
        {
            await _prestamoService.UpdateAsync(id, prestamoDto);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating prestamo with ID {Id}", id);
            return StatusCode(500, "An error occurred while processing your request");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _prestamoService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting prestamo with ID {Id}", id);
            return StatusCode(500, "An error occurred while processing your request");
        }
    }
}
