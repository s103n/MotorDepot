using AutoMapper;
using MotorDepot.BLL.Models;
using MotorDepot.DAL.Entities;
using MotorDepot.DAL.Entities.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorDepot.BLL.Infrastructure.Mappers
{
    public static class MapperExtensions
    {
        public static IMapper MapperBLL = Mapper.GetConfig().CreateMapper();

        public static AutoDto ToDto(this Auto auto)
        {
            return MapperBLL.Map<Auto, AutoDto>(auto);
        }

        public static IEnumerable<AutoDto> ToDto(this IEnumerable<Auto> autos)
        {
            return autos.Select(x => x.ToDto());
        }

        public static Auto ToEntity(this AutoDto autoDto)
        {
            return MapperBLL.Map<AutoDto, Auto>(autoDto);
        }

        public static AutoBrand ToEntity(this AutoBrandDto model)
        {
            return MapperBLL.Map<AutoBrandDto, AutoBrand>(model);
        }

        public static AutoBrandDto ToDto(this AutoBrand model)
        {
            return MapperBLL.Map<AutoBrand, AutoBrandDto>(model);
        }

        public static IEnumerable<AutoBrandDto> ToDto(this IEnumerable<AutoBrand> models)
        {
            return models.Select(x => x.ToDto());
        }

        public static AppUser ToEntity(this UserDto userDto)
        {
            return MapperBLL.Map<UserDto, AppUser>(userDto);
        }

        public static UserDto ToUserDto(this AppUser user)
        {
            return MapperBLL.Map<AppUser, UserDto>(user);
        }

        public static IEnumerable<UserDto> ToDto(this IEnumerable<AppUser> appUsers)
        {
            return MapperBLL.Map<IEnumerable<AppUser>, IEnumerable<UserDto>>(appUsers);
        }

        public static DriverDto ToDriverDto(this AppUser user)
        {
            return MapperBLL.Map<AppUser, DriverDto>(user);
        }

        public static DispatcherDto ToDispatcherDto(this AppUser user)
        {
            return MapperBLL.Map<AppUser, DispatcherDto>(user);
        }

        public static AppUser ToEntity(this DriverDto driver)
        {
            return MapperBLL.Map<DriverDto, AppUser>(driver);
        }

        public static AppUser ToEntity(this DispatcherDto dispatcher)
        {
            return MapperBLL.Map<DispatcherDto, AppUser>(dispatcher);
        }

        public static LogEvent ToEntity(this LogEventDto model)
        {
            return MapperBLL.Map<LogEventDto, LogEvent>(model);
        }

        public static LogEventDto ToDto(this LogEvent model)
        {
            return MapperBLL.Map<LogEvent, LogEventDto>(model);
        }

        public static IEnumerable<LogEventDto> ToDto(this IEnumerable<LogEvent> models)
        {
            return models.Select(x => x.ToDto());
        }

        public static Flight ToEntity(this FlightDto model)
        {
            return MapperBLL.Map<FlightDto, Flight>(model);
        }

        public static FlightDto ToDto(this Flight model)
        {
            return MapperBLL.Map<Flight, FlightDto>(model);
        }

        public static IEnumerable<FlightDto> ToDto(this IEnumerable<Flight> models)
        {
            return models.Select(x => x.ToDto());
        }

        public static FlightRequest ToEntity(this FlightRequestDto request)
        {
            return MapperBLL.Map<FlightRequestDto, FlightRequest>(request);
        }

        public static FlightRequestDto ToDto(this FlightRequest request)
        {
            return MapperBLL.Map<FlightRequest, FlightRequestDto>(request);
        }

        public static IEnumerable<FlightRequestDto> ToDto(this IEnumerable<FlightRequest> requests)
        {
            return requests.Select(x => x.ToDto());
        }


    }
}
