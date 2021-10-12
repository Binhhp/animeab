using AnimeAB.ApiIntegration.AccountEndpoints;
using AnimeAB.Domain.DTOs;
using AutoMapper;

namespace AnimeAB.ApiIntegration.MapperProfile
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<AccountRequest, AccountSignUpDto>()
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
