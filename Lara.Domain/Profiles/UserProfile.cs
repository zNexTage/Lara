using AutoMapper;
using Lara.Domain.DataTransferObjects;
using Lara.Domain.Entities;

namespace Lara.Domain.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserDto, ApplicationUser>()
            .ForMember(dest => dest.UserName, 
                opt => 
                    opt.MapFrom(source => source.Email));
        CreateMap<ApplicationUser, ReadUserDto>();
    }
}