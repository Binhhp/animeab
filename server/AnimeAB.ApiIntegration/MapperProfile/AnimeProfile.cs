
using AnimeAB.Domain.DTOs;
using AnimeAB.Domain.Entities;
using AnimeAB.Infrastructure.ApiResponse;
using AutoMapper;

namespace AnimeAB.ApiIntegration.MapperProfile
{
    public class AnimeProfile : Profile
    {
        public AnimeProfile()
        {
            //Resposne api anime
            CreateMap<Animes, AnimeResponse>()
               .ForMember(dto => dto.Collection,
               conf => conf.MapFrom(opt => opt.CollectionId))
               .ForMember(dto => dto.Link,
               conf => conf.MapFrom(opt => opt.IsStatus < 3 ? opt.LinkEnd : opt.LinkStart));
            //Response api favorite
            CreateMap<Animes, FavoriteResponse>()
               .ForMember(dto => dto.Link,
               conf => conf.MapFrom(opt => opt.IsStatus < 3 ? opt.LinkEnd : opt.LinkStart));
            //Upgrade anime detail
            CreateMap<AnimeDetailDto, AnimeDetailed>();

            CreateMap<CollectionDto, Collections>()
               .ForMember(dto => dto.FileName,
                       conf => conf.MapFrom(opt => opt.FileUpload.FileName))
               .ReverseMap();
        }
    }
}
