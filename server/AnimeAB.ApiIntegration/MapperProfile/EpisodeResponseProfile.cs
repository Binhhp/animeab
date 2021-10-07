
using AnimeAB.Core.MapperProfile.MapperMember;
using AnimeAB.Domain.Entities;
using AnimeAB.Infrastructure.ApiResponse;
using AutoMapper;

namespace AnimeAB.Core.MapperProfile
{
    public class EpisodeResponseProfile : Profile
    {
        public EpisodeResponseProfile()
        {
            //Link auto get
            CreateMap<AnimeDetailed, EpisodeResponse>()
                .ForMember(dto => dto.Link,
                conf => conf.MapFrom(opt => opt.MapperLink(false)))
                .ForMember(dto => dto.Type,
                    conf => conf.MapFrom(opt => opt.MapperType("")));

            CreateMap<AnimeDetailed, EpisodeInstance>();
            //Link server vuighe
            CreateMap<AnimeDetailed, EpisodeVuighe>()
                .ForMember(dto => dto.Link,
                conf => conf.MapFrom(opt => opt.MapperVuighe()))
                .ForMember(dto => dto.Type,
                conf => conf.MapFrom(opt => opt.MapperVuigheType()));
            //Link server animevsub
            CreateMap<AnimeDetailed, EpisodeAnimeVsub>()
                .ForMember(dto => dto.Link,
                conf => conf.MapFrom(opt => opt.MapperLink(true)))
                .ForMember(dto => dto.Type,
                conf => conf.MapFrom(opt => opt.MapperType("")));
        }
    }
}
