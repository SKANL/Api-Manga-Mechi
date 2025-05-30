using AutoMapper;
using MangaMechiApi.Data.Repositories;
using MangaMechiApi.Models.DTOs;
using MangaMechiApi.Models.Entities;
using MangaMechiApi.Services.Interfaces;

namespace MangaMechiApi.Services.Implementations;

public class PrestamoService : IPrestamoService
{
    private readonly IPrestamoRepository _prestamoRepository;
    private readonly IMangaRepository _mangaRepository;
    private readonly IMapper _mapper;

    public PrestamoService(
        IPrestamoRepository prestamoRepository,
        IMangaRepository mangaRepository,
        IMapper mapper)
    {
        _prestamoRepository = prestamoRepository;
        _mangaRepository = mangaRepository;
        _mapper = mapper;
    }    public async Task<IEnumerable<PrestamoDto>> GetAllAsync()
    {
        var prestamos = await _prestamoRepository.GetAllAsync();
        foreach (var prestamo in prestamos)
        {
            // Cargar el manga asociado a cada préstamo
            var manga = await _mangaRepository.GetByIdAsync(prestamo.MangaId);
            prestamo.Manga = manga;
        }
        return _mapper.Map<IEnumerable<PrestamoDto>>(prestamos);
    }public async Task<PrestamoDto?> GetByIdAsync(int id)
    {
        var prestamo = await _prestamoRepository.GetByIdAsync(id);
        if (prestamo != null)
        {
            // Cargar el manga asociado al préstamo
            var manga = await _mangaRepository.GetByIdAsync(prestamo.MangaId);
            prestamo.Manga = manga;
            return _mapper.Map<PrestamoDto>(prestamo);
        }
        return null;
    }

    public async Task<IEnumerable<PrestamoDto>> GetByMangaIdAsync(int mangaId)
    {
        var prestamos = await _prestamoRepository.GetByMangaIdAsync(mangaId);
        return _mapper.Map<IEnumerable<PrestamoDto>>(prestamos);
    }    public async Task<PrestamoDto> CreateAsync(PrestamoCreateDto prestamoDto)
    {
        // Verificar que el manga existe y cargarlo
        var manga = await _mangaRepository.GetByIdAsync(prestamoDto.MangaId);
        if (manga == null)
        {
            throw new KeyNotFoundException($"Manga with ID {prestamoDto.MangaId} not found");
        }

        var prestamo = _mapper.Map<Prestamo>(prestamoDto);
        prestamo.FechaPrestamo = DateTime.UtcNow;
        prestamo.Estado = "Prestado";
        
        var createdPrestamo = await _prestamoRepository.CreateAsync(prestamo);
        createdPrestamo.Manga = manga; // Asignar el manga cargado

        return _mapper.Map<PrestamoDto>(createdPrestamo);
    }    public async Task UpdateAsync(int id, PrestamoUpdateDto prestamoDto)
    {
        var existingPrestamo = await _prestamoRepository.GetByIdAsync(id);
        if (existingPrestamo == null)
        {
            throw new KeyNotFoundException($"Préstamo with ID {id} not found");
        }

        // Actualizar solo los campos proporcionados en el DTO
        if (prestamoDto.FechaDevolucion.HasValue)
            existingPrestamo.FechaDevolucion = prestamoDto.FechaDevolucion;
        if (!string.IsNullOrEmpty(prestamoDto.Estado))
            existingPrestamo.Estado = prestamoDto.Estado;

        await _prestamoRepository.UpdateAsync(existingPrestamo);
    }

    public async Task DeleteAsync(int id)
    {
        if (!await _prestamoRepository.ExistsAsync(id))
        {
            throw new KeyNotFoundException($"Préstamo with ID {id} not found");
        }

        await _prestamoRepository.DeleteAsync(id);
    }
}
