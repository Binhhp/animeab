using AnimeAB.ApiIntegration.MapperProfile;
using AnimeAB.Core.ApiResponse;
using AnimeAB.Domain.Entities;
using AutoMapper;

namespace AnimeAB.Core.MapperProfile
{
    public class EpisodeProfile : Profile
    {
        public EpisodeProfile()
        {
            //Link auto get
            CreateMap<AnimeDetailed, EpisodeResponse>()
                .ForMember(dto => dto.Link,
                conf => conf.MapFrom(opt => opt.MapperLink()))
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
                conf => conf.MapFrom(opt => opt.MapperLink()))
                .ForMember(dto => dto.Type,
                conf => conf.MapFrom(opt => opt.MapperType("animevsub")));

            //Link server hdx
            CreateMap<AnimeDetailed, EpisodeHDX>()
                .ForMember(dto => dto.Link,
                conf => conf.MapFrom(opt => opt.MapperLinkHDX()))
                .ForMember(dto => dto.Type,
                    conf => conf.MapFrom(opt => opt.MapperType("hdx")));
        }
    }
}
