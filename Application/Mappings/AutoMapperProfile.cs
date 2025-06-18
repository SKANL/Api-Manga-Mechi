using AutoMapper;
using MangaMechiApi.Core.Entities;
using MangaMechiApi.Application.DTOs;

namespace MangaMechiApi.Application.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Manga, MangaDto>();
        CreateMap<MangaCreateDto, Manga>();
        CreateMap<MangaUpdateDto, Manga>();

        CreateMap<Prestamo, PrestamoDto>()
            .ForMember(dest => dest.MangaTitle, 
                opt => opt.MapFrom(src => src.Manga != null ? src.Manga.Title : string.Empty));
        CreateMap<PrestamoCreateDto, Prestamo>();
        CreateMap<PrestamoUpdateDto, Prestamo>();
    }
}
