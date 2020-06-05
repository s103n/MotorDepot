using AutoMapper;
using MotorDepot.BLL.Models;
using MotorDepot.DAL.Entities;
using MotorDepot.DAL.Entities.Logging;

namespace MotorDepot.BLL.Infrastructure.Mappers
{
    public static class Mapper
    {
        public static MapperConfiguration GetConfig()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Auto, AutoDto>()
                    .ForMember("Brand", opt => opt.MapFrom(x => x.Brand.ToDto()))
                    .ForMember("Type", opt => opt.MapFrom(x => x.AutoTypeLookupId))
                    .ForMember("Status", opt => opt.MapFrom(x => x.AutoStatusLookupId));
                cfg.CreateMap<AutoDto, Auto>()
                    .ForMember("AutoTypeLookupId", opt => opt.MapFrom(x => x.Type))
                    .ForMember("AutoStatusLookupId", opt => opt.MapFrom(x => x.Status))
                    .ForMember("AutoBrandId", opt => opt.MapFrom(x => x.Brand.Id))
                    .ForMember("Status", opt => opt.Ignore())
                    .ForMember("Brand", opt => opt.Ignore())
                    .ForMember("Type", opt => opt.Ignore());
                cfg.CreateMap<AutoBrandDto, AutoBrand>()
                    .ForMember("Autos", opt => opt.Ignore());
                cfg.CreateMap<AutoBrand, AutoBrandDto>()
                    .ForMember("Autos", opt => opt.Ignore());
                cfg.CreateMap<FlightDto, Flight>()
                     .ForMember("FlightStatusLookupId", opt => opt.MapFrom(x => x.Status))
                     .ForMember("Status", opt => opt.Ignore())
                     .ForMember("Auto", opt => opt.Ignore())
                     .ForMember("AutoId", opt => opt.MapFrom(x => x.Auto.Id))
                     .ForMember("Driver", opt => opt.Ignore())
                     .ForMember("DriverId", opt => opt.MapFrom(x => x.Driver.Id))
                     .ForMember("CreatorId", opt => opt.MapFrom(x => x.DispatcherCreator.Id));
                cfg.CreateMap<Flight, FlightDto>()
                     .ForMember("Status", opt => opt.MapFrom(x => x.FlightStatusLookupId))
                     .ForMember("Auto", opt => opt.MapFrom(x => x.Auto.ToDto()))
                     .ForMember("Driver", opt => opt.MapFrom(x => x.Driver.ToDriverDto()))
                     .ForMember("DispatcherCreator", opt => opt.MapFrom(x => x.Creator.ToDispatcherDto()));
                cfg.CreateMap<FlightRequestDto, FlightRequest>()
                    .ForMember("Driver", opt => opt.Ignore())
                    .ForMember("Dispatcher", opt => opt.Ignore())
                    .ForMember("Status", opt => opt.Ignore())
                    .ForMember("AutoType", opt => opt.Ignore())
                    .ForMember("RequestedFlight", opt => opt.Ignore())
                    .ForMember("DriverId", opt => opt.MapFrom(x => x.Driver.Id))
                    .ForMember("DispatcherId", opt => opt.MapFrom(x => x.Dispatcher.Id))
                    .ForMember("FlightRequestStatusLookupId", opt => opt.MapFrom(x => (int)x.Status))
                    .ForMember("FlightId", opt => opt.MapFrom(x => x.RequestedFlight.Id))
                    .ForMember("AutoTypeId", opt => opt.MapFrom(x => (int)x.AutoType));
                cfg.CreateMap<FlightRequest, FlightRequestDto>()
                    .ForMember("Driver", opt => opt.MapFrom(x => x.Driver.ToDriverDto()))
                    .ForMember("Dispatcher", opt => opt.MapFrom(x => x.Dispatcher.ToDispatcherDto()))
                    .ForMember("Status", opt => opt.MapFrom(x => x.FlightRequestStatusLookupId))
                    .ForMember("RequestedFlight", opt => opt.MapFrom(x => x.RequestedFlight.ToDto()))
                    .ForMember("AutoType", opt => opt.MapFrom(x => x.AutoTypeId));
                cfg.CreateMap<LogEventDto, LogEvent>()
                    .ForMember(x => x.LogTypeLookupId, opt => opt.MapFrom(x => x.LogType))
                    .ForMember(x => x.LogType, opt => opt.Ignore());
                cfg.CreateMap<LogEvent, LogEventDto>()
                    .ForMember(x => x.LogType, opt => opt.MapFrom(x => x.LogType.Name));
                cfg.CreateMap<UserDto, AppUser>()
                    .ForMember("UserName", opt => opt.MapFrom(x => x.Email));
                cfg.CreateMap<AppUser, UserDto>();
                cfg.CreateMap<AppUser, DriverDto>();
                cfg.CreateMap<AppUser, DispatcherDto>();
                cfg.CreateMap<DriverDto, AppUser>();
                cfg.CreateMap<DispatcherDto, AppUser>();
            });
        }
    }
}
