using AnimeAB.ApiIntegration.AnimeEndpoints;
using AnimeAB.Core.ApiResponse;
using AnimeAB.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        }
    }
}
