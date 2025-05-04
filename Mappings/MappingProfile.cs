using AutoMapper;
using GymBooking.API.Models;
using GymBooking.API.DTOs;

namespace GymBooking.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<Trainer, TrainerDto>();
            CreateMap<Session, SessionDto>()
                .ForMember(dest => dest.TrainerName, opt => opt.MapFrom(src => src.Trainer.Name));
            CreateMap<Booking, BookingDto>()
                .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => src.User.FullName))
                .ForMember(dest => dest.SessionName, opt => opt.MapFrom(src => src.Session.Name));
        }
    }
}
