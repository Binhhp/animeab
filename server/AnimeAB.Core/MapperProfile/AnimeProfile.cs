using AnimeAB.Core.Controllers;
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
            CreateMap<AnimeDto, Animes>();

            CreateMap<AnimeDto, AnimesDomain>();

            CreateMap<AnimeDetailDto, AnimeDetailed>();

            CreateMap<CollectionDto, Collections>()
               .ForMember(dto => dto.FileName,
                       conf => conf.MapFrom(opt => opt.FileUpload.FileName))
               .ReverseMap();
        }
    }
}
