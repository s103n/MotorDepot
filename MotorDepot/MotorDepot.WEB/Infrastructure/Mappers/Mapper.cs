using AutoMapper;
using MotorDepot.BLL.Models;
using MotorDepot.Shared.Enums;
using MotorDepot.WEB.Models.Auto;
using MotorDepot.WEB.Models.Flight;
using MotorDepot.WEB.Models.FlightRequest;
using MotorDepot.WEB.Models.Logging;
using MotorDepot.WEB.Models.User;

namespace MotorDepot.WEB.Infrastructure.Mappers
{
    public static class Mapper
    {
        public static MapperConfiguration GetConfig()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserRegisterViewModel, UserDto>();
                cfg.CreateMap<LoginViewModel, UserDto>();
                cfg.CreateMap<UserDto, DriverDto>();
                cfg.CreateMap<UserDto, DispatcherDto>();
                cfg.CreateMap<UserDto, DispatcherViewModel>()
                    .ForMember(x => x.Name, opt => opt.MapFrom(x => x.FirstName + " " + x.LastName));
                cfg.CreateMap<UserDto, DriverViewModel>()
                    .ForMember(x => x.Name, opt => opt.MapFrom(x => x.FirstName + " " + x.LastName));
                cfg.CreateMap<UserDto, DriverDetailsViewModel>();
                cfg.CreateMap<UserDto, DispatcherDetailsViewModel>();
                cfg.CreateMap<AutoDto, AutoSetViewModel>()
                    .ForMember("AutoBrand", opt => opt.MapFrom(x => x.Brand.Name));
                cfg.CreateMap<AutoCreateViewModel, AutoDto>()
                    .ForMember("Status", opt => opt.MapFrom(x => (AutoType)x.AutoTypeId))
                    .ForMember("Brand", opt => opt.MapFrom(x => new AutoBrandDto { Id = x.AutoBrandId }));
                cfg.CreateMap<AutoDto, AutoEditViewModel>()
                    .ForMember("AutoBrandId", opt => opt.MapFrom(x => x.Brand.Id))
                    .ForMember("AutoTypeId", opt => opt.MapFrom(x => (int)x.Type));
                cfg.CreateMap<AutoEditViewModel, AutoDto>()
                    .ForMember("Brand", opt => opt.MapFrom(x => new AutoBrandDto { Id = x.AutoBrandId }))
                    .ForMember("Type", opt => opt.MapFrom(x => (AutoType)x.AutoTypeId));
                cfg.CreateMap<AutoDto, AutoDisplayViewModel>()
                    .ForMember("Brand", opt => opt.MapFrom(x => x.Brand.Name))
                    .ForMember("Type", opt => opt.MapFrom(x => x.Type.ToString()))
                    .ForMember("Status", opt => opt.MapFrom(x => x.Status.ToString()));
                cfg.CreateMap<AutoDto, AutoDetailsViewModel>()
                    .ForMember(x => x.AutoBrand, opt => opt.MapFrom(x => x.Brand.Name))
                    .ForMember(x => x.AutoType, opt => opt.MapFrom(x => x.Type))
                    .ForMember(x => x.AutoStatus, opt => opt.MapFrom(x => x.Status));
                cfg.CreateMap<FlightDto, FlightViewModel>()
                    .ForMember("AutoName", opt => opt.MapFrom(x => x.Auto.Model))
                    .ForMember("AutoNumbers", opt => opt.MapFrom(x => x.Auto.Numbers))
                    .ForMember("DriverEmail", opt => opt.MapFrom(x => x.Driver.Email))
                    .ForMember("DriverName", opt => opt.MapFrom(x => $"{x.Driver.FirstName} {x.Driver.LastName}"))
                    .ForMember("AutoId", opt => opt.MapFrom(x => x.Auto.Id));
                cfg.CreateMap<FlightRequestCreateViewModel, FlightRequestDto>()
                    .ForMember("Driver", opt => opt.MapFrom(x => new DriverDto { Id = x.DriverId }))
                    .ForMember("Dispatcher", opt => opt.MapFrom(x => new DispatcherDto()))
                    .ForMember("RequestedFlight", opt => opt.MapFrom(x => new FlightDto { Id = x.RequestedFlightId }));
                cfg.CreateMap<FlightRequestDto, FlightRequestDisplayViewModel>()
                    .ForMember("DriverName", opt => opt.MapFrom(x => $"{x.Driver.FirstName} {x.Driver.LastName}"));
                cfg.CreateMap<FlightRequestDto, FlightRequestDetailsViewModel>()
                    .ForMember("DriverName", opt => opt.MapFrom(x => $"{x.Driver.FirstName} {x.Driver.LastName}"))
                    .ForMember("DriverEmail", opt => opt.MapFrom(x => x.Driver.Email));
                cfg.CreateMap<FlightCreateViewModel, FlightDto>()
                    .ForMember("DispatcherCreator", opt => opt.MapFrom(x => new DispatcherDto { Id = x.DispatcherCreatorId }));
                cfg.CreateMap<FlightEditViewModel, FlightDto>();
                cfg.CreateMap<FlightDto, FlightEditViewModel>();
                cfg.CreateMap<FlightDto, FlightDetailsViewModel>()
                    .ForMember(x => x.Status, o => o.MapFrom(x => x.Status.ToString()))
                    .ForMember(x => x.AutoId, o => o.MapFrom(x => x.Auto == null ? (int?)null : x.Auto.Id))
                    .ForMember(x => x.DriverEmail, o => o.MapFrom(x => x.Driver == null ? null : x.Driver.Id))
                    .ForMember(x => x.DispatcherCreatorEmail, o => o.MapFrom(x => x.DispatcherCreator.Email));
                cfg.CreateMap<LogEventDto, LogViewModel>()
                    .ForMember(x => x.LogType, opt => opt.MapFrom(x => x.LogType));
                cfg.CreateMap<LogEventDto, LogActionDetailsViewModel>();
                cfg.CreateMap<LogEventDto, LogExceptionDetailsViewModel>();
            });
        }
    }
}