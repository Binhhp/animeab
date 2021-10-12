using AnimeAB.ApiIntegration.AnimeEndpoints;
using AnimeAB.Domain.Entities;
using AutoMapper;

namespace AnimeAB.Core.MapperProfile
{
    public class SeriesProfile : Profile
    {
        public SeriesProfile()
        {
            CreateMap<Animes, AnimeSeriesResponse>()
               .ForMember(dto => dto.Key,
               conf => conf.MapFrom(opt => opt.Key))
               .ForMember(dto => dto.AnimeTitle,
               conf => conf.MapFrom(opt => opt.Title))
               .ForMember(dto => dto.YearProduce,
               conf => conf.MapFrom(opt => opt.DateRelease.Year))
               .ForMember(dto => dto.Season,
               conf => conf.MapFrom(opt => string.IsNullOrEmpty(opt.Season) ? "TV Series" : opt.Season))
               .ForMember(dto => dto.Link,
               conf => conf.MapFrom(opt => opt.IsStatus < 3 ? opt.LinkEnd : opt.LinkStart));
        }
    }
}
