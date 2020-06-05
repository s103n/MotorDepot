using AutoMapper;
using MotorDepot.BLL.Models;
using MotorDepot.Shared.Enums;
using MotorDepot.WEB.Models.Auto;
using MotorDepot.WEB.Models.Flight;
using MotorDepot.WEB.Models.FlightRequest;
using MotorDepot.WEB.Models.Logging;
using MotorDepot.WEB.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MotorDepot.WEB.Infrastructure.Mappers
{
    public static class MapperExtentions
    {
        public static IMapper MapperWEB = Mapper.GetConfig().CreateMapper();

        public static UserDto ToUserDto(this UserRegisterViewModel rvm, string role)
        {
            var usr = MapperWEB.Map<UserRegisterViewModel, UserDto>(rvm);
            usr.Role = role;
            return usr;
        }

        public static UserDto ToUserDto(this LoginViewModel model)
        {
            return MapperWEB.Map<LoginViewModel, UserDto>(model);
        }

        public static DriverDto ToDriverDto(this UserDto user, string id)
        {
            var usr = MapperWEB.Map<UserDto, DriverDto>(user);
            usr.Id = id;
            return usr;
        }

        public static DispatcherDto ToDispatcherDto(this UserDto user, string id)
        {
            var usr = MapperWEB.Map<UserDto, DispatcherDto>(user);
            usr.Id = id;
            return usr;
        }

        public static IEnumerable<DispatcherViewModel> ToViewModelDispatcher(this IEnumerable<UserDto> users)
        {
           return MapperWEB.Map<IEnumerable<UserDto>, IEnumerable<DispatcherViewModel>>(users);
        }

        public static IEnumerable<DriverViewModel> ToViewModelDriver(this IEnumerable<UserDto> users)
        {
            return MapperWEB.Map<IEnumerable<UserDto>, IEnumerable<DriverViewModel>>(users);
        }

        public static DriverDetailsViewModel ToDriverDetailsViewModel(this UserDto driver, IEnumerable<FlightViewModel> driverFlights)
        {
            var ddvm = MapperWEB.Map<UserDto, DriverDetailsViewModel>(driver);
            ddvm.Flights = driverFlights;
            return ddvm;
        }

        public static DispatcherDetailsViewModel ToDispatcherDetailsViewModel(this UserDto dispatcher,
            IEnumerable<FlightViewModel> dispatcherFlights,
            IEnumerable<FlightRequestDisplayViewModel> dispatcherFlightRequests)
        {
            var ddvm = MapperWEB.Map<UserDto, DispatcherDetailsViewModel>(dispatcher);
            ddvm.Flights = dispatcherFlights;
            ddvm.FlightRequests = dispatcherFlightRequests;
            return ddvm;
        }

        public static AutoSetViewModel ToSetViewModel(this AutoDto model)
        {
            return MapperWEB.Map<AutoDto, AutoSetViewModel>(model);
        }

        public static IEnumerable<AutoSetViewModel> ToSetViewModels(this IEnumerable<AutoDto> models)
        {
            return models.Select(x => x.ToSetViewModel());
        }

        public static AutoDto ToDto(this AutoCreateViewModel model)
        {
            return MapperWEB.Map<AutoCreateViewModel, AutoDto>(model);
        }

        public static AutoEditViewModel ToEditViewModel(this AutoDto model)
        {
            return MapperWEB.Map<AutoDto, AutoEditViewModel>(model);
        }

        public static AutoDto ToDto(this AutoEditViewModel model)
        {
            return MapperWEB.Map<AutoEditViewModel, AutoDto>(model);
        }

        public static IEnumerable<AutoDisplayViewModel> ToDisplayViewModel(
            this IEnumerable<AutoDto> model,
            IEnumerable<FlightDto> flights)
        {
            var advm = MapperWEB.Map<IEnumerable<AutoDto>, IEnumerable<AutoDisplayViewModel>>(model);
            foreach(var t in advm)
            {
                t.UsedInFlightNow = flights.IsInFlightByAutoId(t.Id);
            }
            return advm;
        }

        private static bool IsInFlightByAutoId(this IEnumerable<FlightDto> flights, int autoId)
        {
            return flights.Any(flight => flight.Auto != null
                                         && flight.Auto.Id == autoId
                                         && flight.Status == FlightStatus.Occupied
                                         || flight.Status == FlightStatus.Performed);
        }

        public static AutoDetailsViewModel ToDetailsViewModel(this AutoDto model)
        {
            return MapperWEB.Map<AutoDto, AutoDetailsViewModel>(model);
        }

        public static FlightViewModel ToDisplayViewModel(this FlightDto model)
        {
            return MapperWEB.Map<FlightDto, FlightViewModel>(model);
        }

        public static IEnumerable<FlightViewModel> ToDisplayViewModel(this IEnumerable<FlightDto> models)
        {
            return models.Select(x => x.ToDisplayViewModel());
        }

        public static FlightRequestDto ToDto(this FlightRequestCreateViewModel model)
        {
            return MapperWEB.Map<FlightRequestCreateViewModel, FlightRequestDto>(model);
        }

        public static FlightRequestDisplayViewModel ToDisplayViewModel(this FlightRequestDto model)
        {
            return MapperWEB.Map<FlightRequestDto, FlightRequestDisplayViewModel>(model);
        }

        public static IEnumerable<FlightRequestDisplayViewModel> ToDisplayViewModels(
            this IEnumerable<FlightRequestDto> models)
        {
            return models.Select(x => x.ToDisplayViewModel());
        }

        public static FlightRequestDetailsViewModel ToDetailsViewModel(this FlightRequestDto model)
        {
            return MapperWEB.Map<FlightRequestDto, FlightRequestDetailsViewModel>(model);
        }

        public static FlightDto ToDto(this FlightCreateViewModel model)
        {
            return MapperWEB.Map<FlightCreateViewModel, FlightDto>(model);
        }

        public static FlightDto ToDto(this FlightEditViewModel model)
        {
            return MapperWEB.Map<FlightEditViewModel, FlightDto>(model);
        }

        public static FlightEditViewModel ToEditViewModel(this FlightDto model)
        {
            return MapperWEB.Map<FlightDto, FlightEditViewModel>(model);
        }

        public static FlightDetailsViewModel ToDetailsViewModel(this FlightDto model)
        {
            return MapperWEB.Map<FlightDto, FlightDetailsViewModel>(model);
        }

        public static LogViewModel ToViewModel(this LogEventDto log)
        {
            return MapperWEB.Map<LogEventDto, LogViewModel>(log);
        }

        public static IEnumerable<LogViewModel> ToViewModel(this IEnumerable<LogEventDto> logs)
        {
            return logs.Select(x => x.ToViewModel());
        }

        public static LogActionDetailsViewModel ToDetailsAction(this LogEventDto log)
        {
            return MapperWEB.Map<LogEventDto, LogActionDetailsViewModel>(log);
        }

        public static LogExceptionDetailsViewModel ToDetailsException(this LogEventDto log)
        {
            return MapperWEB.Map<LogEventDto, LogExceptionDetailsViewModel>(log);
        }
    }
}