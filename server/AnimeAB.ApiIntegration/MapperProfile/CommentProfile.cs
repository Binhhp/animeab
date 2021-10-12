using AnimeAB.Domain.DTOs;
using AnimeAB.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimeAB.ApiIntegration.MapperProfile
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<CommentRequest, Comment>()
               .ForMember(dto => dto.Key,
                       conf => conf.MapFrom(opt => opt.key))
               .ForMember(dto => dto.UserLocal,
                       conf => conf.MapFrom(opt => opt.user_local))
               .ForMember(dto => dto.Message,
                       conf => conf.MapFrom(opt => opt.message))
               .ForMember(dto => dto.When,
                       conf => conf.MapFrom(opt => opt.when))
               .ForMember(dto => dto.ReplyComment,
                       conf => conf.MapFrom(opt => opt.reply_comment))
               .ReverseMap();
        }
    }
}
