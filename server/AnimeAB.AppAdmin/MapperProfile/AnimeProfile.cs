using AnimeAB.AppAdmin.AnimeEndpoints;
using AnimeAB.AppAdmin.MapperProfile.Hook;
using AnimeAB.Core.Controllers;
using AnimeAB.Domain;
using AnimeAB.Domain.Entities;
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

            //Insert Anime
            CreateMap<AnimeRequest, Animes>()
                .ForMember(dto => dto.Categories,
                conf => conf.MapFrom(opt => new Dictionary<string, Categories>()))
                .ForMember(dto => dto.CollectionId,
                conf => conf.MapFrom(opt => opt.MapperCollect()));
            //Update Anime
            CreateMap<AnimeRequest, AnimesDomain>()
                .ForMember(dto => dto.Categories,
                conf => conf.MapFrom(opt =>  new Dictionary<string, Categories>()))
                .ForMember(dto => dto.CollectionId,
                conf => conf.MapFrom(opt => opt.MapperCollect()));
            //Upgrade anime detail
            CreateMap<AnimeDetailRequest, AnimeDetailed>();

            CreateMap<CollectionDto, Collections>()
               .ForMember(dto => dto.FileName,
                       conf => conf.MapFrom(opt => opt.FileUpload.FileName))
               .ReverseMap();
        }
    }
}
