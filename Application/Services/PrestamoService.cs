using AutoMapper;
using MangaMechiApi.Core.Entities;
using MangaMechiApi.Core.Interfaces;
using MangaMechiApi.Application.DTOs;

namespace MangaMechiApi.Application.Services;

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
    }

    public async Task<IEnumerable<PrestamoDto>> GetAllAsync()
    {
        var prestamos = await _prestamoRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<PrestamoDto>>(prestamos);
    }

    public async Task<PrestamoDto?> GetByIdAsync(int id)
    {
        var prestamo = await _prestamoRepository.GetByIdAsync(id);
        return prestamo != null ? _mapper.Map<PrestamoDto>(prestamo) : null;
    }

    public async Task<IEnumerable<PrestamoDto>> GetByMangaIdAsync(int mangaId)
    {
        var prestamos = await _prestamoRepository.GetByMangaIdAsync(mangaId);
        return _mapper.Map<IEnumerable<PrestamoDto>>(prestamos);
    }

    public async Task<PrestamoDto> CreateAsync(PrestamoCreateDto prestamoDto)
    {
        var manga = await _mangaRepository.GetByIdAsync(prestamoDto.MangaId);
        if (manga == null)
        {
            throw new KeyNotFoundException($"Manga with ID {prestamoDto.MangaId} not found");
        }
        var prestamo = _mapper.Map<Prestamo>(prestamoDto);
        prestamo.FechaPrestamo = DateTime.UtcNow;
        prestamo.Estado = "Prestado";
        var createdPrestamo = await _prestamoRepository.CreateAsync(prestamo);
        return _mapper.Map<PrestamoDto>(createdPrestamo);
    }

    public async Task UpdateAsync(int id, PrestamoUpdateDto prestamoDto)
    {
        var existingPrestamo = await _prestamoRepository.GetByIdAsync(id);
        if (existingPrestamo == null)
        {
            throw new KeyNotFoundException($"Prestamo with ID {id} not found");
        }
        _mapper.Map(prestamoDto, existingPrestamo);
        await _prestamoRepository.UpdateAsync(existingPrestamo);
    }

    public async Task DeleteAsync(int id)
    {
        if (!await _prestamoRepository.ExistsAsync(id))
        {
            throw new KeyNotFoundException($"Prestamo with ID {id} not found");
        }
        await _prestamoRepository.DeleteAsync(id);
    }
}
