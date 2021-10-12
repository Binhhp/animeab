using AnimeAB.ApiIntegration.AnimeEndpoints;
using AnimeAB.Core.ApiResponse;
using AnimeAB.Domain.Entities;
using AutoMapper;

namespace AnimeAB.ApiIntegration.MapperProfile
{
    public class AnimeRankProfile : Profile
    {
        public AnimeRankProfile()
        {
            CreateMap<Animes, AnimeRankDayResponse>()
                .ForMember(dto => dto.Views,
                conf => conf.MapFrom(opt => opt.ViewDays))
               .ForMember(dto => dto.Link,
               conf => conf.MapFrom(opt => opt.IsStatus < 3 ? opt.LinkEnd : opt.LinkStart));

            CreateMap<Animes, AnimeRankWeekResponse>()
                .ForMember(dto => dto.Views,
                conf => conf.MapFrom(opt => opt.ViewWeeks))
               .ForMember(dto => dto.Link,
              conf => conf.MapFrom(opt => opt.IsStatus < 3 ? opt.LinkEnd : opt.LinkStart));

            CreateMap<Animes, AnimeRankMonthResponse>()
                .ForMember(dto => dto.Views,
                conf => conf.MapFrom(opt => opt.ViewMonths))
               .ForMember(dto => dto.Link,
              conf => conf.MapFrom(opt => opt.IsStatus < 3 ? opt.LinkEnd : opt.LinkStart));
        }
    }
}
