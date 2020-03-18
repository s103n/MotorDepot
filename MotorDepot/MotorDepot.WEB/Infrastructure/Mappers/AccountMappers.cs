using System.Collections.Generic;
using System.Net.NetworkInformation;
using AutoMapper;
using MotorDepot.BLL.Models;
using MotorDepot.WEB.Models.Auto;
using MotorDepot.WEB.Models.Flight;
using MotorDepot.WEB.Models.FlightRequest;
using MotorDepot.WEB.Models.User;

namespace MotorDepot.WEB.Infrastructure.Mappers
{
    public static class AccountMappers
    {
        public static UserDto ToUserDto(this UserRegisterViewModel rvm, string role)
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserRegisterViewModel, UserDto>()
                    .ForMember("Role", opt => opt.MapFrom(x => role));
            }).CreateMapper().Map<UserRegisterViewModel, UserDto>(rvm);
        }

        public static UserDto ToUserDto(this LoginViewModel model)
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<LoginViewModel, UserDto>(); 

            }).CreateMapper().Map<LoginViewModel, UserDto>(model);
        }

        public static DriverDto ToDriverDto(this UserDto user, string id)
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDto, DriverDto>()
                    .ForMember("Id", opt => opt.MapFrom(x => id));
            }).CreateMapper().Map<UserDto, DriverDto>(user);
        }

        public static DispatcherDto ToDispatcherDto(this UserDto user, string id)
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDto, DispatcherDto>()
                    .ForMember("Id", opt => opt.MapFrom(x => id));
            }).CreateMapper().Map<UserDto, DispatcherDto>(user);
        }

        public static IEnumerable<DispatcherViewModel> ToViewModelDispatcher(this IEnumerable<UserDto> users)
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDto, DispatcherViewModel>()
                    .ForMember(x => x.Name, opt => opt.MapFrom(x => x.FirstName + " " + x.LastName));
            }).CreateMapper().Map<IEnumerable<UserDto>, IEnumerable<DispatcherViewModel>>(users);
        }

        public static IEnumerable<DriverViewModel> ToViewModelDriver(this IEnumerable<UserDto> users)
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDto, DriverViewModel>()
                    .ForMember(x => x.Name, opt => opt.MapFrom(x => x.FirstName + " " + x.LastName));
            }).CreateMapper().Map<IEnumerable<UserDto>, IEnumerable<DriverViewModel>>(users);
        }

        public static DriverDetailsViewModel ToDriverDetailsViewModel(this UserDto driver, 
            IEnumerable<FlightViewModel> driverFlights)
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDto, DriverDetailsViewModel>()
                    .ForMember(x => x.Flights, opt => opt.MapFrom(x => driverFlights));
            }).CreateMapper().Map<UserDto, DriverDetailsViewModel>(driver);
        }

        public static DispatcherDetailsViewModel ToDispatcherDetailsViewModel(this UserDto dispatcher,
            IEnumerable<FlightViewModel> dispatcherFlights,
            IEnumerable<FlightRequestDisplayViewModel> dispatcherFlightRequests)
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDto, DispatcherDetailsViewModel>()
                    .ForMember(x => x.Flights, opt => opt.MapFrom(x => dispatcherFlights))
                    .ForMember("FlightRequests", opt => opt.MapFrom(x => dispatcherFlightRequests));
            }).CreateMapper().Map<UserDto, DispatcherDetailsViewModel>(dispatcher);
        }
    }
}