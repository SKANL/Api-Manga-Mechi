using AutoMapper;
using MangaMechiApi.Models.DTOs;
using MangaMechiApi.Models.Entities;

namespace MangaMechiApi.Mapping;

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
