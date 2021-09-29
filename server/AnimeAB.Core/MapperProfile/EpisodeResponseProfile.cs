using AnimeAB.Core.ApiResponse;
using AnimeAB.Core.MapperProfile.MapperMember;
using AnimeAB.Reponsitories.Entities;
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
                conf => conf.MapFrom(opt => opt.MapperLink()))
                .ForMember(dto => dto.Type,
                    conf => conf.MapFrom(opt => opt.MapperType("")));
            //Link server vuighe
            CreateMap<AnimeDetailed, EpisodeVuighe>()
                .ForMember(dto => dto.Link,
                conf => conf.MapFrom(opt => opt.LinkVuighe))
                .ForMember(dto => dto.Type,
                conf => conf.MapFrom(opt => "hls"));
            //Link server animevsub
            CreateMap<AnimeDetailed, EpisodeAnimeVsub>()
                .ForMember(dto => dto.Link,
                conf => conf.MapFrom(opt => opt.Link))
                .ForMember(dto => dto.Type,
                conf => conf.MapFrom(opt => opt.MapperType("animevsub")));
        }
    }
}
