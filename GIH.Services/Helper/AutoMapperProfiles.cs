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

        CreateMap<Restaurant, RestaurantDto>().ForMember(dest => dest.restaurantId,
            opt =>
            {
                opt.MapFrom(src => src.restaurantId);
            }
        ).ForMember(dest => dest.restaurantName,
            opt =>
            {
                opt.MapFrom(src => src.restaurantName);
            }
        ).ForMember(dest => dest.restaurantAdress,
            opt =>
            {
                opt.MapFrom(src => src.restaurantAdress);
            }
        ).ForMember(dest => dest.restaurantPhoneNumber,
            opt =>
            {
                opt.MapFrom(src => src.restaurantNumber);
            }
        );

        CreateMap<Advert, AdvertDto>().ForMember(dest => dest.AdvertName,
            opt =>
            {
                opt.MapFrom(src => src.AdvertName);
            }
        ).ForMember(dest => dest.AdvertDescription,
            opt =>
            {
                opt.MapFrom(src => src.AdvertDescription);
            }
        ).ForMember(dest => dest.AdvertKilo,
            opt =>
            {
                opt.MapFrom(src => src.AdvertKilo);
            }
        ).ForMember(dest => dest.AdvertDate,
            opt =>
            {
                opt.MapFrom(src => src.AdvertDate);
            }
        );
        
        CreateMap<Person, PersonForAuthenticationDto>().ForMember(dest => dest.Username,
            opt =>
            {
                opt.MapFrom(src => src.PersonNickName);
            }
        ).ForMember(dest => dest.Password,
            opt =>
            {
                opt.MapFrom(src => src.PersonPassword);
            }
        );
        
        CreateMap<Restaurant, RestaurantForAuthentication>().ForMember(dest => dest.Username,
            opt =>
            {
                opt.MapFrom(src => src.restaurantNickname);
            }
        ).ForMember(dest => dest.Password,
            opt =>
            {
                opt.MapFrom(src => src.restaurantPassword);
            }
        );
    }
}