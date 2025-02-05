using AutoMapper;
using Users.Core.Dto;
using Users.Core.Entities;

namespace Users.Api.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserDto, User>()
            .ReverseMap().
            ForMember(dest => dest.Age, opt => opt.MapFrom<AgeCalculator>());
    }
}
