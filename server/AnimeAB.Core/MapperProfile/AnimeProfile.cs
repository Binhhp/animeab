using AnimeAB.Core.ApiResponse;
using AnimeAB.Core.Controllers;
using AnimeAB.Core.MapperProfile.MapperMember;
using AnimeAB.Reponsitories.Domain;
using AnimeAB.Reponsitories.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimeAB.Core.MapperProfile
{
    public class AnimeProfile : Profile
    {
        public AnimeProfile()
        {
            CreateMap<Animes, AnimeResponse>()
               .ForMember(dto => dto.Collection,
               conf => conf.MapFrom(opt => opt.CollectionId))
               .ForMember(dto => dto.Link,
               conf => conf.MapFrom(opt => opt.IsStatus < 3 ? opt.LinkEnd : opt.LinkStart));

            CreateMap<AnimeDto, Animes>()
                .ForMember(dto => dto.Categories,
                conf => conf.MapFrom(opt => new Dictionary<string, Categories>()))
                .ForMember(dto => dto.CollectionId,
                conf => conf.MapFrom(opt => opt.MapperCollect()));

            CreateMap<AnimeDto, AnimesDomain>()
                .ForMember(dto => dto.Categories,
                conf => conf.MapFrom(opt =>  new Dictionary<string, Categories>()))
                .ForMember(dto => dto.CollectionId,
                conf => conf.MapFrom(opt => opt.MapperCollect()));

            CreateMap<AnimeDetailDto, AnimeDetailed>();

            CreateMap<CollectionDto, Collections>()
               .ForMember(dto => dto.FileName,
                       conf => conf.MapFrom(opt => opt.FileUpload.FileName))
               .ReverseMap();
        }
    }
}
