using AnimeAB.Domain.DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimeAB.Core.MapperProfile
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<ClientDto, AccountSignUpDto>()
               .ForMember(dto => dto.Email,
                       conf => conf.MapFrom(opt => opt.email))
               .ForMember(dto => dto.FullName,
                       conf => conf.MapFrom(opt => opt.name))
               .ForMember(dto => dto.Password,
                       conf => conf.MapFrom(opt => opt.password))
               .ForMember(dto => dto.ConfirmPassword,
                       conf => conf.MapFrom(opt => opt.confirm_password))
               .ForMember(dto => dto.Role,
                       conf => conf.MapFrom(opt => "Client"))
               .ReverseMap();
        }
    }
}
