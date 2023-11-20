using AutoMapper;
using GIH.Entities;
using GIH.Entities.DTOs;

namespace GIH.Services.Helper;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Person, PersonDto>().ForMember(dest => dest.PersonName,
                opt =>
                {
                    opt.MapFrom(src => src.PersonName);
                })
            .ForMember(dest => dest.PersonSurname,
                opt =>
                {
                    opt.MapFrom(src => src.PersonSurname);
                })
            .ForMember(dest => dest.PersonPhoneNumber,
                opt =>
                {
                    opt.MapFrom(src => src.PersonPhoneNumber);
                }
            ) 
            .ForMember(dest => dest.PersonId,
                opt =>
                {
                    opt.MapFrom(src => src.PersonId);
                }
            );
    }
}